using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_sprite_entity_definition_profile
{
    [SerializeField] public GameObject v_profile_right;
    [SerializeField] public GameObject v_profile_left;
}

[Serializable]
public class svl_sprite_entity_definition_orientation
{
    [Header("Front")]
    [SerializeField] public svl_sprite_entity_definition_profile v_front_orientation_setup = new svl_sprite_entity_definition_profile();
    [Header("Back")]
    [SerializeField] public svl_sprite_entity_definition_profile v_back_orientation_setup = new svl_sprite_entity_definition_profile();
}

[Serializable]
public class svl_sprite_entity_definition_element
{
    [SerializeField] public v_tags_sprite_state_list v_state_type;
    [SerializeField] public bool v_state_enabled;
    [SerializeField] public bool v_sprite_profile_flipping;
    [SerializeField] public List<svl_sprite_entity_definition_orientation> v_definition_element_setup = new List<svl_sprite_entity_definition_orientation>();
}

[Serializable]
public class svl_sprite_entity_definition
{
    [Header("Configurable Variables")]
    [SerializeField] public v_tags_sprite_entity_list v_sprite_entity_definition_tag;
    [SerializeField] public GameObject v_sprite_entity_definition_subject;
    [SerializeField] public s_sprite_handler v_sprite_entity_definition_subject_script;
    [SerializeField] public GameObject v_sprite_entity_definition_fallback;
    [SerializeField] public List<svl_sprite_entity_definition_element> v_sprite_entity_state_setup = new List<svl_sprite_entity_definition_element>();
    [SerializeField] public List<v_tags_sprite_state_list> v_sprite_entity_state_index_list = new List<v_tags_sprite_state_list>();
    [Header("Reference Variables")]
    [SerializeField] public s_sprite_handler v_sprite_entity_definition_fallback_script;
}

[Serializable]
public class svl_sprite_entity_state
{
    [Header("Configurable Variables")]
    [SerializeField] public v_tags_sprite_state_list v_sprite_state;
    [SerializeField] public v_tags_sprite_orientation_list v_sprite_state_orientation;
    [SerializeField] public v_tags_sprite_profile_list v_sprite_state_profile;
    [SerializeField] public bool v_sprite_entity_counter_always_reset = false;
    [SerializeField] public bool v_sprite_entity_do_not_null_empty_references = false;
    [Header("Reference Variables")]
    [SerializeField] public v_tags_sprite_state_list v_sprite_state_current;
    [SerializeField] public v_tags_sprite_orientation_list v_sprite_state_orientation_current;
    [SerializeField] public v_tags_sprite_profile_list v_sprite_state_profile_current;
    [SerializeField] public bool v_sprite_entity_change_state_gate;
    [SerializeField] public bool v_sprite_entity_change_state_gate_bypass;
    [SerializeField] public bool v_sprite_entity_counter_reset = false;
}

public class s_sprite_entity_definition : MonoBehaviour
{
    [Header("Sprite Entity Time Handler Setup")]
    [SerializeField] public svgl_time_handler v_sprite_entity_time_handler_setup = new svgl_time_handler();

    [Header("Sprite Entity State Setup")]
    [SerializeField] public svl_sprite_entity_state v_sprite_entity_state_setup = new svl_sprite_entity_state();

    [Header("Sprite Entity Definition Setup")]
    [SerializeField] public svl_sprite_entity_definition v_sprite_entity_definition_setup = new svl_sprite_entity_definition();

    void Start()
    {
        f_sprite_entity_gameobject_finder();
    }

    void Update()
    {
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_time_handler_setup.v_time_handler_level = v_sprite_entity_time_handler_setup.v_time_handler_level;

        v_sprite_entity_state_setup.v_sprite_entity_change_state_gate = false;
        if (!v_sprite_entity_state_setup.v_sprite_state.Equals(v_sprite_entity_state_setup.v_sprite_state_current))
        {
            v_sprite_entity_state_setup.v_sprite_entity_counter_reset = true;
            v_sprite_entity_state_setup.v_sprite_entity_change_state_gate = true;
        }
        else if (!v_sprite_entity_state_setup.v_sprite_state_orientation.Equals(v_sprite_entity_state_setup.v_sprite_state_orientation_current))
        {
            v_sprite_entity_state_setup.v_sprite_entity_counter_reset = false;
            v_sprite_entity_state_setup.v_sprite_entity_change_state_gate = true;
        }
        else if (!v_sprite_entity_state_setup.v_sprite_state_profile.Equals(v_sprite_entity_state_setup.v_sprite_state_profile_current))
        {
            v_sprite_entity_state_setup.v_sprite_entity_counter_reset = false;
            v_sprite_entity_state_setup.v_sprite_entity_change_state_gate = true;
        }
        else
        {
            v_sprite_entity_state_setup.v_sprite_entity_counter_reset = false;
            v_sprite_entity_state_setup.v_sprite_entity_change_state_gate = false;
        }

        if (v_sprite_entity_state_setup.v_sprite_entity_counter_always_reset)
        {
            v_sprite_entity_state_setup.v_sprite_entity_counter_reset = true;
        }

        if (((v_sprite_entity_state_setup.v_sprite_entity_change_state_gate) && (v_sprite_entity_time_handler_setup.v_time_handler_script.f_time_level_gate_get(v_sprite_entity_time_handler_setup.v_time_handler_level))) || (v_sprite_entity_state_setup.v_sprite_entity_change_state_gate_bypass))
        {
            if ((v_sprite_entity_definition_setup.v_sprite_entity_state_setup.Count > 0) && (v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.Count > 0))
            {
                if (v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.Contains(v_sprite_entity_state_setup.v_sprite_state))
                {
                    v_sprite_entity_state_setup.v_sprite_state_current = v_sprite_entity_state_setup.v_sprite_state;
                    v_sprite_entity_state_setup.v_sprite_state_orientation_current = v_sprite_entity_state_setup.v_sprite_state_orientation;
                    v_sprite_entity_state_setup.v_sprite_state_profile_current = v_sprite_entity_state_setup.v_sprite_state_profile;

                    f_sprite_entity_definition_state_determiner(v_sprite_entity_state_setup.v_sprite_state_current);
                }
                else
                {
                    f_sprite_entity_nuller();
                }
            }
            else
            {
                f_sprite_entity_nuller();
            }

            v_sprite_entity_state_setup.v_sprite_entity_change_state_gate = false;
            v_sprite_entity_state_setup.v_sprite_entity_change_state_gate_bypass = false;
        }
    }

    public void f_sprite_entity_gameobject_finder()
    {
        v_sprite_entity_time_handler_setup.v_time_handler_gameobject = GameObject.Find(v_sprite_entity_time_handler_setup.v_time_handler_gameobject_name);
        v_sprite_entity_time_handler_setup.v_time_handler_script = v_sprite_entity_time_handler_setup.v_time_handler_gameobject.GetComponent<s_time_handler>();

        v_sprite_entity_definition_setup.v_sprite_entity_definition_fallback_script = v_sprite_entity_definition_setup.v_sprite_entity_definition_fallback.GetComponent<s_sprite_handler>();
    }

    public void f_sprite_entity_definition_state_determiner(v_tags_sprite_state_list sv_targetState)
    {
        int tv_targetIndex = v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.IndexOf(sv_targetState);
        int tv_targetCount = v_sprite_entity_definition_setup.v_sprite_entity_state_setup[tv_targetIndex].v_definition_element_setup.Count;
        if (tv_targetCount > 0)
        {
            if (v_sprite_entity_definition_setup.v_sprite_entity_state_setup[tv_targetIndex].v_state_enabled)
            {
                if (v_sprite_entity_state_setup.v_sprite_state_orientation_current.Equals(v_tags_sprite_orientation_list.Front))
                {
                    if (v_sprite_entity_state_setup.v_sprite_state_profile_current.Equals(v_tags_sprite_profile_list.Right))
                    {
                        f_sprite_entity_definition_reader(
                            true, v_sprite_entity_state_setup.v_sprite_entity_counter_reset, v_tags_sprite_profile_list.Right, v_sprite_entity_definition_setup.v_sprite_entity_state_setup[tv_targetIndex].v_definition_element_setup[UnityEngine.Random.Range(0, tv_targetCount)].v_front_orientation_setup.v_profile_right.GetComponent<s_sprite_handler>());
                    }
                    else if (v_sprite_entity_state_setup.v_sprite_state_profile_current.Equals(v_tags_sprite_profile_list.Left))
                    {
                        if (v_sprite_entity_definition_setup.v_sprite_entity_state_setup[v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.IndexOf(v_tags_sprite_state_list.Idle)].v_sprite_profile_flipping)
                        {
                            f_sprite_entity_definition_reader(true, v_sprite_entity_state_setup.v_sprite_entity_counter_reset, v_tags_sprite_profile_list.Left, v_sprite_entity_definition_setup.v_sprite_entity_state_setup[v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.IndexOf(sv_targetState)].v_definition_element_setup[UnityEngine.Random.Range(0, tv_targetCount)].v_front_orientation_setup.v_profile_right.GetComponent<s_sprite_handler>());
                        }
                        else
                        {
                            f_sprite_entity_definition_reader(true, v_sprite_entity_state_setup.v_sprite_entity_counter_reset, v_tags_sprite_profile_list.Right, v_sprite_entity_definition_setup.v_sprite_entity_state_setup[v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.IndexOf(sv_targetState)].v_definition_element_setup[UnityEngine.Random.Range(0, tv_targetCount)].v_front_orientation_setup.v_profile_left.GetComponent<s_sprite_handler>());
                        }
                    }
                    else
                    {
                        f_sprite_entity_nuller();
                    }
                }
                else if (v_sprite_entity_state_setup.v_sprite_state_orientation_current.Equals(v_tags_sprite_orientation_list.Back))
                {
                    if (v_sprite_entity_state_setup.v_sprite_state_profile_current.Equals(v_tags_sprite_profile_list.Right))
                    {
                        f_sprite_entity_definition_reader(true, v_sprite_entity_state_setup.v_sprite_entity_counter_reset, v_tags_sprite_profile_list.Right, v_sprite_entity_definition_setup.v_sprite_entity_state_setup[v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.IndexOf(sv_targetState)].v_definition_element_setup[UnityEngine.Random.Range(0, tv_targetCount)].v_back_orientation_setup.v_profile_right.GetComponent<s_sprite_handler>());
                    }
                    else if (v_sprite_entity_state_setup.v_sprite_state_profile_current.Equals(v_tags_sprite_profile_list.Left))
                    {
                        if (v_sprite_entity_definition_setup.v_sprite_entity_state_setup[v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.IndexOf(v_tags_sprite_state_list.Idle)].v_sprite_profile_flipping)
                        {
                            f_sprite_entity_definition_reader(true, v_sprite_entity_state_setup.v_sprite_entity_counter_reset, v_tags_sprite_profile_list.Left, v_sprite_entity_definition_setup.v_sprite_entity_state_setup[v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.IndexOf(sv_targetState)].v_definition_element_setup[UnityEngine.Random.Range(0, tv_targetCount)].v_back_orientation_setup.v_profile_right.GetComponent<s_sprite_handler>());
                        }
                        else
                        {
                            f_sprite_entity_definition_reader(true, v_sprite_entity_state_setup.v_sprite_entity_counter_reset, v_tags_sprite_profile_list.Right, v_sprite_entity_definition_setup.v_sprite_entity_state_setup[v_sprite_entity_definition_setup.v_sprite_entity_state_index_list.IndexOf(sv_targetState)].v_definition_element_setup[UnityEngine.Random.Range(0, tv_targetCount)].v_back_orientation_setup.v_profile_left.GetComponent<s_sprite_handler>());
                        }
                    }
                    else
                    {
                        f_sprite_entity_nuller();
                    }
                }
                else
                {
                    f_sprite_entity_nuller();
                }
            }
            else
            {
                f_sprite_entity_nuller();
            }
        }
        else
        {
            f_sprite_entity_nuller();
        }
    }

    public void f_sprite_entity_definition_reader(bool sv_play, bool sv_reset_counter, v_tags_sprite_profile_list sv_profile, s_sprite_handler sv_target_script)
    {
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_frame_setup.v_frame_play = sv_play;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_frame_setup.v_frame_loops = sv_target_script.v_sprite_frame_setup.v_frame_loops;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_frame_setup.v_frame_profile_inversion = sv_target_script.v_sprite_frame_setup.v_frame_profile_inversion;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_frame_setup.v_frame_scale = sv_target_script.v_sprite_frame_setup.v_frame_scale;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_frame_setup.v_sprite_renderer_material = sv_target_script.v_sprite_frame_setup.v_sprite_renderer_material;

        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_frame_setup.v_frame_list.Clear();
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_frame_setup.v_frame_list.AddRange(sv_target_script.v_sprite_frame_setup.v_frame_list);

        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_frame_setup.v_frame_sprite_profile = sv_profile;

        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_alpha_setup.v_sprite_alpha_global = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_global;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_alpha_setup.v_sprite_alpha_update_on_timer = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_update_on_timer;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_alpha_setup.v_sprite_alpha_target = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_target;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_alpha_setup.v_sprite_alpha_target_max = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_target_max;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_alpha_setup.v_sprite_alpha_target_min = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_target_min;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_alpha_setup.v_sprite_alpha_increment = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_increment;

        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_enabled = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_enabled;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_global = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_global;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_auto_change_direction = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_auto_change_direction;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_rely_on_frame_change_for_direction_change = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_rely_on_frame_change_for_direction_change;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_distance_threshold = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_distance_threshold;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_distance = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_distance;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance_max = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance_max;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_origin = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_origin;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin_max = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin_max;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_smoothness = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_smoothness;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness_max = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness_max;

        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_index_setup.Clear();
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_index_setup.AddRange(sv_target_script.v_sprite_index_setup);

        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_sort_setup.v_sort_modifier = sv_target_script.v_sprite_sort_setup.v_sort_modifier;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_sort_setup.v_enable_parent = sv_target_script.v_sprite_sort_setup.v_enable_parent;
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_sort_setup.v_enable_parent_root = sv_target_script.v_sprite_sort_setup.v_enable_parent_root;

        if (sv_reset_counter)
        {
            v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.v_sprite_frame_setup.v_frame_counter = 0;
            v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.f_sprite_index_element_counter_parameters_reset();
        }
    }

    public void f_sprite_entity_nuller()
    {
        if (!v_sprite_entity_state_setup.v_sprite_entity_do_not_null_empty_references)
        {
            f_sprite_entity_definition_reader(false, true, v_tags_sprite_profile_list.Right, v_sprite_entity_definition_setup.v_sprite_entity_definition_fallback_script);
        }
    }

    public void f_sprite_manually_skip_to_last_frame()
    {
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.f_sprite_skip_to_last_frame();
    }

    public void f_sprite_manually_rewind_to_first_frame()
    {
        v_sprite_entity_definition_setup.v_sprite_entity_definition_subject_script.f_sprite_rewind_to_first_frame();
    }
}
