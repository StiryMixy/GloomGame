using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_player_movement
{
    [Header("Configurable Variables")]
    [SerializeField] public GameObject v_player_collider_object;
    [SerializeField] public s_player_collider_controller v_player_collider_object_script;
    [SerializeField] public float v_player_collider_object_distance_accuracy_threshold;
    [SerializeField] public float v_player_collider_object_distance_position_threshold;
    [SerializeField] public float v_player_collider_object_distance_sprite_threshold;
    [SerializeField] public float v_player_collider_object_distance_speed_threshold;
    [SerializeField] public float v_player_collider_object_follow_speed_multiplier;
    [Header("Reference Variables")]
    [SerializeField] public List<KeyCode> v_player_movement_key_list;
    [SerializeField] public bool v_player_movement_key_index_list_direction_gate = false;
    [SerializeField] public bool v_player_movement_gate = false;
    [SerializeField] public bool v_player_position_update_gate = false;
    [SerializeField] public int v_player_movement_key_index = 0;
    [SerializeField] public float v_player_collider_object_distance;
}

[Serializable]
public class svl_player_sprite
{
    [Header("Configurable Variables")]
    [SerializeField] public bool v_sprite_enabled;
    [SerializeField] public v_tags_sprite_entity_list v_sprite_entity = v_tags_sprite_entity_list.entity_null;
    [SerializeField] public v_tags_sprite_state_list v_sprite_state;
    [SerializeField] public v_tags_sprite_orientation_list v_sprite_state_orientation;
    [SerializeField] public v_tags_sprite_profile_list v_sprite_state_profile;
    [SerializeField] public GameObject v_player_sprite_caller_object;
    [SerializeField] public s_sprite_entity_definition v_player_sprite_caller_object_script;
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_sprite_target_object;
    [SerializeField] public GameObject v_sprite_target_object_gate;
    [SerializeField] public s_sprite_entity_definition v_sprite_target_object_script;
}

[Serializable]
public class svl_player_sight_collider
{
    [Header("Configurable Variables")]
    [SerializeField] public GameObject v_player_sight_collider_object;
    [Header("Reference Variables")]
    [SerializeField] public v_tags_entity_direction_list v_player_sight_collider_object_last_direction;
    [SerializeField] public bool v_player_sight_collider_direction_changed = false;
}

public class s_player_handler : MonoBehaviour
{
    [Header("Player Time Handler Setup")]
    [SerializeField] public svgl_time_handler v_player_time_handler_setup = new svgl_time_handler();
    [Header("Player Tag Storage Setup")]
    [SerializeField] public sgvl_tag_storage v_player_tag_storage_setup = new sgvl_tag_storage();
    [Header("Player Movement Key Manager Object Setup")]
    [SerializeField] public svgl_key_manager v_player_movement_key_manager_gameobject_setup = new svgl_key_manager();
    [Header("Player Movement Setup")]
    [SerializeField] public svl_player_movement v_player_movement_setup = new svl_player_movement();
    [Header("Player Sprite Setup")]
    [SerializeField] public svl_player_sprite v_player_sprite_setup = new svl_player_sprite();
    [Header("Player Sight Collider Setup")]
    [SerializeField] public svl_player_sight_collider v_player_sight_collider_setup = new svl_player_sight_collider();
    [Header("Player Debug Setup")]
    [SerializeField] public sgvl_debug_full_controller v_player_debug_render_setup = new sgvl_debug_full_controller();

    void Start()
    {
        f_player_gameobject_finder();
    }

    void Update()
    {
        v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_time_handler_setup.v_time_handler_level = v_player_time_handler_setup.v_time_handler_level;
        v_player_movement_setup.v_player_collider_object_script.v_player_collider_time_handler_setup.v_time_handler_level = v_player_time_handler_setup.v_time_handler_level;
        f_player_sprite_handler();
        f_player_movement_module();
        f_player_sight_collider_handler();
        f_player_sprite_result_reader();
        v_player_debug_render_setup.v_debug_manager_gameobject_script.f_debug_renderer_controller(v_player_debug_render_setup.v_debug_gameobjects_list);
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        
    }

    private void OnTriggerStay(Collider sv_other_object)
    {

    }

    private void OnTriggerExit(Collider sv_other_object)
    {
        
    }

    public void f_player_gameobject_finder()
    {
        v_player_time_handler_setup.v_time_handler_gameobject = GameObject.Find(v_player_time_handler_setup.v_time_handler_gameobject_name);
        v_player_time_handler_setup.v_time_handler_script = v_player_time_handler_setup.v_time_handler_gameobject.GetComponent<s_time_handler>();

        v_player_tag_storage_setup.v_tag_storage_gameobject = GameObject.Find(v_player_tag_storage_setup.v_tag_storage_gameobject_name);
        v_player_tag_storage_setup.v_tag_storage_gameobject_script = v_player_tag_storage_setup.v_tag_storage_gameobject.GetComponent<s_tag_storage>();

        v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject = GameObject.Find(v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject_name);
        v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script = v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject.GetComponent<s_key_manager>();

        v_player_debug_render_setup.v_debug_manager_gameobject = GameObject.Find(v_player_debug_render_setup.v_debug_manager_gameobject_name);
        v_player_debug_render_setup.v_debug_manager_gameobject_script = v_player_debug_render_setup.v_debug_manager_gameobject.GetComponent<s_debug_controller>();
    }

    public void f_player_movement_module()
    {
        f_player_movement_command_verify(v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Forward);
        f_player_movement_command_verify(v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Backward);
        f_player_movement_command_verify(v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Left);
        f_player_movement_command_verify(v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Right);

        if (v_player_movement_setup.v_player_collider_object_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_detected)
        {
            v_player_movement_setup.v_player_movement_key_list.Clear();
        }
        if (v_player_movement_setup.v_player_collider_object_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_cooldown_counter > 0)
        {
            v_player_movement_setup.v_player_movement_key_list.Clear();
        }

        if (v_player_movement_setup.v_player_movement_key_list.Count > 0)
        {
            if (v_player_movement_setup.v_player_movement_key_index >= v_player_movement_setup.v_player_movement_key_list.Count)
            {
                v_player_movement_setup.v_player_movement_key_index = v_player_movement_setup.v_player_movement_key_list.Count - 1;
            }
            else if (v_player_movement_setup.v_player_movement_key_index < 0)
            {
                v_player_movement_setup.v_player_movement_key_index = 0;
            }

            int tv_targetIndex = v_player_movement_setup.v_player_movement_key_index;
            if (v_player_movement_setup.v_player_movement_key_list[tv_targetIndex] == v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Forward)
            {
                if (v_player_sprite_setup.v_sprite_enabled)
                {
                    v_player_sprite_setup.v_sprite_state_orientation = v_tags_sprite_orientation_list.Back;
                }
            }
            else if (v_player_movement_setup.v_player_movement_key_list[tv_targetIndex] == v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Left)
            {
                if (v_player_sprite_setup.v_sprite_enabled)
                {
                    v_player_sprite_setup.v_sprite_state_profile = v_tags_sprite_profile_list.Left;
                }
            }
            else if (v_player_movement_setup.v_player_movement_key_list[tv_targetIndex] == v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Backward)
            {
                if (v_player_sprite_setup.v_sprite_enabled)
                {
                    v_player_sprite_setup.v_sprite_state_orientation = v_tags_sprite_orientation_list.Front;
                }
            }
            else if (v_player_movement_setup.v_player_movement_key_list[tv_targetIndex] == v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Right)
            {
                if (v_player_sprite_setup.v_sprite_enabled)
                {
                    v_player_sprite_setup.v_sprite_state_profile = v_tags_sprite_profile_list.Right;
                }
            }

            if (v_player_movement_setup.v_player_collider_object_script.v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_pathing_current_collisions_list.Count == 0)
            {
                if (v_player_movement_setup.v_player_movement_key_index_list_direction_gate)
                {
                    if ((v_player_movement_setup.v_player_movement_key_index - 1) >= 0)
                    {
                        v_player_movement_setup.v_player_movement_key_index -= 1;
                    }
                }
                else
                {
                    if ((v_player_movement_setup.v_player_movement_key_index + 1) < v_player_movement_setup.v_player_movement_key_list.Count)
                    {
                        v_player_movement_setup.v_player_movement_key_index += 1;
                    }
                }

                if (v_player_movement_setup.v_player_movement_key_index >= (v_player_movement_setup.v_player_movement_key_list.Count - 1))
                {
                    v_player_movement_setup.v_player_movement_key_index_list_direction_gate = true;
                }
                else if (v_player_movement_setup.v_player_movement_key_index <= 0)
                {
                    v_player_movement_setup.v_player_movement_key_index_list_direction_gate = false;
                }
            }
            else
            {
                if ((v_player_movement_setup.v_player_movement_key_index - 1) >= 0)
                {
                    v_player_movement_setup.v_player_movement_key_index -= 1;
                }
            }
        }
        else
        {
            v_player_movement_setup.v_player_movement_key_index = 0;
            v_player_movement_setup.v_player_movement_key_index_list_direction_gate = false;
        }

        v_player_movement_setup.v_player_collider_object_distance = Vector3.Distance(transform.position, v_player_movement_setup.v_player_collider_object.transform.position);
        if (v_player_movement_setup.v_player_collider_object_distance > v_player_movement_setup.v_player_collider_object_distance_position_threshold)
        {
            v_player_movement_setup.v_player_position_update_gate = true;
        }
        if (v_player_movement_setup.v_player_position_update_gate)
        {
            v_player_movement_setup.v_player_position_update_gate = f_player_move_towards(v_player_movement_setup.v_player_collider_object.transform.position, v_player_time_handler_setup.v_time_handler_script.f_time_level_rate_get(v_player_time_handler_setup.v_time_handler_level));
        }

        if (v_player_sprite_setup.v_sprite_enabled)
        {
            if (v_player_movement_setup.v_player_collider_object_distance > v_player_movement_setup.v_player_collider_object_distance_sprite_threshold)
            {
                if (v_player_movement_setup.v_player_collider_object_distance > v_player_movement_setup.v_player_collider_object_distance_speed_threshold)
                {
                    if (v_player_movement_setup.v_player_collider_object_script.v_player_collider_movement_setup.v_player_collider_movement_speed_setup.v_player_collider_movement_speed_is_walking)
                    {
                        v_player_sprite_setup.v_sprite_state = v_tags_sprite_state_list.Walk;
                    }
                    else
                    {
                        v_player_sprite_setup.v_sprite_state = v_tags_sprite_state_list.Run;
                    }
                }
                else
                {
                    v_player_sprite_setup.v_sprite_state = v_tags_sprite_state_list.Walk;
                }
            }

            if ((v_player_movement_setup.v_player_collider_object_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_detected) && (v_player_movement_setup.v_player_collider_object_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_detected_target.v_player_collider_movement_target_pathing))
            {
                v_player_sprite_setup.v_sprite_state = v_tags_sprite_state_list.Dodge;
                v_player_sprite_setup.v_player_sprite_caller_object_script.f_sprite_manually_rewind_to_first_frame();
                v_player_sight_collider_setup.v_player_sight_collider_direction_changed = true;
            }
            else if ((!v_player_movement_setup.v_player_collider_object_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_detected) && (v_player_movement_setup.v_player_collider_object_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_cooldown_counter > 0))
            {
                v_player_sprite_setup.v_sprite_state = v_tags_sprite_state_list.Dodge;
                v_player_sprite_setup.v_player_sprite_caller_object_script.f_sprite_manually_skip_to_last_frame();
            }
        }
    }

    public void f_player_movement_command_verify(KeyCode sv_key)
    {
        if (Input.GetKey(sv_key))
        {
            if (v_player_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.v_player_movement_enabled)
            {
                if (!v_player_movement_setup.v_player_movement_key_list.Contains(sv_key))
                {
                    v_player_movement_setup.v_player_movement_key_list.Insert(0, sv_key);
                }
            }
        }
        else
        {
            if (v_player_movement_setup.v_player_movement_key_list.Contains(sv_key))
            {
                v_player_movement_setup.v_player_movement_key_list.Remove(sv_key);
            }
        }
    }

    public bool f_player_move_towards(Vector3 sv_target_position, float sv_timer_rate)
    {
        float tv_determined_speed = (float)(v_player_movement_setup.v_player_collider_object_script.v_player_collider_movement_setup.v_player_collider_movement_target_speed * 0.00001);
        tv_determined_speed *= v_player_movement_setup.v_player_collider_object_follow_speed_multiplier;
        if (v_player_movement_setup.v_player_collider_object_distance > v_player_movement_setup.v_player_collider_object_distance_accuracy_threshold)
        {
            transform.position = Vector3.Lerp(transform.position, sv_target_position, tv_determined_speed * sv_timer_rate);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void f_player_sprite_handler()
    {
        if (v_player_sprite_setup.v_sprite_enabled)
        {
            v_player_sprite_setup.v_sprite_target_object_gate = v_player_tag_storage_setup.v_tag_storage_gameobject_script.v_sprite_entity_list_object_setup[v_player_tag_storage_setup.v_tag_storage_gameobject_script.v_sprite_entity_list_index_setup.IndexOf(v_player_sprite_setup.v_sprite_entity)];
            if (v_player_sprite_setup.v_sprite_target_object_gate != v_player_sprite_setup.v_sprite_target_object)
            {
                v_player_sprite_setup.v_sprite_target_object = v_player_sprite_setup.v_sprite_target_object_gate;
                v_player_sprite_setup.v_sprite_target_object_script = v_player_sprite_setup.v_sprite_target_object.GetComponent<s_sprite_entity_definition>();

                v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_state_setup.v_sprite_entity_counter_always_reset = v_player_sprite_setup.v_sprite_target_object_script.v_sprite_entity_state_setup.v_sprite_entity_counter_always_reset;
                v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_state_setup.v_sprite_entity_do_not_null_empty_references = v_player_sprite_setup.v_sprite_target_object_script.v_sprite_entity_state_setup.v_sprite_entity_do_not_null_empty_references;
                v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_state_setup.v_sprite_entity_change_state_gate_bypass = true;

                v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_definition_setup.v_sprite_entity_definition_tag = v_player_sprite_setup.v_sprite_target_object_script.v_sprite_entity_definition_setup.v_sprite_entity_definition_tag;

                v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_definition_setup.v_sprite_entity_state_setup.Clear();
                v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_definition_setup.v_sprite_entity_state_setup.AddRange(v_player_sprite_setup.v_sprite_target_object_script.v_sprite_entity_definition_setup.v_sprite_entity_state_setup);

                v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.Clear();
                v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.AddRange(v_player_sprite_setup.v_sprite_target_object_script.v_sprite_entity_definition_setup.v_sprite_entity_state_index_list);

                v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_state_shadow_setup.v_sprite_entity_shadow_enabled = v_player_sprite_setup.v_sprite_target_object_script.v_sprite_entity_state_shadow_setup.v_sprite_entity_shadow_enabled;
                v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_state_shadow_setup.v_sprite_entity_shadow_is_white = v_player_sprite_setup.v_sprite_target_object_script.v_sprite_entity_state_shadow_setup.v_sprite_entity_shadow_is_white;
                v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_state_shadow_setup.v_sprite_entity_shadow_scale = v_player_sprite_setup.v_sprite_target_object_script.v_sprite_entity_state_shadow_setup.v_sprite_entity_shadow_scale;
            }

            v_player_sprite_setup.v_sprite_state = v_tags_sprite_state_list.Idle;
        }
    }

    public void f_player_sprite_result_reader()
    {
        if (v_player_sprite_setup.v_sprite_enabled)
        {
            v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_state_setup.v_sprite_state = v_player_sprite_setup.v_sprite_state;
            v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_state_setup.v_sprite_state_orientation = v_player_sprite_setup.v_sprite_state_orientation;
            v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_state_setup.v_sprite_state_profile = v_player_sprite_setup.v_sprite_state_profile;
        }
    }

    public void f_player_sight_collider_handler()
    {
        if (v_player_movement_setup.v_player_collider_object_script.v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_cooldown_counter > 0)
        {
            if (v_player_sight_collider_setup.v_player_sight_collider_direction_changed)
            {
                if (v_player_sight_collider_setup.v_player_sight_collider_object_last_direction.Equals(v_tags_entity_direction_list.Right))
                {
                    v_player_sight_collider_setup.v_player_sight_collider_object_last_direction = v_tags_entity_direction_list.Backward;
                }
                else if (v_player_sight_collider_setup.v_player_sight_collider_object_last_direction.Equals(v_tags_entity_direction_list.Backward))
                {
                    v_player_sight_collider_setup.v_player_sight_collider_object_last_direction = v_tags_entity_direction_list.Right;
                }
                else if (v_player_sight_collider_setup.v_player_sight_collider_object_last_direction.Equals(v_tags_entity_direction_list.Left))
                {
                    v_player_sight_collider_setup.v_player_sight_collider_object_last_direction = v_tags_entity_direction_list.Forward;
                }
                else if (v_player_sight_collider_setup.v_player_sight_collider_object_last_direction.Equals(v_tags_entity_direction_list.Forward))
                {
                    v_player_sight_collider_setup.v_player_sight_collider_object_last_direction = v_tags_entity_direction_list.Left;
                }

                v_player_sight_collider_setup.v_player_sight_collider_direction_changed = false;
            }
        }
        else
        {
            if ((v_player_sprite_setup.v_sprite_state_orientation.Equals(v_tags_sprite_orientation_list.Front)) && (v_player_sprite_setup.v_sprite_state_profile.Equals(v_tags_sprite_profile_list.Right)))
            {
                v_player_sight_collider_setup.v_player_sight_collider_object_last_direction = v_tags_entity_direction_list.Right;
            }
            else if ((v_player_sprite_setup.v_sprite_state_orientation.Equals(v_tags_sprite_orientation_list.Front)) && (v_player_sprite_setup.v_sprite_state_profile.Equals(v_tags_sprite_profile_list.Left)))
            {
                v_player_sight_collider_setup.v_player_sight_collider_object_last_direction = v_tags_entity_direction_list.Backward;
            }
            else if ((v_player_sprite_setup.v_sprite_state_orientation.Equals(v_tags_sprite_orientation_list.Back)) && (v_player_sprite_setup.v_sprite_state_profile.Equals(v_tags_sprite_profile_list.Left)))
            {
                v_player_sight_collider_setup.v_player_sight_collider_object_last_direction = v_tags_entity_direction_list.Left;
            }
            else if ((v_player_sprite_setup.v_sprite_state_orientation.Equals(v_tags_sprite_orientation_list.Back)) && (v_player_sprite_setup.v_sprite_state_profile.Equals(v_tags_sprite_profile_list.Right)))
            {
                v_player_sight_collider_setup.v_player_sight_collider_object_last_direction = v_tags_entity_direction_list.Forward;
            }
        }

        if (v_player_sight_collider_setup.v_player_sight_collider_object_last_direction.Equals(v_tags_entity_direction_list.Right))
        {
            v_player_sight_collider_setup.v_player_sight_collider_object.transform.localEulerAngles = Vector3.zero;
        }
        else if (v_player_sight_collider_setup.v_player_sight_collider_object_last_direction.Equals(v_tags_entity_direction_list.Backward))
        {
            v_player_sight_collider_setup.v_player_sight_collider_object.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (v_player_sight_collider_setup.v_player_sight_collider_object_last_direction.Equals(v_tags_entity_direction_list.Left))
        {
            v_player_sight_collider_setup.v_player_sight_collider_object.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (v_player_sight_collider_setup.v_player_sight_collider_object_last_direction.Equals(v_tags_entity_direction_list.Forward))
        {
            v_player_sight_collider_setup.v_player_sight_collider_object.transform.localEulerAngles = new Vector3(0, 270, 0);
        }
    }

    public void f_scene_reset_action()
    {
        transform.position = Vector3.zero;
        v_player_sprite_setup.v_player_sprite_caller_object_script.f_sprite_manual_counter_reset();

        v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_alpha_setup.v_sprite_alpha_global = true;
        v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_alpha_setup.v_sprite_alpha_update_on_timer = false;
        v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_alpha_setup.v_sprite_alpha_target = 0.0f;
        v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_alpha_setup.v_sprite_alpha = 0.0f;

        v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_state_shadow_setup.v_sprite_entity_shadow_script.v_sprite_alpha_setup.v_sprite_alpha_target = 0.0f;
        v_player_sprite_setup.v_player_sprite_caller_object_script.v_sprite_entity_state_shadow_setup.v_sprite_entity_shadow_script.v_sprite_alpha_setup.v_sprite_alpha = 0.0f;
    }
}
