using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class svl_image_alpha_handler
{
    [Header("Configurable Variables")]
    [SerializeField] public UnityEngine.UI.Image v_image_script;
    [Range(0.0f, 1.0f)][SerializeField] public float v_image_alpha_target = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] public float v_image_alpha_target_max = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] public float v_image_alpha_target_min = 0.0f;
    [SerializeField] public float v_image_alpha_increment = 0.01f;
    [Header("Reference Variables")]
    [Range(0.0f, 1.0f)][SerializeField] public float v_image_alpha = 0.0f;
}

public class s_ui_hud_image_alpha_handler : MonoBehaviour
{
    [Header("Image Alpha Setup")]
    [SerializeField] public svl_image_alpha_handler v_image_alpha_handler_setup = new svl_image_alpha_handler();

    void Update()
    {
        f_image_handler_alpha_controller();
        v_image_alpha_handler_setup.v_image_script.color = new Color(v_image_alpha_handler_setup.v_image_script.color.r, v_image_alpha_handler_setup.v_image_script.color.g, v_image_alpha_handler_setup.v_image_script.color.b, v_image_alpha_handler_setup.v_image_alpha);
    }

    public void f_image_handler_alpha_controller()
    {
        if (v_image_alpha_handler_setup.v_image_alpha != v_image_alpha_handler_setup.v_image_alpha_target)
        {
            if (v_image_alpha_handler_setup.v_image_alpha > v_image_alpha_handler_setup.v_image_alpha_target)
            {
                if ((v_image_alpha_handler_setup.v_image_alpha - v_image_alpha_handler_setup.v_image_alpha_increment) < v_image_alpha_handler_setup.v_image_alpha_target)
                {
                    v_image_alpha_handler_setup.v_image_alpha = v_image_alpha_handler_setup.v_image_alpha_target;
                }
                else
                {
                    v_image_alpha_handler_setup.v_image_alpha -= v_image_alpha_handler_setup.v_image_alpha_increment;
                }
            }
            else if (v_image_alpha_handler_setup.v_image_alpha < v_image_alpha_handler_setup.v_image_alpha_target)
            {
                if ((v_image_alpha_handler_setup.v_image_alpha + v_image_alpha_handler_setup.v_image_alpha_increment) > v_image_alpha_handler_setup.v_image_alpha_target)
                {
                    v_image_alpha_handler_setup.v_image_alpha = v_image_alpha_handler_setup.v_image_alpha_target;
                }
                else
                {
                    v_image_alpha_handler_setup.v_image_alpha += v_image_alpha_handler_setup.v_image_alpha_increment;
                }
            }
        }

        if (v_image_alpha_handler_setup.v_image_alpha < v_image_alpha_handler_setup.v_image_alpha_target_min)
        {
            v_image_alpha_handler_setup.v_image_alpha = v_image_alpha_handler_setup.v_image_alpha_target_min;
        }

        if (v_image_alpha_handler_setup.v_image_alpha > v_image_alpha_handler_setup.v_image_alpha_target_max)
        {
            v_image_alpha_handler_setup.v_image_alpha = v_image_alpha_handler_setup.v_image_alpha_target_max;
        }
    }
}
