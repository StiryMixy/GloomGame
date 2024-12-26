using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_entity_tag_library;

public class s_entity_camera_joystick : MonoBehaviour
{
    [Header("Camera Joystick Focus Variables")]
    public bool v_camera_joystick_focus_enable = true;
    public string v_camera_joystick_focus_gameobject_name;
    public string v_camera_joystick_focus_gameobject_name_default;
    public GameObject v_camera_joystick_focus_gameobject;
    public float v_camera_joystick_focus_distance_threshold = 1.25f;
    public float v_camera_joystick_focus_lerp_speed = 10.0f;
    public float v_camera_joystick_focus_clamp = 3.0f;

    [Header("Camera Joystick Focus Key Bind Setup")]
    public string v_key_manager_gameobject_name;
    public GameObject v_key_manager_gameobject;
    public bool v_camera_joystick_focus_detach = false;
    public KeyCode v_camera_joystick_focus_detach_key;
    public v_key_press_mode_list v_camera_joystick_focus_detach_key_press_mode;

    [Header("Camera Joystick Debug Setup")]
    public bool v_debug_render_enabled;
    public List<GameObject> v_debug_camera_joystick_gameobjects;

    void Start()
    {
    }

    void Update()
    {
        f_camera_joystick_focus_detach_key_press_setup_refresh();

        f_camera_joystick_focus_gameobject_finder();

        if (v_camera_joystick_focus_enable && v_camera_joystick_focus_gameobject != null && !v_camera_joystick_focus_detach)
        {
            transform.position = v_camera_joystick_focus_gameobject.transform.position;
        }

        v_camera_joystick_focus_detach = f_camera_joystick_focus_detach_key_press_mode_controller(v_camera_joystick_focus_detach);

        if (v_camera_joystick_focus_detach)
        {
            if (Input.mousePosition.y < ((Screen.height / 2) - ((Screen.height / 2) / v_camera_joystick_focus_distance_threshold)))
            {
                f_camera_joystick_move_to_direction("up");
            }
            else if (Input.mousePosition.y >= ((Screen.height / 2) + ((Screen.height / 2) / v_camera_joystick_focus_distance_threshold)))
            {
                f_camera_joystick_move_to_direction("down");
            }
            if (Input.mousePosition.x < ((Screen.width / 2) - ((Screen.width / 2) / v_camera_joystick_focus_distance_threshold)))
            {
                f_camera_joystick_move_to_direction("right");
            }
            else if (Input.mousePosition.x >= ((Screen.width / 2) + ((Screen.width / 2) / v_camera_joystick_focus_distance_threshold)))
            {
                f_camera_joystick_move_to_direction("left");
            }
            
            if (v_camera_joystick_focus_gameobject != null)
            {
                transform.position = new Vector3
                (
                    Mathf.Clamp(transform.position.x, (v_camera_joystick_focus_gameobject.transform.position.x - v_camera_joystick_focus_clamp), (v_camera_joystick_focus_gameobject.transform.position.x + v_camera_joystick_focus_clamp)),
                    transform.position.y,
                    Mathf.Clamp(transform.position.z, (v_camera_joystick_focus_gameobject.transform.position.z - v_camera_joystick_focus_clamp), (v_camera_joystick_focus_gameobject.transform.position.z + v_camera_joystick_focus_clamp))
                );
            }
        }

        f_camera_joystick_debug_renderer_controller(v_debug_render_enabled);
    }

    public void f_camera_joystick_focus_gameobject_finder()
    {
        v_camera_joystick_focus_gameobject = GameObject.Find(v_camera_joystick_focus_gameobject_name);
        if (v_camera_joystick_focus_gameobject == null)
        {
            v_camera_joystick_focus_gameobject = GameObject.Find(v_camera_joystick_focus_gameobject_name_default);
        }
    }

    public void f_camera_joystick_move_to_direction(string sv_direction_target)
    {
        switch (sv_direction_target)
        {
            case "up":
                f_camera_joystick_move((transform.forward), v_camera_joystick_focus_lerp_speed);
                break;
            case "down":
                f_camera_joystick_move((transform.forward * -1), v_camera_joystick_focus_lerp_speed);
                break;
            case "left":
                f_camera_joystick_move((transform.right * -1), v_camera_joystick_focus_lerp_speed);
                break;
            case "right":
                f_camera_joystick_move((transform.right), v_camera_joystick_focus_lerp_speed);
                break;
            default:
                break;
        }
    }

    public void f_camera_joystick_move(Vector3 sv_direction, float sv_speed)
    {
        transform.Translate(sv_direction * sv_speed * Time.deltaTime);
    }

    public void f_camera_joystick_focus_detach_key_press_setup_refresh()
    {
        v_key_manager_gameobject = GameObject.Find(v_key_manager_gameobject_name);
        if (v_key_manager_gameobject != null)
        {
            if (v_key_manager_gameobject.GetComponent<s_entity_key_manager>() != null)
            {
                v_camera_joystick_focus_detach_key = v_key_manager_gameobject.GetComponent<s_entity_key_manager>().v_camera_joystick_focus_detach_key;
                v_camera_joystick_focus_detach_key_press_mode = v_key_manager_gameobject.GetComponent<s_entity_key_manager>().v_camera_joystick_focus_detach_key_press_mode;
            }
        }
    }

    public bool f_camera_joystick_focus_detach_key_press_mode_controller(bool sv_camera_joystick_focus_detach)
    {
        if (v_camera_joystick_focus_detach_key_press_mode.Equals(v_key_press_mode_list.Toggle))
        {
            if (Input.GetKeyDown(v_camera_joystick_focus_detach_key))
            {
                return (!sv_camera_joystick_focus_detach);
            }
            else
            {
                return (sv_camera_joystick_focus_detach);
            }
        }
        else if (v_camera_joystick_focus_detach_key_press_mode.Equals(v_key_press_mode_list.Hold))
        {
            if (Input.GetKey(v_camera_joystick_focus_detach_key))
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

    private void f_camera_joystick_debug_renderer_controller(bool sv_is_allowed)
    {
        foreach (GameObject item in v_debug_camera_joystick_gameobjects)
        {
            foreach (Renderer r in item.GetComponentsInChildren<Renderer>())
            {
                r.enabled = sv_is_allowed;
            }
        }
    }
}
