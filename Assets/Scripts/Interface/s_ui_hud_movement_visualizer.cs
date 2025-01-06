using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[Serializable]
public class svl_movement_visualizer
{
    [Header("Configurable Variables")]
    [SerializeField] public s_player_collider_controller v_movement_visualizer_player_collider_gameobject_script;
    [SerializeField] public UnityEngine.UI.Image v_movement_visualizer_self_image_script;
    [Space(10)]
    [SerializeField] public Sprite v_movement_visualizer_walk_sprite;
    [SerializeField] public Sprite v_movement_visualizer_run_sprite;
}

public class s_ui_hud_movement_visualizer : MonoBehaviour
{
    [Header("Movement Visualizer Setup")]
    [SerializeField] public svl_movement_visualizer v_movement_visualizer_setup = new svl_movement_visualizer();

    void Update()
    {
        if (v_movement_visualizer_setup.v_movement_visualizer_player_collider_gameobject_script.v_player_collider_movement_setup.v_player_collider_movement_speed_setup.v_player_collider_movement_speed_is_walking)
        {
            v_movement_visualizer_setup.v_movement_visualizer_self_image_script.sprite = v_movement_visualizer_setup.v_movement_visualizer_walk_sprite;
        }
        else
        {
            v_movement_visualizer_setup.v_movement_visualizer_self_image_script.sprite = v_movement_visualizer_setup.v_movement_visualizer_run_sprite;
        }
    }
}
