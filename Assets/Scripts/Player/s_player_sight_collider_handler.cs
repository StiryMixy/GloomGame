using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_player_sight_collider_handler : MonoBehaviour
{
    [Header("Sight Collider Handler Debug Setup")]
    [SerializeField] public sgvl_debug_full_controller v_sight_collider_handler_debug_render_setup = new sgvl_debug_full_controller();
    
    void Start()
    {
        f_ground_handler_gameobject_finder();
    }

    void Update()
    {
        v_sight_collider_handler_debug_render_setup.v_debug_manager_gameobject_script.f_debug_renderer_controller(v_sight_collider_handler_debug_render_setup.v_debug_gameobjects_list);
    }

    public void f_ground_handler_gameobject_finder()
    {
        v_sight_collider_handler_debug_render_setup.v_debug_manager_gameobject = GameObject.Find(v_sight_collider_handler_debug_render_setup.v_debug_manager_gameobject_name);
        v_sight_collider_handler_debug_render_setup.v_debug_manager_gameobject_script = v_sight_collider_handler_debug_render_setup.v_debug_manager_gameobject.GetComponent<s_debug_controller>();
    }
}
