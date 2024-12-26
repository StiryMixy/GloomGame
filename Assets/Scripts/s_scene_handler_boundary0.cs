using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_entity_tag_library;

public class s_scene_handler_boundary0 : MonoBehaviour
{
    [Header("Scene Handler Setup")]
    public bool v_scene_enabled = true;
    public bool v_scene_debug_enabled = false;

    [Header("Camera Information Setup")]
    public GameObject v_entity_camera_gameobject;

    [Header("Camera Joystick Information Setup")]
    public GameObject v_entity_camera_joystick_gameobject;

    [Header("Player Information Setup")]
    public GameObject v_entity_player_gameobject;
    public s_entity_player v_entity_player_gameobject_script;

    [Header("Scene Information Setup")]
    public GameObject v_entity_origin_gameobject;
    public GameObject v_entity_scene_mover_1_gameobject;
    public GameObject v_entity_scene_mover_1_gameobject_destination;
    public GameObject v_entity_scene_mover_2_gameobject;
    public GameObject v_entity_scene_mover_2_gameobject_destination;

    // Start is called before the first frame update
    void Start()
    {
        if (v_scene_enabled)
        {
            v_entity_camera_gameobject = GameObject.Find("entity_camera");
            if (v_entity_camera_gameobject != null)
            {
                v_entity_camera_gameobject.transform.position = v_entity_origin_gameobject.transform.position;
            }

            v_entity_camera_joystick_gameobject = GameObject.Find("entity_camera_joystick");
            if (v_entity_camera_joystick_gameobject != null)
            {
                v_entity_camera_joystick_gameobject.transform.position = v_entity_origin_gameobject.transform.position;
            }

            v_entity_player_gameobject = GameObject.Find("entity_player");
            if (v_entity_player_gameobject != null)
            {
                v_entity_player_gameobject.transform.position = v_entity_origin_gameobject.transform.position;

                v_entity_player_gameobject_script = v_entity_player_gameobject.GetComponent<s_entity_player>();
                if (v_entity_player_gameobject_script != null)
                {
                    if (!v_entity_player_gameobject_script.v_player_tag_list.Contains(v_entity_tag_list.Alive))
                    {
                        v_entity_player_gameobject_script.v_player_tag_list.Add(v_entity_tag_list.Alive);
                    }
                    if (v_scene_debug_enabled)
                    {
                        if (!v_entity_player_gameobject_script.v_player_tag_list.Contains(v_entity_tag_list.Idle))
                        {
                            v_entity_player_gameobject_script.v_player_tag_list.Add(v_entity_tag_list.Idle);
                        }
                        if (!v_entity_player_gameobject_script.v_player_tag_list.Contains(v_entity_tag_list.CanWalk))
                        {
                            v_entity_player_gameobject_script.v_player_tag_list.Add(v_entity_tag_list.CanWalk);
                        }
                    }
                    else
                    {
                        if (!v_entity_player_gameobject_script.v_player_tag_list.Contains(v_entity_tag_list.Birth))
                        {
                            v_entity_player_gameobject_script.v_player_tag_list.Add(v_entity_tag_list.Birth);
                        }
                    }

                    v_entity_player_gameobject_script.v_player_entity_type = v_entity_list.entity_mote;
                    v_entity_player_gameobject_script.v_player_current_movement_mode = v_movement_mode_list.Walking;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //f_scene_handler_scene_mover_handler(v_entity_scene_mover_1_gameobject, v_entity_scene_mover_1_gameobject_destination);
        //f_scene_handler_scene_mover_handler(v_entity_scene_mover_2_gameobject, v_entity_scene_mover_2_gameobject_destination);
    }

    void f_scene_handler_scene_mover_handler(GameObject sv_object_entrance, GameObject sv_object_exit)
    {
        if (sv_object_entrance.GetComponent<s_entity_pathindicator>().v_pathindicator_collider_current_collisions_list.Contains(v_entity_player_gameobject))
        {
            v_entity_camera_gameobject.transform.position = sv_object_exit.transform.position;
            v_entity_camera_joystick_gameobject.transform.position = sv_object_exit.transform.position;
            v_entity_player_gameobject.transform.position = sv_object_exit.transform.position;
        }
    }
}
