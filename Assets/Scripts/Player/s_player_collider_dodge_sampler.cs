using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_player_collider_dodge_sampler_pathing_search
{
    [Header("Configurable Variables")]
    [SerializeField] public int v_player_collider_dodge_sampler_pathing_search_duration;
    [Header("Reference Variables")]
    [SerializeField] public int v_player_collider_dodge_sampler_pathing_search_duration_timer;
}

public class s_player_collider_dodge_sampler : MonoBehaviour
{
    [Header("Player Collider Dodge Sampler Setup")]
    [SerializeField] public GameObject v_player_collider_dodge_sampler_player_collider_gameobject;
    [SerializeField] public s_player_collider_controller v_player_collider_dodge_sampler_player_collider_gameobject_script;
    [Header("Player Collider Dodge Sampler References")]
    [SerializeField] public List<GameObject> v_player_collider_dodge_sampler_pathing_current_collisions_list;
    [Header("Player Collider Dodge Sampler Distance Setup")]
    [SerializeField] public float v_player_collider_dodge_sampler_pathing_distance_threshold;
    [SerializeField] public float v_player_collider_dodge_sampler_embed_distance_threshold;
    [Header("Player Collider Dodge Sampler Search Duration Setup")]
    [SerializeField] public svl_player_collider_dodge_sampler_pathing_search v_player_collider_dodge_sampler_pathing_search_setup = new svl_player_collider_dodge_sampler_pathing_search();

    void Update()
    {
        if (v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_detected)
        {
            if (v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_embedding_allowed)
            {
                if (v_player_collider_dodge_sampler_pathing_current_collisions_list.Count > 0)
                {
                    v_player_collider_dodge_sampler_pathing_search_setup.v_player_collider_dodge_sampler_pathing_search_duration_timer = v_player_collider_dodge_sampler_pathing_search_setup.v_player_collider_dodge_sampler_pathing_search_duration;

                    int tv_target_index = 0;
                    if (v_player_collider_dodge_sampler_pathing_current_collisions_list.Count > 1)
                    {
                        if (Vector3.Distance(v_player_collider_dodge_sampler_player_collider_gameobject.transform.position, v_player_collider_dodge_sampler_pathing_current_collisions_list[0].transform.position) >= v_player_collider_dodge_sampler_embed_distance_threshold)
                        {
                            tv_target_index = 0;
                        }
                        else
                        {
                            tv_target_index = 1;
                        }
                    }
                    
                    v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_detected_target.v_player_collider_movement_target_pathing = v_player_collider_dodge_sampler_pathing_current_collisions_list[tv_target_index];
                    v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_detected_target.v_player_collider_movement_target_pathing_script = v_player_collider_dodge_sampler_pathing_current_collisions_list[tv_target_index].GetComponent<s_pathing>();

                    v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_embedding_allowed = false;
                }
                else
                {
                    if (v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_time_handler_setup.v_time_handler_script.f_time_level_gate_get(v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_time_handler_setup.v_time_handler_level))
                    {
                        v_player_collider_dodge_sampler_pathing_search_setup.v_player_collider_dodge_sampler_pathing_search_duration_timer -= 1;
                    }

                    if (v_player_collider_dodge_sampler_pathing_search_setup.v_player_collider_dodge_sampler_pathing_search_duration_timer <= 0)
                    {
                        v_player_collider_dodge_sampler_pathing_search_setup.v_player_collider_dodge_sampler_pathing_search_duration_timer = v_player_collider_dodge_sampler_pathing_search_setup.v_player_collider_dodge_sampler_pathing_search_duration;

                        v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_detected = false;
                        v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_embedding_allowed = false;
                        transform.localPosition = Vector3.zero;
                    }
                }
            }
        }

        if (v_player_collider_dodge_sampler_pathing_search_setup.v_player_collider_dodge_sampler_pathing_search_duration_timer <= 0)
        {
            v_player_collider_dodge_sampler_pathing_search_setup.v_player_collider_dodge_sampler_pathing_search_duration_timer = v_player_collider_dodge_sampler_pathing_search_setup.v_player_collider_dodge_sampler_pathing_search_duration;
        }
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        if (!v_player_collider_dodge_sampler_pathing_current_collisions_list.Contains(sv_other_object.gameObject) && (sv_other_object.gameObject != v_player_collider_dodge_sampler_player_collider_gameobject))
        {
            if (Vector3.Distance(v_player_collider_dodge_sampler_player_collider_gameobject.transform.position, sv_other_object.gameObject.transform.position) >= v_player_collider_dodge_sampler_pathing_distance_threshold)
            {
                if ((v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_detected) && (v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_embedding_allowed))
                {
                    if (sv_other_object.gameObject.TryGetComponent<s_pathing>(out var ov_pathing))
                    {
                        if (v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                        {
                            if (!ov_pathing.v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.None))
                            {
                                v_player_collider_dodge_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
                                //v_player_collider_dodge_sampler_pathing_current_collisions_list.Insert(0, sv_other_object.gameObject);
                            }
                        }
                        else if (v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.Flying))
                        {
                            if
                                (
                                    (ov_pathing.v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                                    ||
                                    (ov_pathing.v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.Flying))
                                )
                            {
                                v_player_collider_dodge_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
                                //v_player_collider_dodge_sampler_pathing_current_collisions_list.Insert(0, sv_other_object.gameObject);
                            }
                        }
                        else if (v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.Walking))
                        {
                            if
                                (
                                    (ov_pathing.v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                                    ||
                                    (ov_pathing.v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.Walking))
                                )
                            {
                                v_player_collider_dodge_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
                                //v_player_collider_dodge_sampler_pathing_current_collisions_list.Insert(0, sv_other_object.gameObject);
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider sv_other_object)
    {

    }

    private void OnTriggerExit(Collider sv_other_object)
    {
        
    }
}
