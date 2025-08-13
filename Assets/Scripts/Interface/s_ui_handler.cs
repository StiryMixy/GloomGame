using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class svl_master_hud
{
    [Header("Configurable Variables")]
    [SerializeField] public GameObject v_titlesequence_hud_gameobject;
    [SerializeField] public bool v_titlesequence_hud_is_visible;
    [Range(0.0f, 1.0f)][SerializeField] public float v_titlesequence_hud_alpha_target_master_max;
    [Range(0.0f, 1.0f)][SerializeField] public float v_titlesequence_hud_alpha_target_master_min;
    [SerializeField] public s_ui_hud_titlesequence_visualizer v_titlesequence_hud_gameobject_script;
    [Space(10)]
    [SerializeField] public GameObject v_player_hud_gameobject;
    [SerializeField] public bool v_player_hud_is_visible;
    [Range(0.0f, 1.0f)][SerializeField] public float v_player_hud_alpha_target_master_max;
    [Range(0.0f, 1.0f)][SerializeField] public float v_player_hud_alpha_target_master_min;
    [Header("Reference Variables")]
    [Range(0.0f, 1.0f)][SerializeField] public float v_titlesequence_hud_alpha_target_master;
    [SerializeField] public bool v_titlesequence_hud_is_visible_target;
    [Space(10)]
    [Range(0.0f, 1.0f)][SerializeField] public float v_player_hud_alpha_target_master;
    [SerializeField] public bool v_player_hud_is_visible_target;
}

[Serializable]
public class svl_ui_caller
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_ui_gameobject_name;
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_ui_gameobject;
    [SerializeField] public s_ui_handler v_ui_gameobject_script;
}

public class s_ui_handler : MonoBehaviour
{
    [Header("Master Hud Setup")]
    [SerializeField] public svl_master_hud v_master_hud_setup = new svl_master_hud();

    void Update()
    {
        if (v_master_hud_setup.v_titlesequence_hud_is_visible_target != v_master_hud_setup.v_titlesequence_hud_is_visible)
        {
            v_master_hud_setup.v_titlesequence_hud_is_visible_target = v_master_hud_setup.v_titlesequence_hud_is_visible;

            if (v_master_hud_setup.v_titlesequence_hud_is_visible_target)
            {
                v_master_hud_setup.v_titlesequence_hud_alpha_target_master = v_master_hud_setup.v_titlesequence_hud_alpha_target_master_max;
            }
            else
            {
                v_master_hud_setup.v_titlesequence_hud_alpha_target_master = v_master_hud_setup.v_titlesequence_hud_alpha_target_master_min;
            }

            foreach (s_ui_hud_image_alpha_handler alpha_script in v_master_hud_setup.v_titlesequence_hud_gameobject.GetComponentsInChildren<s_ui_hud_image_alpha_handler>())
            {
                alpha_script.v_image_alpha_handler_setup.v_image_alpha_target = v_master_hud_setup.v_titlesequence_hud_alpha_target_master;
            }
        }

        if (v_master_hud_setup.v_player_hud_is_visible_target != v_master_hud_setup.v_player_hud_is_visible)
        {
            v_master_hud_setup.v_player_hud_is_visible_target = v_master_hud_setup.v_player_hud_is_visible;

            if (v_master_hud_setup.v_player_hud_is_visible_target)
            {
                v_master_hud_setup.v_player_hud_alpha_target_master = v_master_hud_setup.v_player_hud_alpha_target_master_max;
            }
            else
            {
                v_master_hud_setup.v_player_hud_alpha_target_master = v_master_hud_setup.v_player_hud_alpha_target_master_min;
            }

            foreach (s_ui_hud_image_alpha_handler alpha_script in v_master_hud_setup.v_player_hud_gameobject.GetComponentsInChildren<s_ui_hud_image_alpha_handler>())
            {
                alpha_script.v_image_alpha_handler_setup.v_image_alpha_target = v_master_hud_setup.v_player_hud_alpha_target_master;
            }
        }
    }

    public void f_scene_reset_action()
    {
        v_master_hud_setup.v_titlesequence_hud_is_visible = false;
        v_master_hud_setup.v_titlesequence_hud_gameobject_script.v_titlesequence_visualizer_setup.v_titlesequence_visualizer_is_enabled = false;
    }
}
