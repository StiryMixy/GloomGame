using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class svl_pathing_state_visualizer
{
    [Header("Configurable Variables")]
    [SerializeField] public s_key_manager v_pathing_state_visualizer_key_manager_gameobject_script;
    [SerializeField] public UnityEngine.UI.Image v_pathing_state_visualizer_self_image_script;
    [Space(10)]
    [SerializeField] public Sprite v_pathing_state_visualizer_on_sprite;
    [SerializeField] public Sprite v_pathing_state_visualizer_off_sprite;
}

public class s_ui_hud_pathing_state_visualizer : MonoBehaviour
{
    [Header("Pathing State Visualizer Setup")]
    [SerializeField] public svl_pathing_state_visualizer v_pathing_state_visualizer_setup = new svl_pathing_state_visualizer();

    void Update()
    {
        if (v_pathing_state_visualizer_setup.v_pathing_state_visualizer_key_manager_gameobject_script.v_key_manager_pathing_render_setup.v_pathing_render_enable)
        {
            v_pathing_state_visualizer_setup.v_pathing_state_visualizer_self_image_script.sprite = v_pathing_state_visualizer_setup.v_pathing_state_visualizer_on_sprite;
        }
        else
        {
            v_pathing_state_visualizer_setup.v_pathing_state_visualizer_self_image_script.sprite = v_pathing_state_visualizer_setup.v_pathing_state_visualizer_off_sprite;
        }
    }
}
