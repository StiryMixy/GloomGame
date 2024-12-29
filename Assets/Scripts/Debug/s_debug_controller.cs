using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class sgvl_debug_controller
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_debug_manager_gameobject_name;
    [SerializeField] public List<GameObject> v_debug_gameobjects_list;
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_debug_manager_gameobject;
    [SerializeField] public s_debug_controller v_debug_manager_gameobject_script;
}

public class s_debug_controller : MonoBehaviour
{
    [Header("debug Key Manager Object Setup")]
    [SerializeField] public svgl_key_manager v_debug_key_manager_gameobject_setup = new svgl_key_manager();
    [Header("Debug Setup")]
    [SerializeField] public bool v_debug_renderers_enabled = true;

    void Start()
    {
        f_debug_gameobject_finder();
    }

    void Update()
    {
        if (f_player_collider_keyup_verify(v_debug_key_manager_gameobject_setup.v_key_manager_gameobject_script.v_key_manager_debug_test_setup.v_debug_test_key_1))
        {
            v_debug_renderers_enabled = !v_debug_renderers_enabled;
        }
    }

    public void f_debug_gameobject_finder()
    {
        v_debug_key_manager_gameobject_setup.v_key_manager_gameobject = GameObject.Find(v_debug_key_manager_gameobject_setup.v_key_manager_gameobject_name);
        v_debug_key_manager_gameobject_setup.v_key_manager_gameobject_script = v_debug_key_manager_gameobject_setup.v_key_manager_gameobject.GetComponent<s_key_manager>();
    }

    public bool f_player_collider_keyup_verify(KeyCode sv_key)
    {
        if (Input.GetKeyUp(sv_key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void f_debug_renderer_controller(List<GameObject> sv_list)
    {
        foreach (GameObject item in sv_list)
        {
            foreach (Renderer r in item.GetComponentsInChildren<Renderer>())
            {
                r.enabled = v_debug_renderers_enabled;
            }
        }
    }
}
