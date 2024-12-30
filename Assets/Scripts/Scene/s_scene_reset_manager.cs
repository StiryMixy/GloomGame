using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class svl_scene_reset_targets
{
    [Header("Configurable Variables")]
    [SerializeField] public s_camera_joystick v_scene_camera_joystick_target;
    [SerializeField] public s_camera v_scene_camera_target;
    [SerializeField] public s_player_collider_controller v_scene_player_collider_controller_target;
    [SerializeField] public s_player_handler v_scene_player_handler_target;
}

[Serializable]
public class svl_scene_reset_manager_focus
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_focus_gameobject_name;
    [SerializeField] public float v_focus_lerp_speed = 1.0f;
    [SerializeField] public float v_focus_distance_threshold = 0.1f;
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_focus_gameobject;
    [SerializeField] public bool v_focus_check = false;
}

public class s_scene_reset_manager : MonoBehaviour
{
    [Header("Scene Reset Manager Targets Setup")]
    [SerializeField] public svl_scene_reset_targets v_scene_reset_manager_targets_setup = new svl_scene_reset_targets();

    [Header("Scene Reset Manager Focus Setup")]
    [SerializeField] public svl_scene_reset_manager_focus v_scene_reset_manager_focus_setup = new svl_scene_reset_manager_focus();

    [Header("Scene Reset Manager Debug Setup")]
    [SerializeField] public sgvl_debug_full_controller v_scene_reset_manager_debug_render_setup = new sgvl_debug_full_controller();

    void Start()
    {
        f_camera_gameobject_finder();
    }

    void Update()
    {
        v_scene_reset_manager_focus_setup.v_focus_check = f_camera_smoothly_move_towards();
        v_scene_reset_manager_debug_render_setup.v_debug_manager_gameobject_script.f_debug_renderer_controller(v_scene_reset_manager_debug_render_setup.v_debug_gameobjects_list);
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        
    }

    private void OnTriggerStay(Collider sv_other_object)
    {

    }

    private void OnTriggerExit(Collider sv_other_object)
    {
        
    }

    public void f_camera_gameobject_finder()
    {
        v_scene_reset_manager_focus_setup.v_focus_gameobject = GameObject.Find(v_scene_reset_manager_focus_setup.v_focus_gameobject_name);
        v_scene_reset_manager_debug_render_setup.v_debug_manager_gameobject = GameObject.Find(v_scene_reset_manager_debug_render_setup.v_debug_manager_gameobject_name);
        v_scene_reset_manager_debug_render_setup.v_debug_manager_gameobject_script = v_scene_reset_manager_debug_render_setup.v_debug_manager_gameobject.GetComponent<s_debug_controller>();
    }

    public bool f_camera_smoothly_move_towards()
    {
        transform.position = Vector3.Lerp(transform.position, v_scene_reset_manager_focus_setup.v_focus_gameobject.transform.position, v_scene_reset_manager_focus_setup.v_focus_lerp_speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, v_scene_reset_manager_focus_setup.v_focus_gameobject.transform.position) < v_scene_reset_manager_focus_setup.v_focus_distance_threshold)
        {
            return true;
        }
        return false;
    }

    public void f_scene_reset_action()
    {
        v_scene_reset_manager_targets_setup.v_scene_camera_joystick_target.f_scene_reset_action();
        v_scene_reset_manager_targets_setup.v_scene_camera_target.f_scene_reset_action();
        v_scene_reset_manager_targets_setup.v_scene_player_collider_controller_target.f_scene_reset_action();
        v_scene_reset_manager_targets_setup.v_scene_player_handler_target.f_scene_reset_action();
        transform.position = Vector3.zero;
    }
}
