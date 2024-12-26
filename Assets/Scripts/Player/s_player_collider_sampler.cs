using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_player_collider_sampler_movement
{
    [Header("Configurable Variables")]
    [SerializeField] public float v_player_collider_sampler_return_run_movement_speed;
    [SerializeField] public float v_player_collider_sampler_return_walk_movement_speed;
    [SerializeField] public float v_player_collider_sampler_return_dodge_movement_speed;
    [SerializeField] public float v_player_collider_sampler_shoot_run_movement_speed;
    [SerializeField] public float v_player_collider_sampler_shoot_walk_movement_speed;
    [SerializeField] public float v_player_collider_sampler_shoot_dodge_movement_speed;
    [SerializeField] public float v_player_collider_sampler_search_dodge_movement_speed;
    [SerializeField] public float v_player_collider_sampler_change_direction_distance_threshold;
    [SerializeField] public float v_player_collider_sampler_dodge_distance_threshold;
    [Header("Reference Variables")]
    [SerializeField] public Vector3 v_player_collider_sampler_movement_detected_target;
    [SerializeField] public Vector3 v_player_collider_sampler_movement_intended_target;
    [SerializeField] public float v_player_collider_sampler_target_distance;
    [SerializeField] public float v_player_collider_sampler_return_movement_speed;
    [SerializeField] public float v_player_collider_sampler_shoot_movement_speed;
    [SerializeField] public Vector3 v_player_collider_sampler_dodge_detected_target;
    [SerializeField] public Vector3 v_player_collider_sampler_dodge_intended_target;
    [SerializeField] public bool v_player_collider_sampler_dodge_destination_reached = false;
    [SerializeField] public bool v_player_collider_sampler_dodge_pathing_detected = false;
}

public class s_player_collider_sampler : MonoBehaviour
{
    [Header("Player Collider Sampler Setup")]
    [SerializeField] public GameObject v_player_collider_sampler_player_collider_gameobject;
    [SerializeField] public s_player_collider_controller v_player_collider_sampler_player_collider_gameobject_script;
    [Header("Player Collider Sampler References")]
    [SerializeField] public List<GameObject> v_player_collider_sampler_pathing_current_collisions_list;
    [Header("Player Collider Sampler Movement Setup")]
    [SerializeField] public svl_player_collider_sampler_movement v_player_collider_sampler_movement_setup = new svl_player_collider_sampler_movement();

    void Update()
    {
        f_sampler_movement_handler();
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        if (!v_player_collider_sampler_pathing_current_collisions_list.Contains(sv_other_object.gameObject) && (sv_other_object.gameObject != v_player_collider_sampler_player_collider_gameobject))
        {
            if (sv_other_object.gameObject.TryGetComponent<s_pathing>(out var ov_pathing))
            {
                if (v_player_collider_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                {
                    if (!ov_pathing.v_pathing_type.Equals(v_tags_movement_mode_list.None))
                    {
                        v_player_collider_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
                    }
                }
                else if (v_player_collider_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.Flying))
                {
                    if
                        (
                            (ov_pathing.v_pathing_type.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                            ||
                            (ov_pathing.v_pathing_type.Equals(v_tags_movement_mode_list.Flying))
                        )
                    {
                        v_player_collider_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
                    }
                }
                else if (v_player_collider_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_mode.Equals(v_tags_movement_mode_list.Walking))
                {
                    if
                        (
                            (ov_pathing.v_pathing_type.Equals(v_tags_movement_mode_list.WalkingAndFlying))
                            ||
                            (ov_pathing.v_pathing_type.Equals(v_tags_movement_mode_list.Walking))
                        )
                    {
                        v_player_collider_sampler_pathing_current_collisions_list.Add(sv_other_object.gameObject);
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
        if (v_player_collider_sampler_pathing_current_collisions_list.Count > 0)
        {
            if (v_player_collider_sampler_pathing_current_collisions_list.Contains(sv_other_object.gameObject))
            {
                v_player_collider_sampler_pathing_current_collisions_list.Remove(sv_other_object.gameObject);
            }
        }
    }

    public void f_sampler_movement_handler()
    {
        v_player_collider_sampler_movement_setup.v_player_collider_sampler_target_distance = Vector3.Distance(v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_intended_target, v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_detected_target);
        bool tv_normal_movement_check = true;

        if (v_player_collider_sampler_player_collider_gameobject_script.v_player_collider_dodge_setup.v_player_collider_dodge_cooldown_counter > 0)
        {
            tv_normal_movement_check = false;
            v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_detected_target = Vector3.zero;
            v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_intended_target = Vector3.zero;

            v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_detected_target = Vector3.zero;
            v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_intended_target = Vector3.zero;

            v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_destination_reached = false;
            v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_pathing_detected = false;
        }
        else if (v_player_collider_sampler_player_collider_gameobject_script.v_player_collider_dodge_setup.v_player_collider_dodge_detected)
        {
            tv_normal_movement_check = false;
            v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_detected_target = Vector3.zero;
            v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_intended_target = Vector3.zero;

            v_player_collider_sampler_movement_setup.v_player_collider_sampler_return_movement_speed = v_player_collider_sampler_movement_setup.v_player_collider_sampler_return_dodge_movement_speed;
            v_player_collider_sampler_movement_setup.v_player_collider_sampler_shoot_movement_speed = v_player_collider_sampler_movement_setup.v_player_collider_sampler_shoot_dodge_movement_speed;

            if ((!v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_intended_target.Equals(v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_detected_target)))
            {
                v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_intended_target = v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_detected_target;
            }

            if (v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_destination_reached)
            {
                if (v_player_collider_sampler_pathing_current_collisions_list.Count > 0)
                {
                    v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_pathing_detected = true;
                }

                if (v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_pathing_detected)
                {
                    v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_detected_target = Vector3.zero;
                    v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_intended_target = Vector3.zero;
                    f_sampler_move_towards(Vector3.zero, v_player_collider_sampler_movement_setup.v_player_collider_sampler_return_movement_speed);
                }
                else
                {
                    f_sampler_move_towards(Vector3.zero, v_player_collider_sampler_movement_setup.v_player_collider_sampler_search_dodge_movement_speed);
                }
            }
            else
            {
                v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_pathing_detected = false;

                if (Vector3.Distance(transform.localPosition, v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_intended_target) <= v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_distance_threshold)
                {
                    v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_destination_reached = true;
                }
                else
                {
                    f_sampler_move_towards(v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_intended_target, v_player_collider_sampler_movement_setup.v_player_collider_sampler_shoot_movement_speed);
                }
            }
        }
        else if (v_player_collider_sampler_player_collider_gameobject_script.f_player_collider_outside_reference_dodge_key())
        {
            tv_normal_movement_check = true;
            v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_detected_target = Vector3.zero;
            if (transform.localPosition.Equals(Vector3.zero))
            {
                v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_intended_target = Vector3.zero;
            }

            v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_destination_reached = false;
            v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_pathing_detected = false;
        }
        else
        {
            tv_normal_movement_check = true;

            v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_detected_target = Vector3.zero;
            v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_intended_target = Vector3.zero;

            v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_destination_reached = false;
            v_player_collider_sampler_movement_setup.v_player_collider_sampler_dodge_pathing_detected = false;
        }

        if (tv_normal_movement_check)
        {
            if (v_player_collider_sampler_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_speed_setup.v_player_collider_movement_speed_toggle)
            {
                v_player_collider_sampler_movement_setup.v_player_collider_sampler_return_movement_speed = v_player_collider_sampler_movement_setup.v_player_collider_sampler_return_walk_movement_speed;
                v_player_collider_sampler_movement_setup.v_player_collider_sampler_shoot_movement_speed = v_player_collider_sampler_movement_setup.v_player_collider_sampler_shoot_walk_movement_speed;
            }
            else
            {
                v_player_collider_sampler_movement_setup.v_player_collider_sampler_return_movement_speed = v_player_collider_sampler_movement_setup.v_player_collider_sampler_return_run_movement_speed;
                v_player_collider_sampler_movement_setup.v_player_collider_sampler_shoot_movement_speed = v_player_collider_sampler_movement_setup.v_player_collider_sampler_shoot_run_movement_speed;
            }

            if ((!v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_intended_target.Equals(v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_detected_target)) && (transform.localPosition.Equals(Vector3.zero)))
            {
                v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_intended_target = v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_detected_target;
            }

            if (v_player_collider_sampler_pathing_current_collisions_list.Count > 0)
            {
                if (v_player_collider_sampler_movement_setup.v_player_collider_sampler_target_distance < v_player_collider_sampler_movement_setup.v_player_collider_sampler_change_direction_distance_threshold)
                {
                    f_sampler_move_towards(Vector3.zero, v_player_collider_sampler_movement_setup.v_player_collider_sampler_return_movement_speed);
                }
                else
                {
                    v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_intended_target = v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_detected_target;

                    f_sampler_move_towards(v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_intended_target, v_player_collider_sampler_movement_setup.v_player_collider_sampler_shoot_movement_speed);
                }
            }
            else
            {
                if (!v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_intended_target.Equals(v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_detected_target))
                {
                    v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_intended_target = v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_detected_target;
                }

                f_sampler_move_towards(v_player_collider_sampler_movement_setup.v_player_collider_sampler_movement_intended_target, v_player_collider_sampler_movement_setup.v_player_collider_sampler_shoot_movement_speed);
            }
        }
    }

    public void f_sampler_move_towards(Vector3 sv_target_position, float sv_determined_speed)
    {
        if (!transform.localPosition.Equals(sv_target_position))
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, sv_target_position, sv_determined_speed);
        }
    }
}
