using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

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
    [SerializeField] public bool v_player_collider_movement_speed_toggle;
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
    [Header("Player Sprite Entity")]
    [SerializeField] public bool v_camera_black_fade_dictate;
    [Range(0.0f, 1.0f)][SerializeField] public float v_camera_black_fade_target = 1.0f;
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

public class s_scene_dictator : MonoBehaviour
{
    [Header("Camera Dictator Setup")]
    [SerializeField] public svl_camera_dictator v_camera_dictator_setup = new svl_camera_dictator();

    [Header("Player Dictator Setup")]
    [SerializeField] public svl_player_dictator v_player_dictator_setup = new svl_player_dictator();

    void Start()
    {
        f_scene_dictator_gameobject_finder();
        f_camera_dictate();
        f_player_dictate();
        f_player_collider_dictate();
    }

    public void f_scene_dictator_gameobject_finder()
    {
        v_camera_dictator_setup.v_camera_gameobject = GameObject.Find(v_camera_dictator_setup.v_camera_gameobject_name);
        v_camera_dictator_setup.v_camera_gameobject_script = v_camera_dictator_setup.v_camera_gameobject.GetComponent<s_camera>();

        v_player_dictator_setup.v_player_gameobject = GameObject.Find(v_player_dictator_setup.v_player_gameobject_name);
        v_player_dictator_setup.v_player_gameobject_script = v_player_dictator_setup.v_player_gameobject.GetComponent<s_player_handler>();

        v_player_dictator_setup.v_player_collider_gameobject = GameObject.Find(v_player_dictator_setup.v_player_collider_gameobject_name);
        v_player_dictator_setup.v_player_collider_gameobject_script = v_player_dictator_setup.v_player_collider_gameobject.GetComponent<s_player_collider_controller>();
    }

    public void f_camera_dictate()
    {
        if (v_camera_dictator_setup.v_camera_gameobject_dictate_element.v_camera_black_fade_dictate)
        {
            v_camera_dictator_setup.v_camera_gameobject_script.v_camera_black_fade_setup.v_camera_black_fade_target = v_camera_dictator_setup.v_camera_gameobject_dictate_element.v_camera_black_fade_target;
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
            v_player_dictator_setup.v_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_speed_setup.v_player_collider_movement_speed_toggle = v_player_dictator_setup.v_player_collider_gameobject_dictate_element.v_player_collider_movement_speed_toggle;
        }
    }
}
