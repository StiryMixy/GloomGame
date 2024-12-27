using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_player_collider_dodge_sampler_movement
{
    [Header("Configurable Variables")]
    [SerializeField] public float v_player_collider_dodge_sampler_return_movement_speed;
    [SerializeField] public float v_player_collider_dodge_sampler_shoot_movement_speed;
    [SerializeField] public float v_player_collider_dodge_sampler_search_movement_speed;
    [Header("Reference Variables")]
    [SerializeField] public Vector3 v_player_collider_dodge_sampler_movement_detected_target;
    [SerializeField] public Vector3 v_player_collider_dodge_sampler_movement_intended_target;
    [SerializeField] public float v_player_collider_dodge_sampler_target_distance;
    [SerializeField] public bool v_player_collider_dodge_sampler_max_distance_reached;
    [SerializeField] public bool v_player_collider_dodge_sampler_pathing_detected;
    [SerializeField] public bool v_player_collider_dodge_sampler_embed_detected_pathing;
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
    [Header("Player Collider Dodge Sampler Movement Setup")]
    [SerializeField] public svl_player_collider_dodge_sampler_movement v_player_collider_dodge_sampler_movement_setup = new svl_player_collider_dodge_sampler_movement();

    void Update()
    {
        f_sampler_movement_handler();
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        if (!v_player_collider_dodge_sampler_pathing_current_collisions_list.Contains(sv_other_object.gameObject) && (sv_other_object.gameObject != v_player_collider_dodge_sampler_player_collider_gameobject))
        {
            if (Vector3.Distance(v_player_collider_dodge_sampler_player_collider_gameobject.transform.position, sv_other_object.gameObject.transform.position) > v_player_collider_dodge_sampler_pathing_distance_threshold)
            {
                if (sv_other_object.gameObject.TryGetComponent<s_pathing>(out var ov_pathing))
                {
                    if (v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                    {
                        if (!ov_pathing.v_pathing_type.Equals(v_tags_movement_mode_list.None))
                        {
                            v_player_collider_dodge_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
                        }
                    }
                    else if (v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.Flying))
                    {
                        if
                            (
                                (ov_pathing.v_pathing_type.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                                ||
                                (ov_pathing.v_pathing_type.Equals(v_tags_movement_mode_list.Flying))
                            )
                        {
                            v_player_collider_dodge_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
                        }
                    }
                    else if (v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.Walking))
                    {
                        if
                            (
                                (ov_pathing.v_pathing_type.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                                ||
                                (ov_pathing.v_pathing_type.Equals(v_tags_movement_mode_list.Walking))
                            )
                        {
                            v_player_collider_dodge_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
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
        if (v_player_collider_dodge_sampler_pathing_current_collisions_list.Count > 0)
        {
            if (v_player_collider_dodge_sampler_pathing_current_collisions_list.Contains(sv_other_object.gameObject))
            {
                v_player_collider_dodge_sampler_pathing_current_collisions_list.Remove(sv_other_object.gameObject);
            }
        }
    }

    public void f_sampler_movement_handler()
    {
        v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_target_distance = Vector3.Distance(v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_intended_target, v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_detected_target);
        bool tv_local_position_zero_check = f_sampler_distance_check(0);

        if ((v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_target_distance > 0) && (tv_local_position_zero_check))
        {
            v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_intended_target = v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_detected_target;
        }

        if (v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_max_distance_reached)
        {
            if (v_player_collider_dodge_sampler_pathing_current_collisions_list.Count > 0)
            {
                v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_pathing_detected = true;

                if (v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_embed_detected_pathing)
                {
                    v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing = v_player_collider_dodge_sampler_pathing_current_collisions_list[0];
                    v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing_script = v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing.GetComponent<s_pathing>();

                    v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_embed_detected_pathing = false;
                }
            }

            if (v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_pathing_detected)
            {
                f_sampler_move_towards(Vector3.zero, v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_return_movement_speed);
            }
            else
            {
                f_sampler_move_towards(Vector3.zero, v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_search_movement_speed);
            }

            if (tv_local_position_zero_check)
            {
                v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_max_distance_reached = false;

                if (v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_pathing_detected)
                {
                    v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_pathing_detected = false;
                }
                else
                {
                    v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_embed_detected_pathing = false;
                    v_player_collider_dodge_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_detected = false;
                }
            }
        }
        else
        {
            v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_pathing_detected = false;

            if (Vector3.Distance(transform.localPosition, v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_intended_target) <= 0)
            {
                v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_max_distance_reached = true;
                v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_detected_target = Vector3.zero;
                v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_intended_target = Vector3.zero;
            }
            else
            {
                f_sampler_move_towards(v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_intended_target, v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_shoot_movement_speed);
            }

            if (tv_local_position_zero_check)
            {
                v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_max_distance_reached = false;
            }
        }
    }

    public void f_sampler_move_towards(Vector3 sv_target_position, float sv_determined_speed)
    {
        if (Vector3.Distance(transform.localPosition, sv_target_position) > 0)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, sv_target_position, sv_determined_speed);
        }
    }

    public bool f_sampler_distance_check(float sv_distance_threshold)
    {
        return (Vector3.Distance(transform.localPosition, Vector3.zero) <= sv_distance_threshold);
    }
}
