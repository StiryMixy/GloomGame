using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_pathing_render
{
    [Header("Configurable Variables")]
    [SerializeField] public s_sprite_handler v_sprite_handler_gameobject_script;
    [Header("Reference Variables")]
    [SerializeField] public bool v_pathing_render_enable = false;
}

public class s_pathing : MonoBehaviour
{
    [Header("Pathing Key Manager Object Setup")]
    [SerializeField] public svgl_key_manager v_pathing_key_manager_gameobject_setup = new svgl_key_manager();

    [Header("Pathing Render Setup")]
    [SerializeField] public svl_pathing_render v_pathing_render_setup = new svl_pathing_render();

    [Header("Pathing Type Setup")]
    [SerializeField] public v_tags_movement_mode_list v_pathing_type;

    [Header("Pathing Collider References")]
    [SerializeField] public List<GameObject> v_pathing_collider_current_collisions_list;

    void Start()
    {
        f_pathing_gameobject_finder();
    }

    void Update()
    {
        v_pathing_render_setup.v_pathing_render_enable = f_pathing_render_controller(v_pathing_render_setup.v_pathing_render_enable);

        if (v_pathing_render_setup.v_pathing_render_enable)
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_global = true;
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_update_on_timer = false;
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target = v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_max;
        }
        else
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_global = true;
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_update_on_timer = false;
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target = v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_alpha_setup.v_sprite_alpha_target_min;
        }

        if (v_pathing_type.Equals(v_tags_movement_mode_list.WalkingAndFlying))
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_frame_setup.v_frame_counter = 0;
        }
        else if (v_pathing_type.Equals(v_tags_movement_mode_list.Flying))
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_frame_setup.v_frame_counter = 1;
        }
        else if (v_pathing_type.Equals(v_tags_movement_mode_list.Walking))
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_frame_setup.v_frame_counter = 2;
        }
        else if (v_pathing_type.Equals(v_tags_movement_mode_list.None))
        {
            v_pathing_render_setup.v_sprite_handler_gameobject_script.v_sprite_frame_setup.v_frame_counter = 3;
        }
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        if (!v_pathing_collider_current_collisions_list.Contains(sv_other_object.gameObject))
        {
            v_pathing_collider_current_collisions_list.Add(sv_other_object.gameObject);
        }
    }

    private void OnTriggerStay(Collider sv_other_object)
    {

    }

    private void OnTriggerExit(Collider sv_other_object)
    {
        if (v_pathing_collider_current_collisions_list.Count > 0)
        {
            if (v_pathing_collider_current_collisions_list.Contains(sv_other_object.gameObject))
            {
                v_pathing_collider_current_collisions_list.Remove(sv_other_object.gameObject);
            }
        }
    }

    public void f_pathing_gameobject_finder()
    {
        v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject = GameObject.Find(v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject_name);
        v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject_script = v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject.GetComponent<s_key_manager>();
    }

    public bool f_pathing_render_controller(bool sv_pathing_render)
    {
        if (v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_pathing_render_setup.v_pathing_render_key_press_mode.Equals(v_tags_key_press_mode_list.Toggle))
        {
            if (Input.GetKeyDown(v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_pathing_render_setup.v_pathing_render_key))
            {
                return (!sv_pathing_render);
            }
            else
            {
                return (sv_pathing_render);
            }
        }
        else if (v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_pathing_render_setup.v_pathing_render_key_press_mode.Equals(v_tags_key_press_mode_list.Hold))
        {
            if (Input.GetKey(v_pathing_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_pathing_render_setup.v_pathing_render_key))
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
