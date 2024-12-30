using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static s_tag_library;

[Serializable]
public class svl_pathing_render
{
    [Header("Configurable Variables")]
    [SerializeField] public s_sprite_handler v_sprite_handler_gameobject_script;
    [Header("Reference Variables")]
    [SerializeField] public bool v_pathing_render_enable = false;
}

[Serializable]
public class svl_pathing_type
{
    [Header("Configurable Variables")]
    [SerializeField] public v_tags_movement_mode_list v_pathing_type;
    [Header("Reference Variables")]
    [SerializeField] public bool v_pathing_type_is_disregarded = false;
}

[Serializable]
public class svl_pathing_scene_changer
{
    [Header("Configurable Variables")]
    [SerializeField] public bool v_pathing_scene_changer_enabled;
    [SerializeField] public string v_pathing_scene_changer_target;
}

public class s_pathing : MonoBehaviour
{
    [Header("Pathing Key Manager Object Setup")]
    [SerializeField] public svgl_key_manager v_pathing_key_manager_gameobject_setup = new svgl_key_manager();

    [Header("Pathing Render Setup")]
    [SerializeField] public svl_pathing_render v_pathing_render_setup = new svl_pathing_render();

    [Header("Pathing Type Setup")]
    [SerializeField] public svl_pathing_type v_pathing_type_setup = new svl_pathing_type();

    [Header("Pathing Scene Changer Setup")]
    [SerializeField] public svl_pathing_scene_changer v_pathing_scene_changer_setup = new svl_pathing_scene_changer();

    [Header("Pathing Collider References")]
    [SerializeField] public List<GameObject> v_pathing_collider_current_collisions_list;

    [Header("Pathing Debug Setup")]
    [SerializeField] public sgvl_debug_full_controller v_pathing_debug_render_setup = new sgvl_debug_full_controller();

    void Start()
    {
        f_pathing_gameobject_finder();
    }

    void Update()
    {
        v_pathing_render_setup.v_pathing_render_enable = v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_pathing_render_setup.v_pathing_render_enable;
        v_pathing_type_setup.v_pathing_type_is_disregarded = false;

        if (v_pathing_render_setup.v_pathing_render_enable)
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_global = true;
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_update_on_timer = false;
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target = v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_max;
        }
        else
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_global = true;
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_update_on_timer = false;
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target = v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_min;
        }

        if (v_pathing_scene_changer_setup.v_pathing_scene_changer_enabled)
        {
            v_pathing_type_setup.v_pathing_type_is_disregarded = true;

            f_pathing_frame_counter_dictator(10, 11, 12, 0);
        }

        if (!v_pathing_type_setup.v_pathing_type_is_disregarded)
        {
            f_pathing_frame_counter_dictator(1, 2, 3, 0);
        }

        v_pathing_debug_render_setup.v_debug_manager_gameobject_script.f_debug_renderer_controller(v_pathing_debug_render_setup.v_debug_gameobjects_list);
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        if (!v_pathing_collider_current_collisions_list.Contains(sv_other_object.gameObject))
        {
            v_pathing_collider_current_collisions_list.Add(sv_other_object.gameObject);
        }
        if (v_pathing_scene_changer_setup.v_pathing_scene_changer_enabled) 
        {
            if (v_pathing_scene_changer_setup.v_pathing_scene_changer_target != "")
            {
                if (sv_other_object.gameObject.TryGetComponent<s_scene_reset_manager>(out var ov_scene_reset_manager))
                {
                    ov_scene_reset_manager.f_scene_reset_action();
                    SceneManager.LoadScene(sceneName: v_pathing_scene_changer_setup.v_pathing_scene_changer_target);
                }
            }
        }
    }

    private void OnTriggerStay(Collider sv_other_object)
    {

    }

    private void OnTriggerExit(Collider sv_other_object)
    {
        if (v_pathing_collider_current_collisions_list.Count > 0)
        {
            if (v_pathing_collider_current_collisions_list.Contains(sv_other_object.gameObject))
            {
                v_pathing_collider_current_collisions_list.Remove(sv_other_object.gameObject);
            }
        }
    }

    public void f_pathing_gameobject_finder()
    {
        v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject = GameObject.Find(v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject_name);
        v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject_script = v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject.GetComponent<s_key_manager>();

        v_pathing_debug_render_setup.v_debug_manager_gameobject = GameObject.Find(v_pathing_debug_render_setup.v_debug_manager_gameobject_name);
        v_pathing_debug_render_setup.v_debug_manager_gameobject_script = v_pathing_debug_render_setup.v_debug_manager_gameobject.GetComponent<s_debug_controller>();
    }

    public void f_pathing_targetted_by_player_default_movement()
    {
        if (!v_pathing_type_setup.v_pathing_type_is_disregarded)
        {
            f_pathing_frame_counter_dictator(4, 5, 6, 0);
        }
        else
        {
            if (v_pathing_scene_changer_setup.v_pathing_scene_changer_enabled)
            {
                f_pathing_frame_counter_dictator(13, 14, 15, 0);
            }
        }
    }

    public void f_pathing_targetted_by_player_dodge_movement()
    {
        if (!v_pathing_type_setup.v_pathing_type_is_disregarded)
        {
            f_pathing_frame_counter_dictator(7, 8, 9, 0);
        }
        else
        {
            if (v_pathing_scene_changer_setup.v_pathing_scene_changer_enabled)
            {
                f_pathing_frame_counter_dictator(16, 17, 18, 0);
            }
        }
    }

    public void f_pathing_frame_counter_dictator(int sv_walking_and_flying_frame, int sv_flying_frame, int sv_walking_frame, int sv_null_frame)
    {
        if (v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.WalkingAndFlying))
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_frame_setup.v_frame_counter = sv_walking_and_flying_frame;
        }
        else if (v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.Flying))
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_frame_setup.v_frame_counter = sv_flying_frame;
        }
        else if (v_pathing_type_setup.v_pathing_type.Equals(v_tags_movement_mode_list.Walking))
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_frame_setup.v_frame_counter = sv_walking_frame;
        }
        else
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_frame_setup.v_frame_counter = sv_null_frame;
        }
    }
}
