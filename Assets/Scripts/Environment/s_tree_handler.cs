using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_tree_sprite_handler
{
    [Header("Configurable Variables")]
    [SerializeField] public GameObject v_tree_self_sprite_gameobject;
    [SerializeField] public s_sprite_handler v_tree_self_sprite_gameobject_script;
    [Space(10)]
    [SerializeField] public GameObject v_tree_visible_sprite_gameobject;
    [SerializeField] public GameObject v_tree_occluded_sprite_gameobject;
    [Space(10)]
    [SerializeField] public bool v_tree_entity_shadow_enabled = false;
    [SerializeField] public bool v_tree_entity_shadow_is_white = false;
    [SerializeField] public float v_tree_entity_shadow_scale;
    [SerializeField] public s_sprite_handler v_tree_entity_shadow_script;
    [Header("Reference Variables")]
    [SerializeField] public s_sprite_handler v_tree_visible_sprite_gameobject_script;
    [SerializeField] public s_sprite_handler v_tree_occluded_sprite_gameobject_script;
    [SerializeField] public bool v_tree_detected_player;
}

public class s_tree_handler : MonoBehaviour
{
    [Header("Tree Handler Time Handler Setup")]
    [SerializeField] public svgl_time_handler v_tree_time_handler_setup = new svgl_time_handler();

    [Header("Tree Sprite Handler Setup")]
    [SerializeField] public svl_tree_sprite_handler v_tree_sprite_handler_setup = new svl_tree_sprite_handler();

    [Header("Tree Collision Handler Setup")]
    [SerializeField] public bool v_tree_detected_player;
    [SerializeField] public List<GameObject> v_tree_collision_list;

    void Start()
    {
        f_tree_handler_gameobject_finder();
        f_tree_entity_spriter();
    }

    void Update()
    {
        f_tree_shadow_controller();
        if (v_tree_sprite_handler_setup.v_tree_detected_player != v_tree_detected_player)
        {
            v_tree_sprite_handler_setup.v_tree_detected_player = v_tree_detected_player;

            f_tree_entity_spriter();
        }
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        if (v_tree_collision_list.Count <= 0)
        {
            if (sv_other_object.gameObject.GetComponent<s_player_handler>())
            {
                v_tree_detected_player = true;
                v_tree_collision_list.Add(sv_other_object.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider sv_other_object)
    {

    }

    private void OnTriggerExit(Collider sv_other_object)
    {
        if (v_tree_collision_list.Count > 0)
        {
            if (v_tree_collision_list.Contains(sv_other_object.gameObject))
            {
                v_tree_detected_player = false;
                v_tree_collision_list.Remove(sv_other_object.gameObject);
            }
        }
    }

    public void f_tree_handler_gameobject_finder()
    {
        v_tree_time_handler_setup.v_time_handler_gameobject = GameObject.Find(v_tree_time_handler_setup.v_time_handler_gameobject_name);
        v_tree_time_handler_setup.v_time_handler_script = v_tree_time_handler_setup.v_time_handler_gameobject.GetComponent<s_time_handler>();

        v_tree_sprite_handler_setup.v_tree_visible_sprite_gameobject_script = v_tree_sprite_handler_setup.v_tree_visible_sprite_gameobject.GetComponent<s_sprite_handler>();
        v_tree_sprite_handler_setup.v_tree_occluded_sprite_gameobject_script = v_tree_sprite_handler_setup.v_tree_occluded_sprite_gameobject.GetComponent<s_sprite_handler>();
    }

    public void f_tree_shadow_controller()
    {
        if (v_tree_sprite_handler_setup.v_tree_entity_shadow_enabled)
        {
            v_tree_sprite_handler_setup.v_tree_entity_shadow_script.v_sprite_alpha_setup.v_sprite_alpha_target = v_tree_sprite_handler_setup.v_tree_entity_shadow_script.v_sprite_alpha_setup.v_sprite_alpha_target_max;

            if (v_tree_sprite_handler_setup.v_tree_entity_shadow_is_white)
            {
                v_tree_sprite_handler_setup.v_tree_entity_shadow_script.v_sprite_frame_setup.v_frame_counter = 1;
            }
            else
            {
                v_tree_sprite_handler_setup.v_tree_entity_shadow_script.v_sprite_frame_setup.v_frame_counter = 0;
            }

            v_tree_sprite_handler_setup.v_tree_entity_shadow_script.v_sprite_frame_setup.v_frame_scale = v_tree_sprite_handler_setup.v_tree_entity_shadow_scale;
        }
        else
        {
            v_tree_sprite_handler_setup.v_tree_entity_shadow_script.v_sprite_alpha_setup.v_sprite_alpha_target = v_tree_sprite_handler_setup.v_tree_entity_shadow_script.v_sprite_alpha_setup.v_sprite_alpha_target_min;
        }
    }

    public void f_tree_entity_definition_reader(bool sv_play, bool sv_reset_counter, v_tags_sprite_profile_list sv_profile, s_sprite_handler sv_target_script)
    {
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_play = sv_play;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_loops = sv_target_script.v_sprite_frame_setup.v_frame_loops;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_profile_inversion = sv_target_script.v_sprite_frame_setup.v_frame_profile_inversion;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_scale = sv_target_script.v_sprite_frame_setup.v_frame_scale;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_frame_setup.v_sprite_renderer_material = sv_target_script.v_sprite_frame_setup.v_sprite_renderer_material;

        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_list.Clear();
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_list.AddRange(sv_target_script.v_sprite_frame_setup.v_frame_list);

        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_sprite_profile = sv_profile;

        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_global = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_global;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_update_on_timer = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_update_on_timer;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_target;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_max = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_target_max;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_min = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_target_min;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_increment = sv_target_script.v_sprite_alpha_setup.v_sprite_alpha_increment;

        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_enabled = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_enabled;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_global = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_global;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_auto_change_direction = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_auto_change_direction;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_rely_on_frame_change_for_direction_change = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_rely_on_frame_change_for_direction_change;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_distance_threshold = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_distance_threshold;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_distance = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_distance;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance_max = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_distance_max;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_origin = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_origin;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin_max = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_origin_max;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_smoothness = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_randomize_smoothness;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness_max = sv_target_script.v_sprite_float_up_setup.v_sprite_float_up_target_smoothness_max;

        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_index_setup.Clear();
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_index_setup.AddRange(sv_target_script.v_sprite_index_setup);

        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_sort_setup.v_sort_modifier = sv_target_script.v_sprite_sort_setup.v_sort_modifier;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_sort_setup.v_enable_parent = sv_target_script.v_sprite_sort_setup.v_enable_parent;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_sort_setup.v_enable_parent_root = sv_target_script.v_sprite_sort_setup.v_enable_parent_root;
        v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_sort_setup.v_sort_modifier = sv_target_script.v_sprite_sort_setup.v_sort_modifier;

        if (sv_reset_counter)
        {
            v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.v_sprite_frame_setup.v_frame_counter = 0;
            v_tree_sprite_handler_setup.v_tree_self_sprite_gameobject_script.f_sprite_index_element_counter_parameters_reset();
        }
    }

    public void f_tree_entity_spriter()
    {
        if (v_tree_sprite_handler_setup.v_tree_detected_player)
        {
            f_tree_entity_definition_reader(true, true, v_tags_sprite_profile_list.Right, v_tree_sprite_handler_setup.v_tree_occluded_sprite_gameobject_script);
        }
        else
        {
            f_tree_entity_definition_reader(true, true, v_tags_sprite_profile_list.Right, v_tree_sprite_handler_setup.v_tree_visible_sprite_gameobject_script);
        }
    }
}
