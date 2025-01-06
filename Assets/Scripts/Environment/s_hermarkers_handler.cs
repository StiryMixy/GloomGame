using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_hermarkers_sprite_handler
{
    [Header("Configurable Variables")]
    [SerializeField] public GameObject v_hermarkers_self_sprite_gameobject;
    [SerializeField] public s_sprite_handler v_hermarkers_self_sprite_gameobject_script;
    [Space(10)]
    [SerializeField] public GameObject v_hermarkers_idle_sprite_gameobject;
    [SerializeField] public GameObject v_hermarkers_death_sprite_gameobject;
    [SerializeField] public GameObject v_hermarkers_birth_sprite_gameobject;
    [SerializeField] public int v_hermarkers_state_index;
    [Header("Reference Variables")]
    [SerializeField] public s_sprite_handler v_hermarkers_idle_sprite_gameobject_script;
    [SerializeField] public s_sprite_handler v_hermarkers_death_sprite_gameobject_script;
    [SerializeField] public s_sprite_handler v_hermarkers_birth_sprite_gameobject_script;
    [SerializeField] public bool v_hermarkers_detected_player_entity;
}

[Serializable]
public class svl_hermarkers_collision_handler
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_hermarkers_detected_player_entity_gameobject_name;
    [SerializeField] public float v_hermarkers_detected_player_entity_threshold;
    [Header("Reference Variables")]
    [SerializeField] public bool v_hermarkers_detected_player_entity;
    [SerializeField] public GameObject v_hermarkers_detected_player_entity_gameobject;
}

public class s_hermarkers_handler : MonoBehaviour
{
    [Header("Her Markers Handler Time Handler Setup")]
    [SerializeField] public svgl_time_handler v_hermarkers_time_handler_setup = new svgl_time_handler();

    [Header("Her Markers Sprite Handler Setup")]
    [SerializeField] public svl_hermarkers_sprite_handler v_hermarkers_sprite_handler_setup = new svl_hermarkers_sprite_handler();

    [Header("Her Markers Collision Handler Setup")]
    [SerializeField] public svl_hermarkers_collision_handler v_hermarkers_collision_handler_setup = new svl_hermarkers_collision_handler();

    void Start()
    {
        f_hermarkers_handler_gameobject_finder();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, v_hermarkers_collision_handler_setup.v_hermarkers_detected_player_entity_gameobject.transform.position) <= v_hermarkers_collision_handler_setup.v_hermarkers_detected_player_entity_threshold)
        {
            v_hermarkers_collision_handler_setup.v_hermarkers_detected_player_entity = true;
        }
        else
        {
            v_hermarkers_collision_handler_setup.v_hermarkers_detected_player_entity = false;
        }

        if (v_hermarkers_sprite_handler_setup.v_hermarkers_detected_player_entity != v_hermarkers_collision_handler_setup.v_hermarkers_detected_player_entity)
        {
            v_hermarkers_sprite_handler_setup.v_hermarkers_detected_player_entity = v_hermarkers_collision_handler_setup.v_hermarkers_detected_player_entity;

            if (v_hermarkers_sprite_handler_setup.v_hermarkers_detected_player_entity)
            {
                if (v_hermarkers_sprite_handler_setup.v_hermarkers_state_index == 0)
                {
                    f_hermarkers_entity_definition_reader(true, true, v_tags_sprite_profile_list.Right, v_hermarkers_sprite_handler_setup.v_hermarkers_birth_sprite_gameobject_script);
                    v_hermarkers_sprite_handler_setup.v_hermarkers_state_index = 1;
                }
                else if (v_hermarkers_sprite_handler_setup.v_hermarkers_state_index == 3)
                {
                    f_hermarkers_entity_definition_reader(true, true, v_tags_sprite_profile_list.Right, v_hermarkers_sprite_handler_setup.v_hermarkers_idle_sprite_gameobject_script);
                    v_hermarkers_sprite_handler_setup.v_hermarkers_state_index = 2;
                }
            }
        }

        if (v_hermarkers_sprite_handler_setup.v_hermarkers_state_index == 1)
        {
            if (v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.f_sprite_reached_end_of_frames())
            {
                f_hermarkers_entity_definition_reader(true, true, v_tags_sprite_profile_list.Right, v_hermarkers_sprite_handler_setup.v_hermarkers_idle_sprite_gameobject_script);
                v_hermarkers_sprite_handler_setup.v_hermarkers_state_index = 2;
            }
        }
        if (v_hermarkers_sprite_handler_setup.v_hermarkers_state_index == 2)
        {
            if (!v_hermarkers_sprite_handler_setup.v_hermarkers_detected_player_entity)
            {
                f_hermarkers_entity_definition_reader(true, true, v_tags_sprite_profile_list.Right, v_hermarkers_sprite_handler_setup.v_hermarkers_death_sprite_gameobject_script);
                v_hermarkers_sprite_handler_setup.v_hermarkers_state_index = 3;
            }
        }
        if (v_hermarkers_sprite_handler_setup.v_hermarkers_state_index == 3)
        {
            if (!v_hermarkers_sprite_handler_setup.v_hermarkers_detected_player_entity)
            {
                if (v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.f_sprite_reached_end_of_frames())
                {
                    v_hermarkers_sprite_handler_setup.v_hermarkers_state_index = 0;
                }
            }
        }
    }

    public void f_hermarkers_handler_gameobject_finder()
    {
        v_hermarkers_time_handler_setup.v_time_handler_gameobject = GameObject.Find(v_hermarkers_time_handler_setup.v_time_handler_gameobject_name);
        v_hermarkers_time_handler_setup.v_time_handler_script = v_hermarkers_time_handler_setup.v_time_handler_gameobject.GetComponent<s_time_handler>();

        v_hermarkers_collision_handler_setup.v_hermarkers_detected_player_entity_gameobject = GameObject.Find(v_hermarkers_collision_handler_setup.v_hermarkers_detected_player_entity_gameobject_name);

        v_hermarkers_sprite_handler_setup.v_hermarkers_idle_sprite_gameobject_script = v_hermarkers_sprite_handler_setup.v_hermarkers_idle_sprite_gameobject.GetComponent<s_sprite_handler>();
        v_hermarkers_sprite_handler_setup.v_hermarkers_death_sprite_gameobject_script = v_hermarkers_sprite_handler_setup.v_hermarkers_death_sprite_gameobject.GetComponent<s_sprite_handler>();
        v_hermarkers_sprite_handler_setup.v_hermarkers_birth_sprite_gameobject_script = v_hermarkers_sprite_handler_setup.v_hermarkers_birth_sprite_gameobject.GetComponent<s_sprite_handler>();
    }

    public void f_hermarkers_entity_definition_reader(bool sv_play, bool sv_reset_counter, v_tags_sprite_profile_list sv_profile, s_sprite_handler sv_target_script)
    {
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_play = sv_play;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_loops = sv_target_script.v_sprite_frame_setup.v_frame_loops;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_profile_inversion = sv_target_script.v_sprite_frame_setup.v_frame_profile_inversion;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_scale = sv_target_script.v_sprite_frame_setup.v_frame_scale;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_frame_setup.v_sprite_renderer_material = sv_target_script.v_sprite_frame_setup.v_sprite_renderer_material;

        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_list.Clear();
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_list.AddRange(sv_target_script.v_sprite_frame_setup.v_frame_list);

        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_sprite_profile = sv_profile;

        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_global = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_global;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_update_on_timer = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_update_on_timer;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_target;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_max = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_target_max;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_min = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_target_min;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_increment = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_increment;

        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_enabled = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_enabled;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_global = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_global;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_auto_change_direction = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_auto_change_direction;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_rely_on_frame_change_for_direction_change = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_rely_on_frame_change_for_direction_change;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_distance_threshold = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_distance_threshold;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_distance = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_distance;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance_max = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance_max;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_origin = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_origin;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin_max = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin_max;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_smoothness = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_smoothness;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness_max = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness_max;

        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_index_setup.Clear();
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_index_setup.AddRange(sv_target_script.v_sprite_index_setup);

        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_sort_setup.v_sort_modifier = sv_target_script.v_sprite_sort_setup.v_sort_modifier;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_sort_setup.v_enable_parent = sv_target_script.v_sprite_sort_setup.v_enable_parent;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_sort_setup.v_enable_parent_root = sv_target_script.v_sprite_sort_setup.v_enable_parent_root;
        v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_sort_setup.v_sort_modifier = sv_target_script.v_sprite_sort_setup.v_sort_modifier;

        if (sv_reset_counter)
        {
            v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_counter = 0;
            v_hermarkers_sprite_handler_setup.v_hermarkers_self_sprite_gameobject_script.f_sprite_index_element_counter_parameters_reset();
        }
    }
}
