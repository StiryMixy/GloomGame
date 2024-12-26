using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_entity_camera : MonoBehaviour
{
    [Header("Camera Object Setup")]
    public GameObject v_camera_pivot_gameobject;
    public GameObject v_actualcamera_gameobject;

    [Header("Camera Height Variables")]
    public float v_camera_height_clamp = 10.0f;
    public float v_camera_zoom_speed = 1.5f;
    public float v_camera_zoom_distance_threshold = 0.01f;
    public float v_camera_zoom_accuracy_gauge = -0.0001f;
    public Vector3 v_camera_zoom_current_position = Vector3.zero;
    public Vector3 v_camera_zoom_target_position = Vector3.zero;
    public float v_camera_zoom_difference = 0.0f;
    public bool v_camera_zoom_check = false;

    [Header("Camera Focus Variables")]
    public string v_camera_focus_gameobject_name;
    public string v_camera_focus_gameobject_name_default;
    public GameObject v_camera_focus_gameobject;
    public float v_camera_focus_lerp_speed = 1.0f;
    public float v_camera_focus_distance_threshold = 0.1f;
    public bool v_camera_focus_check = false;

    [Header("Camera Debug Setup")]
    public bool v_debug_render_enabled = false;
    public List<GameObject> v_debug_camera_gameobjects;
    
    // Start is called before the first frame update
    void Start()
    {
        v_camera_zoom_check = f_actualcamera_height_controller(true);
    }

    // Update is called once per frame
    void Update()
    {
        f_camera_focus_gameobject_finder();
        v_camera_zoom_check = f_actualcamera_height_controller(false);
        v_camera_focus_check = f_camera_smoothly_move_towards();
        f_camera_debug_renderer_controller(v_debug_render_enabled);
    }

    public void f_camera_focus_gameobject_finder()
    {
        v_camera_focus_gameobject = GameObject.Find(v_camera_focus_gameobject_name);
        if (v_camera_focus_gameobject == null)
        {
            v_camera_focus_gameobject = GameObject.Find(v_camera_focus_gameobject_name_default);
        }
    }

    public bool f_actualcamera_height_controller(bool sv_is_instant)
    {
        if (v_camera_zoom_target_position.y != v_camera_height_clamp)
        {
            if (v_camera_zoom_target_position.y < v_camera_height_clamp)
            {
                if ((v_camera_height_clamp - v_camera_zoom_target_position.y) > v_camera_zoom_distance_threshold)
                {
                    Vector3 tv_position_to_add = (v_actualcamera_gameobject.transform.parent.InverseTransformDirection(v_actualcamera_gameobject.transform.forward)) * v_camera_zoom_accuracy_gauge;
                    while (v_camera_zoom_target_position.y < v_camera_height_clamp)
                    {
                        v_camera_zoom_target_position += tv_position_to_add;
                    }
                }
            }
            else if (v_camera_zoom_target_position.y > v_camera_height_clamp)
            {
                if ((v_camera_zoom_target_position.y - v_camera_height_clamp) > v_camera_zoom_distance_threshold)
                {
                    Vector3 tv_position_to_add = (v_actualcamera_gameobject.transform.parent.InverseTransformDirection(v_actualcamera_gameobject.transform.forward)) * v_camera_zoom_accuracy_gauge;
                    while (v_camera_zoom_target_position.y > v_camera_height_clamp)
                    {
                        v_camera_zoom_target_position -= tv_position_to_add;
                    }
                }
            }
        }

        v_camera_zoom_target_position.x = 0.0f;
        v_camera_zoom_current_position = v_actualcamera_gameobject.transform.localPosition;
        v_camera_zoom_difference = Vector3.Distance(v_camera_zoom_current_position, v_camera_zoom_target_position);
        if (sv_is_instant)
        {
            if (v_actualcamera_gameobject.transform.localPosition != v_camera_zoom_target_position)
            {
                v_actualcamera_gameobject.transform.localPosition = v_camera_zoom_target_position;
            }
        }
        else
        {
            if (v_actualcamera_gameobject.transform.localPosition != v_camera_zoom_target_position)
            {
                if (v_camera_zoom_difference >= v_camera_zoom_distance_threshold)
                {
                    v_actualcamera_gameobject.transform.localPosition = Vector3.Lerp(v_actualcamera_gameobject.transform.localPosition, v_camera_zoom_target_position, v_camera_zoom_speed * Time.deltaTime);
                }
                else
                {
                    v_actualcamera_gameobject.transform.localPosition = v_camera_zoom_target_position;
                }
            }
        }
        return (v_actualcamera_gameobject.transform.localPosition.Equals(v_camera_zoom_target_position));
    }

    public bool f_camera_smoothly_move_towards()
    {
        transform.position = Vector3.Lerp(transform.position, v_camera_focus_gameobject.transform.position, v_camera_focus_lerp_speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, v_camera_focus_gameobject.transform.position) < v_camera_focus_distance_threshold)
        {
            return true;
        }
        return false;
    }

    public void f_camera_debug_renderer_controller(bool sv_is_allowed)
    {
        foreach (GameObject item in v_debug_camera_gameobjects)
        {
            foreach (Renderer r in item.GetComponentsInChildren<Renderer>())
            {
                r.enabled = sv_is_allowed;
            }
        }
    }
}
