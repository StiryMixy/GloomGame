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

public class s_camera : MonoBehaviour
{
    [Header("Camera Game Object Setup")]
    [SerializeField] public svl_camera_actualcamera v_camera_actualcamera_gameobject_setup = new svl_camera_actualcamera();

    [Header("Camera Height Setup")]
    [SerializeField] public svl_camera_height v_camera_height_setup = new svl_camera_height();

    [Header("Camera Focus Setup")]
    [SerializeField] public svl_camera_focus v_camera_focus_setup = new svl_camera_focus();

    [Header("Camera Debug Setup")]
    [SerializeField] public sgvl_debug_controller v_camera_debug_render_setup = new sgvl_debug_controller();

    void Start()
    {
        f_camera_gameobject_finder();
        v_camera_height_setup.v_zoom_check = f_camera_height_controller(true);
    }

    void Update()
    {
        v_camera_height_setup.v_zoom_check = f_camera_height_controller(false);
        v_camera_focus_setup.v_focus_check = f_camera_smoothly_move_towards();
        f_camera_debug_renderer_controller(v_camera_debug_render_setup.v_debug_manager_gameobject_script.v_debug_renderers_enabled);
    }
    
    public void f_camera_gameobject_finder()
    {
        v_camera_focus_setup.v_focus_gameobject = GameObject.Find(v_camera_focus_setup.v_focus_gameobject_name);
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

    public void f_camera_debug_renderer_controller(bool sv_is_allowed)
    {
        foreach (GameObject item in v_camera_debug_render_setup.v_debug_camera_gameobjects)
        {
            foreach (Renderer r in item.GetComponentsInChildren<Renderer>())
            {
                r.enabled = sv_is_allowed;
            }
        }
    }
}
