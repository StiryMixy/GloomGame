using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_time_handler_gameobject_dictate_element
{
    [Header("Time Stopper")]
    [SerializeField] public bool v_time_handler_gameobject_dictate;
    [SerializeField] public bool v_time_is_stopped;
}

[Serializable]
public class svl_time_caller
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_time_handler_gameobject_name;
    [SerializeField] public svl_time_handler_gameobject_dictate_element v_time_handler_gameobject_dictate_element = new svl_time_handler_gameobject_dictate_element();
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_time_handler_gameobject;
    [SerializeField] public s_time_handler v_time_handler_script;
}

[Serializable]
public class svl_key_manager_gameobject_dictate_element
{
    [Header("Player Movement")]
    [SerializeField] public bool v_Key_manager_player_movement_dictate;
    [SerializeField] public bool v_player_movement_enabled;
}

[Serializable]
public class svl_key_manager_dictator
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_key_manager_gameobject_name;
    [SerializeField] public svl_key_manager_gameobject_dictate_element v_key_manager_gameobject_dictate_element = new svl_key_manager_gameobject_dictate_element();
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_key_manager_gameobject;
    [SerializeField] public s_key_manager v_key_manager_script;
}

[Serializable]
public class svl_player_gameobject_dictate_element
{
    [Header("Player Sprite Entity")]
    [SerializeField] public bool v_sprite_entity_dictate;
    [SerializeField] public v_tags_sprite_entity_list v_sprite_entity = v_tags_sprite_entity_list.entity_null;
    [Header("Player Sprite Last Direction")]
    [SerializeField] public bool v_sprite_direction_dictate;
    [SerializeField] public v_tags_sprite_orientation_list v_sprite_state_orientation;
    [SerializeField] public v_tags_sprite_profile_list v_sprite_state_profile;
}

[Serializable]
public class svl_player_collider_gameobject_dictate_element
{
    [Header("Player Speed")]
    [SerializeField] public bool v_speed_dictate;
    [SerializeField] public bool v_player_collider_movement_speed_is_walking;
}

[Serializable]
public class svl_player_dictator
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_player_gameobject_name;
    [SerializeField] public string v_player_collider_gameobject_name;
    [SerializeField] public svl_player_gameobject_dictate_element v_player_gameobject_dictate_element = new svl_player_gameobject_dictate_element();
    [Space(10)]
    [SerializeField] public svl_player_collider_gameobject_dictate_element v_player_collider_gameobject_dictate_element = new svl_player_collider_gameobject_dictate_element();
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_player_gameobject;
    [SerializeField] public s_player_handler v_player_gameobject_script;
    [SerializeField] public GameObject v_player_collider_gameobject;
    [SerializeField] public s_player_collider_controller v_player_collider_gameobject_script;
}

[Serializable]
public class svl_camera_gameobject_dictate_element
{
    [Header("Camera Black Fade Alpha Target")]
    [SerializeField] public bool v_camera_black_fade_target_dictate;
    [Range(0.0f, 1.0f)][SerializeField] public float v_camera_black_fade_target = 1.0f;
    [Header("Camera Black Fade Alpha")]
    [SerializeField] public bool v_camera_black_fade_alpha_dictate;
    [Range(0.0f, 1.0f)][SerializeField] public float v_sprite_alpha = 0.0f;
}

[Serializable]
public class svl_camera_dictator
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_camera_gameobject_name;
    [SerializeField] public svl_camera_gameobject_dictate_element v_camera_gameobject_dictate_element = new svl_camera_gameobject_dictate_element();
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_camera_gameobject;
    [SerializeField] public s_camera v_camera_gameobject_script;
}

[Serializable]
public class svl_ui_gameobject_dictate_element
{
    [Header("UI Player Hud Visibility")]
    [SerializeField] public bool v_ui_visibility_dictate;
    [SerializeField] public bool v_player_hud_is_visible;
}

[Serializable]
public class svl_ui_dictator
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_ui_gameobject_name;
    [SerializeField] public svl_ui_gameobject_dictate_element v_ui_gameobject_dictate_element = new svl_ui_gameobject_dictate_element();
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_ui_gameobject;
    [SerializeField] public s_ui_handler v_ui_gameobject_script;
}

public class s_scene_dictator : MonoBehaviour
{
    [Header("Time Handler Dictator Setup")]
    [SerializeField] public svl_time_caller v_time_handler_dictator_setup = new svl_time_caller();
    [Header("Key Manager Dictator Setup")]
    [SerializeField] public svl_key_manager_dictator v_key_manager_dictator_setup = new svl_key_manager_dictator();
    [Header("Camera Dictator Setup")]
    [SerializeField] public svl_camera_dictator v_camera_dictator_setup = new svl_camera_dictator();
    [Header("Player Dictator Setup")]
    [SerializeField] public svl_player_dictator v_player_dictator_setup = new svl_player_dictator();
    [Header("UI Dictator Setup")]
    [SerializeField] public svl_ui_dictator v_ui_dictator_setup = new svl_ui_dictator();

    void Start()
    {
        f_scene_dictator_gameobject_finder();
        f_time_handler_dictate();
        f_key_manager_dictate();
        f_camera_dictate();
        f_player_dictate();
        f_player_collider_dictate();
        f_ui_dictate();
    }

    public void f_scene_dictator_gameobject_finder()
    {
        v_time_handler_dictator_setup.v_time_handler_gameobject = GameObject.Find(v_time_handler_dictator_setup.v_time_handler_gameobject_name);
        v_time_handler_dictator_setup.v_time_handler_script = v_time_handler_dictator_setup.v_time_handler_gameobject.GetComponent<s_time_handler>();

        v_key_manager_dictator_setup.v_key_manager_gameobject = GameObject.Find(v_key_manager_dictator_setup.v_key_manager_gameobject_name);
        v_key_manager_dictator_setup.v_key_manager_script = v_key_manager_dictator_setup.v_key_manager_gameobject.GetComponent<s_key_manager>();

        v_camera_dictator_setup.v_camera_gameobject = GameObject.Find(v_camera_dictator_setup.v_camera_gameobject_name);
        v_camera_dictator_setup.v_camera_gameobject_script = v_camera_dictator_setup.v_camera_gameobject.GetComponent<s_camera>();

        v_player_dictator_setup.v_player_gameobject = GameObject.Find(v_player_dictator_setup.v_player_gameobject_name);
        v_player_dictator_setup.v_player_gameobject_script = v_player_dictator_setup.v_player_gameobject.GetComponent<s_player_handler>();

        v_player_dictator_setup.v_player_collider_gameobject = GameObject.Find(v_player_dictator_setup.v_player_collider_gameobject_name);
        v_player_dictator_setup.v_player_collider_gameobject_script = v_player_dictator_setup.v_player_collider_gameobject.GetComponent<s_player_collider_controller>();

        v_ui_dictator_setup.v_ui_gameobject = GameObject.Find(v_ui_dictator_setup.v_ui_gameobject_name);
        v_ui_dictator_setup.v_ui_gameobject_script = v_ui_dictator_setup.v_ui_gameobject.GetComponent<s_ui_handler>();
    }

    public void f_time_handler_dictate()
    {
        if (v_time_handler_dictator_setup.v_time_handler_gameobject_dictate_element.v_time_handler_gameobject_dictate)
        {
            v_time_handler_dictator_setup.v_time_handler_script.v_time_is_stopped = v_time_handler_dictator_setup.v_time_handler_gameobject_dictate_element.v_time_is_stopped;
        }
    }

    public void f_key_manager_dictate()
    {
        if (v_key_manager_dictator_setup.v_key_manager_gameobject_dictate_element.v_Key_manager_player_movement_dictate)
        {
            v_key_manager_dictator_setup.v_key_manager_script.v_key_manager_player_movement_setup.v_player_movement_enabled = v_key_manager_dictator_setup.v_key_manager_gameobject_dictate_element.v_player_movement_enabled;
        }
    }

    public void f_camera_dictate()
    {
        if (v_camera_dictator_setup.v_camera_gameobject_dictate_element.v_camera_black_fade_target_dictate)
        {
            v_camera_dictator_setup.v_camera_gameobject_script.v_camera_black_fade_setup.v_camera_black_fade_target = v_camera_dictator_setup.v_camera_gameobject_dictate_element.v_camera_black_fade_target;
        }
        if (v_camera_dictator_setup.v_camera_gameobject_dictate_element.v_camera_black_fade_alpha_dictate)
        {
            v_camera_dictator_setup.v_camera_gameobject_script.v_camera_black_fade_setup.v_camera_black_fade_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha = v_camera_dictator_setup.v_camera_gameobject_dictate_element.v_sprite_alpha;
        }
    }

    public void f_player_dictate()
    {
        if (v_player_dictator_setup.v_player_gameobject_dictate_element.v_sprite_entity_dictate)
        {
            v_player_dictator_setup.v_player_gameobject_script.v_player_sprite_setup.v_sprite_entity = v_player_dictator_setup.v_player_gameobject_dictate_element.v_sprite_entity;
        }
        if (v_player_dictator_setup.v_player_gameobject_dictate_element.v_sprite_direction_dictate)
        {
            v_player_dictator_setup.v_player_gameobject_script.v_player_sprite_setup.v_sprite_state_orientation = v_player_dictator_setup.v_player_gameobject_dictate_element.v_sprite_state_orientation;
            v_player_dictator_setup.v_player_gameobject_script.v_player_sprite_setup.v_sprite_state_profile = v_player_dictator_setup.v_player_gameobject_dictate_element.v_sprite_state_profile;
        }
    }

    public void f_player_collider_dictate()
    {
        if (v_player_dictator_setup.v_player_collider_gameobject_dictate_element.v_speed_dictate)
        {
            v_player_dictator_setup.v_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_speed_setup.v_player_collider_movement_speed_is_walking = v_player_dictator_setup.v_player_collider_gameobject_dictate_element.v_player_collider_movement_speed_is_walking;
        }
    }

    public void f_ui_dictate()
    {
        if (v_ui_dictator_setup.v_ui_gameobject_dictate_element.v_ui_visibility_dictate)
        {
            v_ui_dictator_setup.v_ui_gameobject_script.v_player_hud_setup.v_player_hud_is_visible = v_ui_dictator_setup.v_ui_gameobject_dictate_element.v_player_hud_is_visible;
        }
    }
}
