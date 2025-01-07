using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svgl_time_handler
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_time_handler_gameobject_name;
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_time_handler_gameobject;
    [SerializeField] public s_time_handler v_time_handler_script;
    [SerializeField] public v_tags_timer_level_list v_time_handler_level;
}

[Serializable]
public class svgl_time_caller
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_time_handler_gameobject_name;
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_time_handler_gameobject;
    [SerializeField] public s_time_handler v_time_handler_script;
}

[Serializable]
public class svl_time_handler_list_element
{
    [Header("Configurable Variables")]
    [SerializeField] public bool v_timer_enabled = true;
    [SerializeField] public v_tags_timer_level_list v_timer_level_type;
    [SerializeField] public float v_timer_rate_increment = 0.1f;
    [SerializeField] public bool v_timer_rate_follow_target = true;
    [SerializeField] public bool v_timer_rate_follow_is_instant = false;
    [Range(0.0f, 60.0f)][SerializeField] public float v_timer_rate_target = 1.0f;
    [Range(0.0f, 60.0f)][SerializeField] public float v_timer_rate_default = 1.0f;
    [Range(0.0f, 60.0f)][SerializeField] public float v_timer_rate = 1.0f;
    [SerializeField] public bool v_timer_purge = false;
    [SerializeField] public float v_timer_purge_threshold = 0.0f;
    [Header("Reference Variables")]
    [SerializeField] public float v_timer = 0.0f;
    [SerializeField] public int v_timer_precise = 0;
    [SerializeField] public bool v_timer_gate = false;
    [SerializeField] public int v_timer_gate_counter = 0;
}

public class s_time_handler : MonoBehaviour
{
    [Header("Time Handler List Setup")]
    [SerializeField] public List<svl_time_handler_list_element> v_time_handler_list_setup = new List<svl_time_handler_list_element>();
    [Header("Time Handler Level Type Setup")]
    public List<v_tags_timer_level_list> v_tags_timer_level_list = new List<v_tags_timer_level_list>();
    [Header("Time Stop Handler Setup")]
    public bool v_time_is_stopped = false;

    void Update()
    {
        if (v_time_handler_list_setup.Count > 0)
        {
            for (int i = 0; i < v_time_handler_list_setup.Count; i++)
            {
                f_time_level_rate_controller(i);
                f_time_level_timer(i);
            }
        }
    }
    
    public bool f_time_level_gate_get(v_tags_timer_level_list sv_time_level_type)
    {
        return v_time_handler_list_setup[v_tags_timer_level_list.IndexOf(sv_time_level_type)].v_timer_gate;
    }

    public float f_time_level_rate_get(v_tags_timer_level_list sv_time_level_type)
    {
        return v_time_handler_list_setup[v_tags_timer_level_list.IndexOf(sv_time_level_type)].v_timer_rate;
    }

    public void f_time_level_rate_controller(int sv_time_level_index)
    {
        float tv_target_val = 0.0f;
        svl_time_handler_list_element tv_reference_index = v_time_handler_list_setup[sv_time_level_index];
        if (tv_reference_index.v_timer_rate_follow_target)
        {
            tv_target_val = tv_reference_index.v_timer_rate_target;
        }
        else
        {
            tv_target_val = tv_reference_index.v_timer_rate_default;
        }

        if (tv_reference_index.v_timer_rate_follow_is_instant)
        {
            if (tv_reference_index.v_timer_rate != tv_target_val)
            {
                tv_reference_index.v_timer_rate = tv_target_val;
            }
        }
        else
        {
            if (tv_reference_index.v_timer_rate != tv_target_val)
            {
                if (tv_reference_index.v_timer_rate > tv_target_val)
                {
                    if ((tv_reference_index.v_timer_rate - tv_reference_index.v_timer_rate_increment) < tv_target_val)
                    {
                        tv_reference_index.v_timer_rate = tv_target_val;
                    }
                    else
                    {
                        tv_reference_index.v_timer_rate -= tv_reference_index.v_timer_rate_increment;
                    }
                }
                else if (tv_reference_index.v_timer_rate < tv_target_val)
                {       
                    if ((tv_reference_index.v_timer_rate + tv_reference_index.v_timer_rate_increment) > tv_target_val)
                    {
                        tv_reference_index.v_timer_rate = tv_target_val;
                    }
                    else
                    {
                        tv_reference_index.v_timer_rate += tv_reference_index.v_timer_rate_increment;
                    }
                }
            }
        }

        if (tv_reference_index.v_timer_purge)
        {
            if (tv_reference_index.v_timer > tv_reference_index.v_timer_purge_threshold)
            {
                tv_reference_index.v_timer = 0.0f;
            }
            if (tv_reference_index.v_timer_precise > (int)tv_reference_index.v_timer_purge_threshold)
            {
                tv_reference_index.v_timer_precise = 0;
            }
            if (tv_reference_index.v_timer_gate_counter > (int)tv_reference_index.v_timer_purge_threshold)
            {
                tv_reference_index.v_timer_gate_counter = 0;
            }
        }
    }

    public void f_time_level_timer(int sv_time_level_index)
    {
        svl_time_handler_list_element tv_reference_index = v_time_handler_list_setup[sv_time_level_index];
        if (tv_reference_index.v_timer_enabled)
        {
            if (!v_time_is_stopped)
            {
                tv_reference_index.v_timer += (Time.deltaTime * tv_reference_index.v_timer_rate);
            }

            if ((int)tv_reference_index.v_timer != tv_reference_index.v_timer_precise)
            {
                tv_reference_index.v_timer_gate = true;
                tv_reference_index.v_timer_precise = (int)tv_reference_index.v_timer;
            }
            else
            {
                tv_reference_index.v_timer_gate = false;
            }

            if (tv_reference_index.v_timer_gate)
            {
                tv_reference_index.v_timer_gate_counter += 1;
            }
        }
    }
}
