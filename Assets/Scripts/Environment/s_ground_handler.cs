using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class svl_ground_handler
{
    [Header("Configurable Variables")]
    [SerializeField] public s_sprite_handler v_ground_handler_target_script;
    [SerializeField] public float v_ground_handler_target_alpha;
}

public class s_ground_handler : MonoBehaviour
{
    [Header("Ground Handler Setup")]
    [SerializeField] public svl_ground_handler v_ground_handler_setup = new svl_ground_handler();

    [Header("Ground Handler Debug Setup")]
    [SerializeField] public sgvl_debug_half_controller v_ground_handler_debug_render_setup = new sgvl_debug_half_controller();

    void Start()
    {
        f_ground_handler_gameobject_finder();
    }

    void Update()
    {
        if (v_ground_handler_debug_render_setup.v_debug_manager_gameobject_script.f_debug_renderer_attach())
        {
            v_ground_handler_setup.v_ground_handler_target_script.v_sprite_alpha_setup.v_sprite_alpha_target = v_ground_handler_setup.v_ground_handler_target_alpha;
        }
        else
        {
            v_ground_handler_setup.v_ground_handler_target_script.v_sprite_alpha_setup.v_sprite_alpha_target = 1.0f;
            v_ground_handler_setup.v_ground_handler_target_script.v_sprite_alpha_setup.v_sprite_alpha = 1.0f;
        }
    }

    public void f_ground_handler_gameobject_finder()
    {
        v_ground_handler_debug_render_setup.v_debug_manager_gameobject = GameObject.Find(v_ground_handler_debug_render_setup.v_debug_manager_gameobject_name);
        v_ground_handler_debug_render_setup.v_debug_manager_gameobject_script = v_ground_handler_debug_render_setup.v_debug_manager_gameobject.GetComponent<s_debug_controller>();
    }

}
