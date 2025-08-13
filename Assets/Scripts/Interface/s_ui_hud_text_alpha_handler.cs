using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class svl_text_alpha_handler
{
    [Header("Configurable Variables")]
    [SerializeField] public UnityEngine.UI.Text v_text_script;
    [Range(0.0f, 1.0f)][SerializeField] public float v_text_alpha_target = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] public float v_text_alpha_target_max = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] public float v_text_alpha_target_min = 0.0f;
    [SerializeField] public float v_text_alpha_increment = 0.01f;
    [Header("Reference Variables")]
    [Range(0.0f, 1.0f)][SerializeField] public float v_text_alpha = 0.0f;
}

public class s_ui_hud_text_alpha_handler : MonoBehaviour
{
    [Header("Text Alpha Setup")]
    [SerializeField] public svl_text_alpha_handler v_text_alpha_handler_setup = new svl_text_alpha_handler();

    void Update()
    {
        f_text_handler_alpha_controller();
        v_text_alpha_handler_setup.v_text_script.color = new Color(v_text_alpha_handler_setup.v_text_script.color.r, v_text_alpha_handler_setup.v_text_script.color.g, v_text_alpha_handler_setup.v_text_script.color.b, v_text_alpha_handler_setup.v_text_alpha);
    }

    public void f_text_handler_alpha_controller()
    {
        if (v_text_alpha_handler_setup.v_text_alpha != v_text_alpha_handler_setup.v_text_alpha_target)
        {
            if (v_text_alpha_handler_setup.v_text_alpha > v_text_alpha_handler_setup.v_text_alpha_target)
            {
                if ((v_text_alpha_handler_setup.v_text_alpha - v_text_alpha_handler_setup.v_text_alpha_increment) < v_text_alpha_handler_setup.v_text_alpha_target)
                {
                    v_text_alpha_handler_setup.v_text_alpha = v_text_alpha_handler_setup.v_text_alpha_target;
                }
                else
                {
                    v_text_alpha_handler_setup.v_text_alpha -= v_text_alpha_handler_setup.v_text_alpha_increment;
                }
            }
            else if (v_text_alpha_handler_setup.v_text_alpha < v_text_alpha_handler_setup.v_text_alpha_target)
            {
                if ((v_text_alpha_handler_setup.v_text_alpha + v_text_alpha_handler_setup.v_text_alpha_increment) > v_text_alpha_handler_setup.v_text_alpha_target)
                {
                    v_text_alpha_handler_setup.v_text_alpha = v_text_alpha_handler_setup.v_text_alpha_target;
                }
                else
                {
                    v_text_alpha_handler_setup.v_text_alpha += v_text_alpha_handler_setup.v_text_alpha_increment;
                }
            }
        }

        if (v_text_alpha_handler_setup.v_text_alpha < v_text_alpha_handler_setup.v_text_alpha_target_min)
        {
            v_text_alpha_handler_setup.v_text_alpha = v_text_alpha_handler_setup.v_text_alpha_target_min;
        }

        if (v_text_alpha_handler_setup.v_text_alpha > v_text_alpha_handler_setup.v_text_alpha_target_max)
        {
            v_text_alpha_handler_setup.v_text_alpha = v_text_alpha_handler_setup.v_text_alpha_target_max;
        }
    }
}
