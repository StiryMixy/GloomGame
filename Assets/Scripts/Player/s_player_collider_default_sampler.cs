using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

public class s_player_collider_default_sampler : MonoBehaviour
{
    [Header("Player Collider Default Sampler Setup")]
    [SerializeField] public GameObject v_player_collider_default_sampler_player_collider_gameobject;
    [SerializeField] public s_player_collider_controller v_player_collider_default_sampler_player_collider_gameobject_script;
    [Header("Player Collider Default Sampler References")]
    [SerializeField] public List<GameObject> v_player_collider_default_sampler_pathing_current_collisions_list;
    [Header("Player Collider Default Sampler Distance Setup")]
    [SerializeField] public float v_player_collider_default_sampler_pathing_distance_threshold;

    void Update()
    {
        if ((!v_player_collider_default_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_detected) && (v_player_collider_default_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_cooldown_counter <= 0))
        {
            if (v_player_collider_default_sampler_pathing_current_collisions_list.Count > 0)
            {
                v_player_collider_default_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_default_detected_target.v_player_collider_movement_target_pathing = v_player_collider_default_sampler_pathing_current_collisions_list[0];
                v_player_collider_default_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_default_detected_target.v_player_collider_movement_target_pathing_script = v_player_collider_default_sampler_pathing_current_collisions_list[0].GetComponent<s_pathing>();
            }
        }
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        if (!v_player_collider_default_sampler_pathing_current_collisions_list.Contains(sv_other_object.gameObject) && (sv_other_object.gameObject != v_player_collider_default_sampler_player_collider_gameobject))
        {
            if (Vector3.Distance(v_player_collider_default_sampler_player_collider_gameobject.transform.position, sv_other_object.gameObject.transform.position) >= v_player_collider_default_sampler_pathing_distance_threshold)
            {
                if (sv_other_object.gameObject.TryGetComponent<s_pathing>(out var ov_pathing))
                {
                    if (v_player_collider_default_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                    {
                        if (!ov_pathing.v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.None))
                        {
                            v_player_collider_default_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
                            //v_player_collider_default_sampler_pathing_current_collisions_list.Insert(0, sv_other_object.gameObject);
                        }
                    }
                    else if (v_player_collider_default_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.Flying))
                    {
                        if
                            (
                                (ov_pathing.v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                                ||
                                (ov_pathing.v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.Flying))
                            )
                        {
                            v_player_collider_default_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
                            //v_player_collider_default_sampler_pathing_current_collisions_list.Insert(0, sv_other_object.gameObject);
                        }
                    }
                    else if (v_player_collider_default_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.Walking))
                    {
                        if
                            (
                                (ov_pathing.v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                                ||
                                (ov_pathing.v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.Walking))
                            )
                        {
                            v_player_collider_default_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
                            //v_player_collider_default_sampler_pathing_current_collisions_list.Insert(0, sv_other_object.gameObject);
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
        if (v_player_collider_default_sampler_pathing_current_collisions_list.Count > 0)
        {
            if (v_player_collider_default_sampler_pathing_current_collisions_list.Contains(sv_other_object.gameObject))
            {
                v_player_collider_default_sampler_pathing_current_collisions_list.Remove(sv_other_object.gameObject);
            }
        }
    }
}
