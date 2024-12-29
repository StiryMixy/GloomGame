using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_camera_joystick_focus
{
    [Header("Configurable Variables")]
    [SerializeField] public bool v_focus_enable = true;
    [SerializeField] public string v_focus_gameobject_name;
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_focus_gameobject;
}

[Serializable]
public class svl_camera_joystick_focus_detach
{
    [Header("Configurable Variables")]
    [SerializeField] public float v_focus_detach_clamp = 3.0f;
    [SerializeField] public float v_focus_detach_lerp_speed = 10.0f;
    [SerializeField] public float v_focus_detach_distance_threshold = 1.25f;
    [Header("Reference Variables")]
    [SerializeField] public bool v_focus_detach_enable = false;
}

public class s_camera_joystick : MonoBehaviour
{
    [Header("Camera Joystick Key Manager Object Setup")]
    [SerializeField] public svgl_key_manager v_camera_joystick_key_manager_gameobject_setup = new svgl_key_manager();

    [Header("Camera Joystick Focus Setup")]
    [SerializeField] public svl_camera_joystick_focus v_camera_joystick_focus_setup = new svl_camera_joystick_focus();

    [Header("Camera Joystick Focus Detach Setup")]
    [SerializeField] public svl_camera_joystick_focus_detach v_camera_joystick_focus_detach_setup = new svl_camera_joystick_focus_detach();

    [Header("Camera Joystick Debug Setup")]
    [SerializeField] public sgvl_debug_controller v_camera_joystick_debug_render_setup = new sgvl_debug_controller();

    void Start()
    {
        f_camera_joystick_gameobject_finder();
    }

    void Update()
    {
        v_camera_joystick_focus_detach_setup.v_focus_detach_enable = f_camera_joystick_focus_detach_controller(v_camera_joystick_focus_detach_setup.v_focus_detach_enable);
        f_camera_joystick_focus_handler();
        f_camera_joystick_focus_detach_handler();
        v_camera_joystick_debug_render_setup.v_debug_manager_gameobject_script.f_debug_renderer_controller(v_camera_joystick_debug_render_setup.v_debug_gameobjects_list);
    }

    public void f_camera_joystick_gameobject_finder()
    {
        v_camera_joystick_key_manager_gameobject_setup.v_key_manager_gameobject = GameObject.Find(v_camera_joystick_key_manager_gameobject_setup.v_key_manager_gameobject_name);
        v_camera_joystick_key_manager_gameobject_setup.v_key_manager_gameobject_script = v_camera_joystick_key_manager_gameobject_setup.v_key_manager_gameobject.GetComponent<s_key_manager>();

        v_camera_joystick_focus_setup.v_focus_gameobject = GameObject.Find(v_camera_joystick_focus_setup.v_focus_gameobject_name);

        v_camera_joystick_debug_render_setup.v_debug_manager_gameobject = GameObject.Find(v_camera_joystick_debug_render_setup.v_debug_manager_gameobject_name);
        v_camera_joystick_debug_render_setup.v_debug_manager_gameobject_script = v_camera_joystick_debug_render_setup.v_debug_manager_gameobject.GetComponent<s_debug_controller>();
    }

    public void f_camera_joystick_focus_handler()
    {
        if (v_camera_joystick_focus_setup.v_focus_enable && !v_camera_joystick_focus_detach_setup.v_focus_detach_enable)
        {
            transform.position = v_camera_joystick_focus_setup.v_focus_gameobject.transform.position;
        }
    }

    public bool f_camera_joystick_focus_detach_controller(bool sv_camera_joystick_focus_detach)
    {
        if (v_camera_joystick_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_camera_joystick_focus_detach_setup.v_camera_joystick_focus_detach_key_press_mode.Equals(v_tags_key_press_mode_list.Toggle))
        {
            if (Input.GetKeyDown(v_camera_joystick_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_camera_joystick_focus_detach_setup.v_camera_joystick_focus_detach_key))
            {
                return (!sv_camera_joystick_focus_detach);
            }
            else
            {
                return (sv_camera_joystick_focus_detach);
            }
        }
        else if (v_camera_joystick_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_camera_joystick_focus_detach_setup.v_camera_joystick_focus_detach_key_press_mode.Equals(v_tags_key_press_mode_list.Hold))
        {
            if (Input.GetKey(v_camera_joystick_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_camera_joystick_focus_detach_setup.v_camera_joystick_focus_detach_key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return (sv_camera_joystick_focus_detach);
        }
    }

    public void f_camera_joystick_focus_detach_handler()
    {
        if (v_camera_joystick_focus_detach_setup.v_focus_detach_enable)
        {
            if (Input.mousePosition.y < ((Screen.height / 2) - ((Screen.height / 2) / v_camera_joystick_focus_detach_setup.v_focus_detach_distance_threshold)))
            {
                f_camera_joystick_focus_detach_move_to_direction("up");
            }
            else if (Input.mousePosition.y >= ((Screen.height / 2) + ((Screen.height / 2) / v_camera_joystick_focus_detach_setup.v_focus_detach_distance_threshold)))
            {
                f_camera_joystick_focus_detach_move_to_direction("down");
            }
            if (Input.mousePosition.x < ((Screen.width / 2) - ((Screen.width / 2) / v_camera_joystick_focus_detach_setup.v_focus_detach_distance_threshold)))
            {
                f_camera_joystick_focus_detach_move_to_direction("right");
            }
            else if (Input.mousePosition.x >= ((Screen.width / 2) + ((Screen.width / 2) / v_camera_joystick_focus_detach_setup.v_focus_detach_distance_threshold)))
            {
                f_camera_joystick_focus_detach_move_to_direction("left");
            }

            transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, (v_camera_joystick_focus_setup.v_focus_gameobject.transform.position.x - v_camera_joystick_focus_detach_setup.v_focus_detach_clamp), (v_camera_joystick_focus_setup.v_focus_gameobject.transform.position.x + v_camera_joystick_focus_detach_setup.v_focus_detach_clamp)),
                transform.position.y,
                Mathf.Clamp(transform.position.z, (v_camera_joystick_focus_setup.v_focus_gameobject.transform.position.z - v_camera_joystick_focus_detach_setup.v_focus_detach_clamp), (v_camera_joystick_focus_setup.v_focus_gameobject.transform.position.z + v_camera_joystick_focus_detach_setup.v_focus_detach_clamp))
            );
        }
    }

    public void f_camera_joystick_focus_detach_move_to_direction(string sv_direction_target)
    {
        switch (sv_direction_target)
        {
            case "up":
                f_camera_joystick_focus_detach_move((transform.forward), v_camera_joystick_focus_detach_setup.v_focus_detach_lerp_speed);
                break;
            case "down":
                f_camera_joystick_focus_detach_move((transform.forward * -1), v_camera_joystick_focus_detach_setup.v_focus_detach_lerp_speed);
                break;
            case "left":
                f_camera_joystick_focus_detach_move((transform.right * -1), v_camera_joystick_focus_detach_setup.v_focus_detach_lerp_speed);
                break;
            case "right":
                f_camera_joystick_focus_detach_move((transform.right), v_camera_joystick_focus_detach_setup.v_focus_detach_lerp_speed);
                break;
            default:
                break;
        }
    }

    public void f_camera_joystick_focus_detach_move(Vector3 sv_direction, float sv_speed)
    {
        transform.Translate(sv_direction * sv_speed * Time.deltaTime);
    }
}
