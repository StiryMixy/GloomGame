using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class svl_camera_actualcamera
{
    [Header("Configurable Variables")]
    [SerializeField] public GameObject v_actualcamera_gameobject;
}

[Serializable]
public class svl_camera_height
{
    [Header("Configurable Variables")]
    [SerializeField] public float v_zoom_height_clamp = 10.0f;
    [SerializeField] public float v_zoom_speed = 1.5f;
    [SerializeField] public float v_zoom_distance_threshold = 0.01f;
    [Header("Reference Variables")]
    [SerializeField] public Vector3 v_zoom_current_position = Vector3.zero;
    [SerializeField] public Vector3 v_zoom_target_position = Vector3.zero;
    [SerializeField] public float v_zoom_difference = 0.0f;
    [SerializeField] public bool v_zoom_check = false;
}

[Serializable]
public class svl_camera_focus
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_focus_gameobject_name;
    [SerializeField] public float v_focus_lerp_speed = 1.0f;
    [SerializeField] public float v_focus_distance_threshold = 0.1f;
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_focus_gameobject;
    [SerializeField] public bool v_focus_check = false;
}

[Serializable]
public class svl_camera_black_fade
{
    [Header("Configurable Variables")]
    [SerializeField] public bool v_camera_black_fade_enabled;
    [SerializeField] public s_sprite_handler v_camera_black_fade_gameobject_script;
    [Range(0.0f, 1.0f)][SerializeField] public float v_camera_black_fade_target = 1.0f;
    [Header("Reference Variables")]
    [SerializeField] public bool v_camera_black_fade_input_block_upward;
    [SerializeField] public bool v_camera_black_fade_input_block_downward;
}

public class s_camera : MonoBehaviour
{
    [Header("Camera Time Caller Setup")]
    [SerializeField] public svgl_time_caller v_sprite_time_caller_setup = new svgl_time_caller();
    [Header("Key Manager Game Object Setup")]
    [SerializeField] public svgl_key_manager v_camera_key_manager_gameobject_setup = new svgl_key_manager();
    [Header("UI Handler Caller Setup")]
    [SerializeField] public svl_ui_caller v_ui_handler_setup = new svl_ui_caller();
    [Header("Camera Game Object Setup")]
    [SerializeField] public svl_camera_actualcamera v_camera_actualcamera_gameobject_setup = new svl_camera_actualcamera();
    [Header("Camera Height Setup")]
    [SerializeField] public svl_camera_height v_camera_height_setup = new svl_camera_height();
    [Header("Camera Focus Setup")]
    [SerializeField] public svl_camera_focus v_camera_focus_setup = new svl_camera_focus();
    [Header("Camera Black Fade Setup")]
    [SerializeField] public svl_camera_black_fade v_camera_black_fade_setup = new svl_camera_black_fade();
    [Header("Camera Debug Setup")]
    [SerializeField] public sgvl_debug_full_controller v_camera_debug_render_setup = new sgvl_debug_full_controller();

    void Start()
    {
        f_camera_gameobject_finder();
        v_camera_height_setup.v_zoom_check = f_camera_height_controller(true);
    }

    void Update()
    {
        v_camera_height_setup.v_zoom_check = f_camera_height_controller(false);
        v_camera_focus_setup.v_focus_check = f_camera_smoothly_move_towards();

        if (v_camera_black_fade_setup.v_camera_black_fade_enabled)
        {
            if (v_camera_black_fade_setup.v_camera_black_fade_input_block_upward)
            {
                v_camera_black_fade_setup.v_camera_black_fade_target = v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_max;
                v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target = v_camera_black_fade_setup.v_camera_black_fade_target;

                f_camera_elements_manipulator(false);

                if (v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha >= v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_max)
                {
                    v_camera_black_fade_setup.v_camera_black_fade_input_block_upward = false;
                    v_camera_black_fade_setup.v_camera_black_fade_input_block_downward = true;
                }
            }
            else if (v_camera_black_fade_setup.v_camera_black_fade_input_block_downward)
            {
                v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target = v_camera_black_fade_setup.v_camera_black_fade_target;

                if (v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha <= v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_min)
                {
                    v_camera_black_fade_setup.v_camera_black_fade_input_block_downward = false;

                    f_camera_elements_manipulator(true);
                }
            }
            else
            {
                v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target = v_camera_black_fade_setup.v_camera_black_fade_target;
            }
        }
        else
        {
            v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target = 0;
            v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha = 0;
        }

        v_camera_debug_render_setup.v_debug_manager_gameobject_script.f_debug_renderer_controller(v_camera_debug_render_setup.v_debug_gameobjects_list);
    }
    
    public void f_camera_gameobject_finder()
    {
        v_sprite_time_caller_setup.v_time_handler_gameobject = GameObject.Find(v_sprite_time_caller_setup.v_time_handler_gameobject_name);
        v_sprite_time_caller_setup.v_time_handler_script = v_sprite_time_caller_setup.v_time_handler_gameobject.GetComponent<s_time_handler>();

        v_ui_handler_setup.v_ui_gameobject = GameObject.Find(v_ui_handler_setup.v_ui_gameobject_name);
        v_ui_handler_setup.v_ui_gameobject_script = v_ui_handler_setup.v_ui_gameobject.GetComponent<s_ui_handler>();

        v_camera_focus_setup.v_focus_gameobject = GameObject.Find(v_camera_focus_setup.v_focus_gameobject_name);

        v_camera_key_manager_gameobject_setup.v_key_manager_gameobject = GameObject.Find(v_camera_key_manager_gameobject_setup.v_key_manager_gameobject_name);
        v_camera_key_manager_gameobject_setup.v_key_manager_gameobject_script = v_camera_key_manager_gameobject_setup.v_key_manager_gameobject.GetComponent<s_key_manager>();

        v_camera_debug_render_setup.v_debug_manager_gameobject = GameObject.Find(v_camera_debug_render_setup.v_debug_manager_gameobject_name);
        v_camera_debug_render_setup.v_debug_manager_gameobject_script = v_camera_debug_render_setup.v_debug_manager_gameobject.GetComponent<s_debug_controller>();
    }

    public bool f_camera_height_controller(bool sv_is_instant)
    {
        if (v_camera_height_setup.v_zoom_target_position.y != v_camera_height_setup.v_zoom_height_clamp)
        {
            if (v_camera_height_setup.v_zoom_target_position.y < v_camera_height_setup.v_zoom_height_clamp)
            {
                if ((v_camera_height_setup.v_zoom_height_clamp - v_camera_height_setup.v_zoom_target_position.y) > v_camera_height_setup.v_zoom_distance_threshold)
                {
                    Vector3 tv_position_to_add = (v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.parent.InverseTransformDirection(v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.forward)) * (-0.0001f);
                    while (v_camera_height_setup.v_zoom_target_position.y < v_camera_height_setup.v_zoom_height_clamp)
                    {
                        v_camera_height_setup.v_zoom_target_position += tv_position_to_add;
                    }
                }
            }
            else if (v_camera_height_setup.v_zoom_target_position.y > v_camera_height_setup.v_zoom_height_clamp)
            {
                if ((v_camera_height_setup.v_zoom_target_position.y - v_camera_height_setup.v_zoom_height_clamp) > v_camera_height_setup.v_zoom_distance_threshold)
                {
                    Vector3 tv_position_to_add = (v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.parent.InverseTransformDirection(v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.forward)) * (-0.0001f);
                    while (v_camera_height_setup.v_zoom_target_position.y > v_camera_height_setup.v_zoom_height_clamp)
                    {
                        v_camera_height_setup.v_zoom_target_position -= tv_position_to_add;
                    }
                }
            }
        }

        v_camera_height_setup.v_zoom_target_position.x = 0.0f;
        v_camera_height_setup.v_zoom_current_position = v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.localPosition;
        v_camera_height_setup.v_zoom_difference = Vector3.Distance(v_camera_height_setup.v_zoom_current_position, v_camera_height_setup.v_zoom_target_position);
        if (sv_is_instant)
        {
            if (v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.localPosition != v_camera_height_setup.v_zoom_target_position)
            {
                v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.localPosition = v_camera_height_setup.v_zoom_target_position;
            }
        }
        else
        {
            if (v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.localPosition != v_camera_height_setup.v_zoom_target_position)
            {
                if (v_camera_height_setup.v_zoom_difference >= v_camera_height_setup.v_zoom_distance_threshold)
                {
                    v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.localPosition = Vector3.Lerp(v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.localPosition, v_camera_height_setup.v_zoom_target_position, v_camera_height_setup.v_zoom_speed * Time.deltaTime);
                }
                else
                {
                    v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.localPosition = v_camera_height_setup.v_zoom_target_position;
                }
            }
        }

        return (v_camera_actualcamera_gameobject_setup.v_actualcamera_gameobject.transform.localPosition.Equals(v_camera_height_setup.v_zoom_target_position));
    }

    public bool f_camera_smoothly_move_towards()
    {
        transform.position = Vector3.Lerp(transform.position, v_camera_focus_setup.v_focus_gameobject.transform.position, v_camera_focus_setup.v_focus_lerp_speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, v_camera_focus_setup.v_focus_gameobject.transform.position) < v_camera_focus_setup.v_focus_distance_threshold)
        {
            return true;
        }
        return false;
    }

    public void f_scene_reset_action()
    {
        transform.position = Vector3.zero;
    }

    public bool f_scene_black_fade_maxed_alpha()
    {
        return (v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha >= v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_max);
    }

    public void f_camera_elements_manipulator(bool sv_target_state)
    {
        if (!sv_target_state)
        {
            v_camera_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_player_movement_setup.v_player_movement_enabled = false;
        }
        v_sprite_time_caller_setup.v_time_handler_script.v_time_is_stopped = !sv_target_state;
        v_ui_handler_setup.v_ui_gameobject_script.v_player_hud_setup.v_player_hud_is_visible = sv_target_state;
    }
}
