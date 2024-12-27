using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_player_collider_movement_target
{
    [SerializeField] public GameObject v_player_collider_movement_target_pathing;
    [SerializeField] public s_pathing v_player_collider_movement_target_pathing_script;
    [SerializeField] public float v_player_collider_movement_target_distance;
    [SerializeField] public float v_player_collider_dodge_target_distance;
}

[Serializable]
public class svl_player_collider_movement_speed
{
    [SerializeField] public bool v_player_collider_movement_speed_toggle;
    [SerializeField] public float v_player_collider_movement_walking_speed;
    [SerializeField] public float v_player_collider_movement_running_speed;
    [SerializeField] public float v_player_collider_movement_dodging_speed;
}

[Serializable]
public class svl_player_collider_movement
{
    [Header("Configurable Variables")]
    [SerializeField] public GameObject v_player_collider_movement_default_sampler_gameobject;
    [SerializeField] public s_player_collider_default_sampler v_player_collider_movement_default_sampler_gameobject_script;
    [SerializeField] public GameObject v_player_collider_movement_dodge_sampler_gameobject;
    [SerializeField] public s_player_collider_dodge_sampler v_player_collider_movement_dodge_sampler_gameobject_script;
    [Space(10)]
    [SerializeField] public v_tags_movement_mode_list v_player_collider_movement_mode;
    [SerializeField] public float v_player_collider_movement_step;
    [SerializeField] public float v_player_collider_movement_distance_threshold;
    [SerializeField] public float v_player_collider_dodge_step;
    [SerializeField] public float v_player_collider_dodge_distance_threshold;
    [SerializeField] public float v_player_collider_dodge_cooldown;
    [SerializeField] public svl_player_collider_movement_speed v_player_collider_movement_speed_setup = new svl_player_collider_movement_speed();
    [Header("Reference Variables")]
    [SerializeField] public svl_player_collider_movement_target v_player_collider_movement_detected_target = new svl_player_collider_movement_target();
    [Space(10)]
    [SerializeField] public List<KeyCode> v_player_collider_movement_key_list;
    [SerializeField] public bool v_player_collider_movement_key_index_list_direction_gate = false;
    [SerializeField] public bool v_player_collider_movement_gate = false;
    [SerializeField] public int v_player_collider_movement_key_index = 0;
    [Space(10)]
    [SerializeField] public float v_player_collider_movement_speed;
    [SerializeField] public v_tags_entity_direction_list v_player_collider_last_direction = v_tags_entity_direction_list.Backward;
    [Space(10)]
    [SerializeField] public bool v_player_collider_movement_dodge_detected = false;
    [SerializeField] public float v_player_collider_movement_dodge_cooldown_counter;
}

public class s_player_collider_controller : MonoBehaviour
{
    [Header("Player Collider Time Handler Setup")]
    [SerializeField] public svgl_time_handler v_player_collider_time_handler_setup = new svgl_time_handler();
    [Header("Player Collider Tag Storage Setup")]
    [SerializeField] public sgvl_tag_storage v_player_collider_tag_storage_setup = new sgvl_tag_storage();
    [Header("Player Collider Movement Key Manager Object Setup")]
    [SerializeField] public svgl_key_manager v_player_collider_movement_key_manager_gameobject_setup = new svgl_key_manager();
    [Header("Player Collider Movement Setup")]
    [SerializeField] public svl_player_collider_movement v_player_collider_movement_setup = new svl_player_collider_movement();
    [Header("Player Collider Debug Setup")]
    [SerializeField] public sgvl_debug_controller v_player_collider_debug_render_setup = new sgvl_debug_controller();

    void Start()
    {
        f_player_collider_gameobject_finder();
    }

    void Update()
    {
        f_player_collider_movement_module();
        f_player_collider_debug_renderer_controller(v_player_collider_debug_render_setup.v_debug_manager_gameobject_script.v_debug_renderers_enabled);
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

    public void f_player_collider_gameobject_finder()
    {
        v_player_collider_time_handler_setup.v_time_handler_gameobject = GameObject.Find(v_player_collider_time_handler_setup.v_time_handler_gameobject_name);
        v_player_collider_time_handler_setup.v_time_handler_script = v_player_collider_time_handler_setup.v_time_handler_gameobject.GetComponent<s_time_handler>();

        v_player_collider_tag_storage_setup.v_tag_storage_gameobject = GameObject.Find(v_player_collider_tag_storage_setup.v_tag_storage_gameobject_name);
        v_player_collider_tag_storage_setup.v_tag_storage_gameobject_script = v_player_collider_tag_storage_setup.v_tag_storage_gameobject.GetComponent<s_tag_storage>();

        v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject = GameObject.Find(v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_name);
        v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script = v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject.GetComponent<s_key_manager>();

        v_player_collider_debug_render_setup.v_debug_manager_gameobject = GameObject.Find(v_player_collider_debug_render_setup.v_debug_manager_gameobject_name);
        v_player_collider_debug_render_setup.v_debug_manager_gameobject_script = v_player_collider_debug_render_setup.v_debug_manager_gameobject.GetComponent<s_debug_controller>();
    }

    public void f_player_collider_movement_module()
    {
        f_player_collider_movement_command_verify(v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Forward);
        f_player_collider_movement_command_verify(v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Backward);
        f_player_collider_movement_command_verify(v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Left);
        f_player_collider_movement_command_verify(v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Right);

        if (v_player_collider_time_handler_setup.v_time_handler_script.f_time_level_gate_get(v_player_collider_time_handler_setup.v_time_handler_level))
        {
            if (v_player_collider_movement_setup.v_player_collider_movement_dodge_cooldown_counter > 0)
            {
                v_player_collider_movement_setup.v_player_collider_movement_dodge_detected = false;
                v_player_collider_movement_setup.v_player_collider_movement_dodge_cooldown_counter -= 1;
            }
        }

        if (f_player_collider_keyup_verify(v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Dodge))
        {
            if (v_player_collider_movement_setup.v_player_collider_movement_dodge_cooldown_counter <= 0)
            {
                if (v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_gameobject_script.f_sampler_distance_check(0))
                {
                    if (v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_dodge_target_distance <= 0)
                    {
                        v_player_collider_movement_setup.v_player_collider_movement_dodge_detected = true;
                        v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_gameobject_script.v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_embed_detected_pathing = true;

                        if (v_player_collider_movement_setup.v_player_collider_last_direction.Equals(v_tags_entity_direction_list.Forward))
                        {
                            v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_gameobject_script.v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_detected_target = new Vector3(0, 0, 0 + -v_player_collider_movement_setup.v_player_collider_dodge_step);
                        }
                        else if (v_player_collider_movement_setup.v_player_collider_last_direction.Equals(v_tags_entity_direction_list.Left))
                        {
                            v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_gameobject_script.v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_detected_target = new Vector3(0 + v_player_collider_movement_setup.v_player_collider_dodge_step, 0, 0);
                        }
                        else if (v_player_collider_movement_setup.v_player_collider_last_direction.Equals(v_tags_entity_direction_list.Backward))
                        {
                            v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_gameobject_script.v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_detected_target = new Vector3(0, 0, 0 + v_player_collider_movement_setup.v_player_collider_dodge_step);
                        }
                        else if (v_player_collider_movement_setup.v_player_collider_last_direction.Equals(v_tags_entity_direction_list.Right))
                        {
                            v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_gameobject_script.v_player_collider_dodge_sampler_movement_setup.v_player_collider_dodge_sampler_movement_detected_target = new Vector3(0 + -v_player_collider_movement_setup.v_player_collider_dodge_step, 0, 0);
                        }
                    }
                }
            }
        }

        f_player_collider_movement_speed_handler();

        if (f_player_collider_key_verify(v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Dodge))
        {
            v_player_collider_movement_setup.v_player_collider_movement_key_list.Clear();
        }

        if (v_player_collider_movement_setup.v_player_collider_movement_dodge_cooldown_counter > 0)
        {
            v_player_collider_movement_setup.v_player_collider_movement_key_list.Clear();
        }

        if (v_player_collider_movement_setup.v_player_collider_movement_key_list.Count > 0)
        {
            if (v_player_collider_movement_setup.v_player_collider_movement_key_index >= v_player_collider_movement_setup.v_player_collider_movement_key_list.Count)
            {
                v_player_collider_movement_setup.v_player_collider_movement_key_index = v_player_collider_movement_setup.v_player_collider_movement_key_list.Count - 1;
            }
            else if (v_player_collider_movement_setup.v_player_collider_movement_key_index < 0)
            {
                v_player_collider_movement_setup.v_player_collider_movement_key_index = 0;
            }

            int tv_targetIndex = v_player_collider_movement_setup.v_player_collider_movement_key_index;
            if (v_player_collider_movement_setup.v_player_collider_movement_key_list[tv_targetIndex] == v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Forward)
            {
                //v_player_collider_movement_setup.v_player_collider_movement_sampler_gameobject.transform.localPosition = new Vector3(0, 0, 0 + -1);
                v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_movement_setup.v_player_collider_default_sampler_movement_detected_target = new Vector3(0, 0, 0 + -v_player_collider_movement_setup.v_player_collider_movement_step);
                v_player_collider_movement_setup.v_player_collider_last_direction = v_tags_entity_direction_list.Forward;
            }
            else if (v_player_collider_movement_setup.v_player_collider_movement_key_list[tv_targetIndex] == v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Left)
            {
                //v_player_collider_movement_setup.v_player_collider_movement_sampler_gameobject.transform.localPosition = new Vector3(0 + 1, 0, 0);
                v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_movement_setup.v_player_collider_default_sampler_movement_detected_target = new Vector3(0 + v_player_collider_movement_setup.v_player_collider_movement_step, 0, 0);
                v_player_collider_movement_setup.v_player_collider_last_direction = v_tags_entity_direction_list.Left;
            }
            else if (v_player_collider_movement_setup.v_player_collider_movement_key_list[tv_targetIndex] == v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Backward)
            {
                //v_player_collider_movement_setup.v_player_collider_movement_sampler_gameobject.transform.localPosition = new Vector3(0, 0, 0 + 1);
                v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_movement_setup.v_player_collider_default_sampler_movement_detected_target = new Vector3(0, 0, 0 + v_player_collider_movement_setup.v_player_collider_movement_step);
                v_player_collider_movement_setup.v_player_collider_last_direction = v_tags_entity_direction_list.Backward;
            }
            else if (v_player_collider_movement_setup.v_player_collider_movement_key_list[tv_targetIndex] == v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Right)
            {
                //v_player_collider_movement_setup.v_player_collider_movement_sampler_gameobject.transform.localPosition = new Vector3(0 + -1, 0, 0);
                v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_movement_setup.v_player_collider_default_sampler_movement_detected_target = new Vector3(0 + -v_player_collider_movement_setup.v_player_collider_movement_step, 0, 0);
                v_player_collider_movement_setup.v_player_collider_last_direction = v_tags_entity_direction_list.Right;
            }
            else
            {
                //v_player_collider_movement_setup.v_player_collider_movement_sampler_gameobject.transform.localPosition = new Vector3(0, 0, 0);
                v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_movement_setup.v_player_collider_default_sampler_movement_detected_target = new Vector3(0, 0, 0);
            }

            if (v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_pathing_current_collisions_list.Count == 0)
            {
                if (v_player_collider_movement_setup.v_player_collider_movement_key_index_list_direction_gate)
                {
                    if ((v_player_collider_movement_setup.v_player_collider_movement_key_index - 1) >= 0)
                    {
                        v_player_collider_movement_setup.v_player_collider_movement_key_index -= 1;
                    }
                }
                else
                {
                    if ((v_player_collider_movement_setup.v_player_collider_movement_key_index + 1) < v_player_collider_movement_setup.v_player_collider_movement_key_list.Count)
                    {
                        v_player_collider_movement_setup.v_player_collider_movement_key_index += 1;
                    }
                }

                if (v_player_collider_movement_setup.v_player_collider_movement_key_index >= (v_player_collider_movement_setup.v_player_collider_movement_key_list.Count - 1))
                {
                    v_player_collider_movement_setup.v_player_collider_movement_key_index_list_direction_gate = true;
                }
                else if (v_player_collider_movement_setup.v_player_collider_movement_key_index <= 0)
                {
                    v_player_collider_movement_setup.v_player_collider_movement_key_index_list_direction_gate = false;
                }
            }
            else
            {
                if ((v_player_collider_movement_setup.v_player_collider_movement_key_index - 1) >= 0)
                {
                    v_player_collider_movement_setup.v_player_collider_movement_key_index -= 1;
                }
            }
        }
        else
        {
            v_player_collider_movement_setup.v_player_collider_movement_key_index = 0;
            v_player_collider_movement_setup.v_player_collider_movement_key_index_list_direction_gate = false;
            v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject.transform.localPosition = new Vector3(0, 0, 0);
        }

        if (!v_player_collider_movement_setup.v_player_collider_movement_dodge_detected)
        {
            if ((v_player_collider_movement_setup.v_player_collider_movement_key_list.Count > 0) && (v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_pathing_current_collisions_list.Count > 0))
            {
                v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing = v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_pathing_current_collisions_list[0];
                v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing_script = v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing.GetComponent<s_pathing>();
            }

            if (v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing)
            {
                v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_distance = Vector3.Distance(transform.position, v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing.transform.position);

                f_player_collider_move_towards(v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing.transform.position, v_player_collider_time_handler_setup.v_time_handler_script.f_time_level_rate_get(v_player_collider_time_handler_setup.v_time_handler_level));

                if (!v_player_collider_movement_setup.v_player_collider_movement_dodge_detected)
                {
                    if (v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_distance <= v_player_collider_movement_setup.v_player_collider_movement_distance_threshold)
                    {
                        v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_pathing_current_collisions_list.Remove(v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing);
                    }
                }

                if (v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_distance <= 0)
                {
                    v_player_collider_movement_setup.v_player_collider_movement_dodge_detected = false;
                    v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing = null;
                    v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing_script = null;
                    v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_distance = 0;
                    v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_dodge_target_distance = 0;

                    v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_movement_setup.v_player_collider_default_sampler_movement_detected_target = Vector3.zero;
                    v_player_collider_movement_setup.v_player_collider_movement_default_sampler_gameobject_script.v_player_collider_default_sampler_movement_setup.v_player_collider_default_sampler_movement_intended_target = Vector3.zero;
                }
            }
        }
        else
        {
            if (v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing)
            {
                v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_dodge_target_distance = Vector3.Distance(transform.position, v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing.transform.position);

                f_player_collider_move_towards(v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing.transform.position, v_player_collider_time_handler_setup.v_time_handler_script.f_time_level_rate_get(v_player_collider_time_handler_setup.v_time_handler_level));

                if (v_player_collider_movement_setup.v_player_collider_movement_dodge_sampler_gameobject_script.f_sampler_distance_check(v_player_collider_movement_setup.v_player_collider_dodge_distance_threshold)) 
                {
                    if (v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_dodge_target_distance <= 0)
                    {
                        v_player_collider_movement_setup.v_player_collider_movement_dodge_detected = false;
                        v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing = null;
                        v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_movement_target_pathing_script = null;
                        v_player_collider_movement_setup.v_player_collider_movement_detected_target.v_player_collider_dodge_target_distance = 0;

                        v_player_collider_movement_setup.v_player_collider_movement_dodge_cooldown_counter = v_player_collider_movement_setup.v_player_collider_dodge_cooldown;
                    }
                }
            }
        }
    }

    public void f_player_collider_movement_command_verify(KeyCode sv_key)
    {
        if (Input.GetKey(sv_key))
        {
            if (sv_key == v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Forward)
            {
                v_player_collider_movement_setup.v_player_collider_last_direction = v_tags_entity_direction_list.Forward;
            }
            else if (sv_key == v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Left)
            {
                v_player_collider_movement_setup.v_player_collider_last_direction = v_tags_entity_direction_list.Left;
            }
            else if (sv_key == v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Backward)
            {
                v_player_collider_movement_setup.v_player_collider_last_direction = v_tags_entity_direction_list.Backward;
            }
            else if (sv_key == v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.Right)
            {
                v_player_collider_movement_setup.v_player_collider_last_direction = v_tags_entity_direction_list.Right;
            }

            if (!v_player_collider_movement_setup.v_player_collider_movement_key_list.Contains(sv_key))
            {
                v_player_collider_movement_setup.v_player_collider_movement_key_list.Insert(0, sv_key);
            }
        }
        else
        {
            if (v_player_collider_movement_setup.v_player_collider_movement_key_list.Contains(sv_key))
            {
                v_player_collider_movement_setup.v_player_collider_movement_key_list.Remove(sv_key);
            }
        }
    }

    public void f_player_collider_move_towards(Vector3 sv_target_position, float sv_timer_rate)
    {
        float tv_determined_speed = (float)(v_player_collider_movement_setup.v_player_collider_movement_speed * 0.00001);
        if (Vector3.Distance(transform.position, sv_target_position) > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, sv_target_position, tv_determined_speed * sv_timer_rate);
        }
    }

    public void f_player_collider_movement_speed_handler()
    {
        if (f_player_collider_keyup_verify(v_player_collider_movement_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.ToggleRunWalk))
        {
            v_player_collider_movement_setup.v_player_collider_movement_speed_setup.v_player_collider_movement_speed_toggle = !v_player_collider_movement_setup.v_player_collider_movement_speed_setup.v_player_collider_movement_speed_toggle;
        }

        if (v_player_collider_movement_setup.v_player_collider_movement_dodge_detected)
        {
            v_player_collider_movement_setup.v_player_collider_movement_speed = v_player_collider_movement_setup.v_player_collider_movement_speed_setup.v_player_collider_movement_dodging_speed;
        }
        else
        {
            if (v_player_collider_movement_setup.v_player_collider_movement_speed_setup.v_player_collider_movement_speed_toggle)
            {
                v_player_collider_movement_setup.v_player_collider_movement_speed = v_player_collider_movement_setup.v_player_collider_movement_speed_setup.v_player_collider_movement_walking_speed;
            }
            else
            {
                v_player_collider_movement_setup.v_player_collider_movement_speed = v_player_collider_movement_setup.v_player_collider_movement_speed_setup.v_player_collider_movement_running_speed;
            }
        }
    }

    public bool f_player_collider_key_verify(KeyCode sv_key)
    {
        if (Input.GetKey(sv_key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool f_player_collider_keydown_verify(KeyCode sv_key)
    {
        if (Input.GetKeyDown(sv_key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool f_player_collider_keyup_verify(KeyCode sv_key)
    {
        if (Input.GetKeyUp(sv_key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void f_player_collider_debug_renderer_controller(bool sv_is_allowed)
    {
        foreach (GameObject item in v_player_collider_debug_render_setup.v_debug_camera_gameobjects)
        {
            foreach (Renderer r in item.GetComponentsInChildren<Renderer>())
            {
                r.enabled = sv_is_allowed;
            }
        }
    }
}
