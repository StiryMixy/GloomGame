using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEditor.Presets;

[Serializable]
public class svl_titlesequence_visualizer
{
    [Header("Configurable Variables")]
    [SerializeField] public bool v_titlesequence_visualizer_is_enabled;
    [Space(10)]
    [SerializeField] public RectTransform v_titlesequence_visualizer_self_rect_transform_script;
    [SerializeField] public Preset v_titlesequence_visualizer_self_rect_transform_null_preset;
    [SerializeField] public UnityEngine.UI.Image v_titlesequence_visualizer_self_image_script;
    [SerializeField] public Preset v_titlesequence_visualizer_self_image_script_null_preset;
    [SerializeField] public s_ui_hud_image_alpha_handler v_titlesequence_visualizer_self_image_alpha_handler;
    [Space(10)]
    [SerializeField] public RectTransform v_titlesequence_visualizer_text_rect_transform_script;
    [SerializeField] public Preset v_titlesequence_visualizer_text_rect_transform_null_preset;
    [SerializeField] public UnityEngine.UI.Text v_titlesequence_visualizer_text_script;
    [SerializeField] public Preset v_titlesequence_visualizer_text_script_null_preset;
    [SerializeField] public s_ui_hud_text_alpha_handler v_titlesequence_visualizer_text_alpha_handler;
    [Space(10)]
    [SerializeField] public Sprite v_titlesequence_visualizer_sprite;
    [SerializeField] public float v_titlesequence_visualizer_self_rect_transform_script_distance_threshold;
    [SerializeField] public float v_titlesequence_visualizer_self_rect_transform_script_lerpspeed;
    [SerializeField] public float v_titlesequence_visualizer_text_distance_alpha_threshold;
    [Space(10)]
    [SerializeField] public bool v_titlesequence_visualizer_scene_changer;
    [SerializeField] public string v_titlesequence_visualizer_scene_changer_target_scene;
    [SerializeField] public float v_titlesequence_visualizer_scene_changer_distance_threshold;
    [SerializeField] public int v_titlesequence_visualizer_scene_changer_elapsed_time_threshold;
    [SerializeField] public bool v_titlesequence_visualizer_scene_changer_fade_changer_enabled;
    [SerializeField] public bool v_titlesequence_visualizer_scene_changer_fade_is_white;
    [Header("Reference Variables")]
    [SerializeField] public bool v_titlesequence_visualizer_self_image_script_is_null;
    [SerializeField] public bool v_titlesequence_visualizer_self_rect_transform_script_is_moving;
    [SerializeField] public float v_titlesequence_visualizer_self_rect_transform_script_distance;
    [SerializeField] public int v_titlesequence_visualizer_elapsed_time;
}

public class s_ui_hud_titlesequence_visualizer : MonoBehaviour
{
    [Header("Title Sequence Time Caller Setup")]
    [SerializeField] public svgl_time_caller v_titlesequence_visualizer_time_caller_setup = new svgl_time_caller();
    [Header("Title Sequence Reset Manager Setup")]
    [SerializeField] public svgl_reset_manager_caller v_titlesequence_visualizer_reset_manager_setup = new svgl_reset_manager_caller();
    [Header("Title Sequence Visualizer Setup")]
    [SerializeField] public svl_titlesequence_visualizer v_titlesequence_visualizer_setup = new svl_titlesequence_visualizer();

    void Start()
    {
        f_titlesequence_time_handler_gameobject_finder();
        f_titlesequence_reset_manager_gameobject_finder();
    }

    void Update()
    {
        if (v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_image_script.sprite.Equals(null))
        {
            v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_image_script_is_null = true;
        }
        else
        {
            v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_image_script_is_null = false;
        }

        v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script_distance = Vector3.Distance(v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script.anchoredPosition, Vector3.zero);

        if (v_titlesequence_visualizer_setup.v_titlesequence_visualizer_is_enabled)
        {
            if (v_titlesequence_visualizer_time_caller_setup.v_time_handler_script.f_time_level_gate_get(s_tag_library.v_tags_timer_level_list.Metaphysical))
            {
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_elapsed_time = v_titlesequence_visualizer_setup.v_titlesequence_visualizer_elapsed_time + 1;
            }

            if ((!v_titlesequence_visualizer_setup.v_titlesequence_visualizer_sprite.Equals(null)) && (v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_image_script_is_null))
            {
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_image_script.sprite = v_titlesequence_visualizer_setup.v_titlesequence_visualizer_sprite;
            }

            v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script_is_moving = f_titlesequence_lerp_to_zero();

            if (v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script_distance < v_titlesequence_visualizer_setup.v_titlesequence_visualizer_text_distance_alpha_threshold)
            {
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_text_alpha_handler.v_text_alpha_handler_setup.v_text_alpha_target = 1.0f;
            }
            else
            {
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_text_alpha_handler.v_text_alpha_handler_setup.v_text_alpha_target = 0.0f;
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_text_alpha_handler.v_text_alpha_handler_setup.v_text_alpha = 0.0f;
            }

            if (v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer)
            {
                if (v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script_distance <= v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer_distance_threshold)
                {
                    if (v_titlesequence_visualizer_setup.v_titlesequence_visualizer_elapsed_time >= v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer_elapsed_time_threshold)
                    {
                        v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_image_alpha_handler.v_image_alpha_handler_setup.v_image_alpha_target = 0.0f;
                        v_titlesequence_visualizer_setup.v_titlesequence_visualizer_text_alpha_handler.v_text_alpha_handler_setup.v_text_alpha_target = 0.0f;

                        if (v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer_target_scene != "")
                        {
                            if (v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer_fade_changer_enabled)
                            {
                                v_titlesequence_visualizer_reset_manager_setup.v_reset_manager_script.v_scene_reset_manager_targets_setup.v_scene_camera_target_fade_is_white = v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer_fade_is_white;
                            }
                            v_titlesequence_visualizer_reset_manager_setup.v_reset_manager_script.f_scene_reset_action(v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer_target_scene);
                        }
                    }
                }
            }
        }
        else
        {
            if (v_titlesequence_visualizer_setup.v_titlesequence_visualizer_elapsed_time != 0)
            {
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_elapsed_time = 0;
            }

            if (!v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_image_script_is_null)
            {
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_sprite = null;
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_null_preset.ApplyTo(v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script);
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_image_script_null_preset.ApplyTo(v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_image_script);
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_text_rect_transform_null_preset.ApplyTo(v_titlesequence_visualizer_setup.v_titlesequence_visualizer_text_rect_transform_script);
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_text_script_null_preset.ApplyTo(v_titlesequence_visualizer_setup.v_titlesequence_visualizer_text_script);
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script_distance_threshold = 0.0f;
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script_lerpspeed = 0.0f;
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_text_distance_alpha_threshold = 0.0f;
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer = false;
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer_target_scene = "";
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer_distance_threshold = 0.0f;
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer_elapsed_time_threshold = 0;
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer_fade_changer_enabled = false;
                v_titlesequence_visualizer_setup.v_titlesequence_visualizer_scene_changer_fade_is_white = false;
            }
        }
    }

    public bool f_titlesequence_lerp_to_zero()
    {
        if (v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script_distance > v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script_distance_threshold)
        {
            v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script.anchoredPosition = Vector3.Lerp(v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script.anchoredPosition, Vector3.zero, v_titlesequence_visualizer_setup.v_titlesequence_visualizer_self_rect_transform_script_lerpspeed);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void f_titlesequence_time_handler_gameobject_finder()
    {
        v_titlesequence_visualizer_time_caller_setup.v_time_handler_gameobject = GameObject.Find(v_titlesequence_visualizer_time_caller_setup.v_time_handler_gameobject_name);
        v_titlesequence_visualizer_time_caller_setup.v_time_handler_script = v_titlesequence_visualizer_time_caller_setup.v_time_handler_gameobject.GetComponent<s_time_handler>();
    }

    public void f_titlesequence_reset_manager_gameobject_finder()
    {
        v_titlesequence_visualizer_reset_manager_setup.v_reset_manager_gameobject = GameObject.Find(v_titlesequence_visualizer_reset_manager_setup.v_reset_manager_gameobject_name);
        v_titlesequence_visualizer_reset_manager_setup.v_reset_manager_script = v_titlesequence_visualizer_reset_manager_setup.v_reset_manager_gameobject.GetComponent<s_scene_reset_manager>();
    }
}
