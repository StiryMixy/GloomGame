using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[Serializable]
public class svl_player_hud
{
    [Header("Configurable Variables")]
    [SerializeField] public GameObject v_player_hud_gameobject;
    [SerializeField] public bool v_player_hud_is_visible;
    [Range(0.0f, 1.0f)][SerializeField] public float v_player_hud_alpha_target_master_max;
    [Range(0.0f, 1.0f)][SerializeField] public float v_player_hud_alpha_target_master_min;
    [Header("Reference Variables")]
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
    [Header("Player Hud Setup")]
    [SerializeField] public svl_player_hud v_player_hud_setup = new svl_player_hud();

    void Update()
    {
        if (v_player_hud_setup.v_player_hud_is_visible_target != v_player_hud_setup.v_player_hud_is_visible)
        {
            v_player_hud_setup.v_player_hud_is_visible_target = v_player_hud_setup.v_player_hud_is_visible;

            if (v_player_hud_setup.v_player_hud_is_visible_target)
            {
                v_player_hud_setup.v_player_hud_alpha_target_master = v_player_hud_setup.v_player_hud_alpha_target_master_max;
            }
            else
            {
                v_player_hud_setup.v_player_hud_alpha_target_master = v_player_hud_setup.v_player_hud_alpha_target_master_min;
            }

            foreach (s_ui_hud_image_alpha_handler alpha_script in v_player_hud_setup.v_player_hud_gameobject.GetComponentsInChildren<s_ui_hud_image_alpha_handler>())
            {
                alpha_script.v_image_alpha_handler_setup.v_image_alpha_target = v_player_hud_setup.v_player_hud_alpha_target_master;
            }
        }
    }
}
