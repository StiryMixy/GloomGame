using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_entity_tag_library;

public class s_entity_key_manager : MonoBehaviour
{
    [Header("Camera Joystick Focus Key Bind Setup")]
    public KeyCode v_camera_joystick_focus_detach_key;
    public v_key_press_mode_list v_camera_joystick_focus_detach_key_press_mode;

    [Header("Path Indicators Render Key Bind Setup")]
    public KeyCode v_pathindicator_alpha_render_key;
    public v_key_press_mode_list v_pathindicator_alpha_render_key_press_mode;

    [Header("Player Movement Key Bind Setup")]
    public KeyCode v_player_move_up_key;
    public KeyCode v_player_move_left_key;
    public KeyCode v_player_move_down_key;
    public KeyCode v_player_move_right_key;
}
