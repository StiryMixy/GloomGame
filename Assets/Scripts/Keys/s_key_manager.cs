using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svgl_key_manager
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_key_manager_gameobject_name;
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_key_manager_gameobject;
    [SerializeField] public s_key_manager v_key_manager_gameobject_script;
}

[Serializable]
public class svl_key_manager_camera_joystick_focus_detach
{
    [Header("Configurable Variables")]
    [SerializeField] public KeyCode v_camera_joystick_focus_detach_key;
    [SerializeField] public v_tags_key_press_mode_list v_camera_joystick_focus_detach_key_press_mode;
}

[Serializable]
public class svl_key_manager_pathing_render
{
    [Header("Configurable Variables")]
    [SerializeField] public KeyCode v_pathing_render_key;
    [SerializeField] public v_tags_key_press_mode_list v_pathing_render_key_press_mode;
    [Header("Reference Variables")]
    [SerializeField] public bool v_pathing_render_enable;
}

[Serializable]
public class svl_key_manager_player_movement
{
    [Header("Configurable Variables")]
    [SerializeField] public KeyCode Forward;
    [SerializeField] public KeyCode Left;
    [SerializeField] public KeyCode Backward;
    [SerializeField] public KeyCode Right;
    [Space(10)]
    [SerializeField] public KeyCode ToggleRunWalk;
    [Space(10)]
    [SerializeField] public KeyCode Dodge;
}

[Serializable]
public class svl_key_manager_debug_test
{
    [Header("Configurable Variables")]
    [SerializeField] public KeyCode v_debug_test_key_1;
    [SerializeField] public KeyCode v_debug_test_key_2;
    [SerializeField] public KeyCode v_debug_test_key_3;
    [SerializeField] public KeyCode v_debug_test_key_4;
    [SerializeField] public KeyCode v_debug_test_key_5;
}

[Serializable]
public class svl_key_manager_detect_key
{
    [Header("WARNING! CAUSES HUGE FPS LOSS!")]
    [Header("Configurable Variables")]
    [SerializeField] public bool v_key_manager_function_enabled = false;
    [SerializeField] public bool v_key_manager_detect_keys = false;
    [Header("Reference Variables")]
    [SerializeField] public List<KeyCode> v_key_manager_detected_key = new List<KeyCode>();
    [SerializeField] public KeyCode[] v_key_manager_enum_values;
}

public class s_key_manager : MonoBehaviour
{
    [Header("Key Manager Camera Joystick Focus Detach Setup")]
    [SerializeField] public svl_key_manager_detect_key v_key_manager_detect_key_setup = new svl_key_manager_detect_key();
    [Header("Key Manager Camera Joystick Focus Detach Setup")]
    [SerializeField] public svl_key_manager_camera_joystick_focus_detach v_key_manager_camera_joystick_focus_detach_setup = new svl_key_manager_camera_joystick_focus_detach();
    [Header("Key Manager Pathing Render Setup")]
    [SerializeField] public svl_key_manager_pathing_render v_key_manager_pathing_render_setup = new svl_key_manager_pathing_render();
    [Header("Key Manager Player Movement Setup")]
    [SerializeField] public svl_key_manager_player_movement v_key_manager_player_movement_setup = new svl_key_manager_player_movement();
    [Header("Key Manager Debug Test Setup")]
    [SerializeField] public svl_key_manager_debug_test v_key_manager_debug_test_setup = new svl_key_manager_debug_test();

    void Update()
    {
        v_key_manager_pathing_render_setup.v_pathing_render_enable = f_pathing_render_controller(v_key_manager_pathing_render_setup.v_pathing_render_enable);
        if (v_key_manager_detect_key_setup.v_key_manager_function_enabled)
        {
            f_key_manager_detect_key_module();
        }
    }

    public void f_key_manager_detect_key_module()
    {
        if (v_key_manager_detect_key_setup.v_key_manager_detect_keys)
        {
            if (v_key_manager_detect_key_setup.v_key_manager_enum_values.Length <= 0)
            {
                v_key_manager_detect_key_setup.v_key_manager_enum_values = (KeyCode[])Enum.GetValues(typeof(KeyCode));
            }

            foreach (KeyCode lv_kcode in v_key_manager_detect_key_setup.v_key_manager_enum_values)
            {
                if (Input.GetKey(lv_kcode))
                {
                    if (!v_key_manager_detect_key_setup.v_key_manager_detected_key.Contains(lv_kcode))
                    {
                        v_key_manager_detect_key_setup.v_key_manager_detected_key.Insert(0, lv_kcode);
                    }
                }
                else
                {
                    if (v_key_manager_detect_key_setup.v_key_manager_detected_key.Count > 0)
                    {
                        if (v_key_manager_detect_key_setup.v_key_manager_detected_key.Contains(lv_kcode))
                        {
                            v_key_manager_detect_key_setup.v_key_manager_detected_key.Remove(lv_kcode);
                        }
                    }
                }
            }
        }
        else
        {
            if (v_key_manager_detect_key_setup.v_key_manager_enum_values.Length > 0)
            {
                v_key_manager_detect_key_setup.v_key_manager_enum_values = new KeyCode[0];
            }
        }
    }

    public bool f_pathing_render_controller(bool sv_pathing_render)
    {
        if (v_key_manager_pathing_render_setup.v_pathing_render_key_press_mode.Equals(v_tags_key_press_mode_list.Toggle))
        {
            if (Input.GetKeyDown(v_key_manager_pathing_render_setup.v_pathing_render_key))
            {
                return (!sv_pathing_render);
            }
            else
            {
                return (sv_pathing_render);
            }
        }
        else if (v_key_manager_pathing_render_setup.v_pathing_render_key_press_mode.Equals(v_tags_key_press_mode_list.Hold))
        {
            if (Input.GetKey(v_key_manager_pathing_render_setup.v_pathing_render_key))
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
            return (sv_pathing_render);
        }
    }

}
