using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class s_entity_time_controller : MonoBehaviour
{
    [Header("Time Level 0 Variables")]
    public float v_time_level_0_timer = 0.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_0_timer_rate = 1.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_0_timer_rate_default = 1.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_0_timer_rate_target = 1.0f;
    public bool v_time_level_0_timer_rate_altered = true;
    public bool v_time_level_0_timer_rate_instant = false;
    public float v_time_level_0_timer_rate_increment = 0.1f;
    public int v_time_level_0_timer_precise = 0;
    public bool v_time_level_0_timer_gate = false;
    public int v_time_level_0_timer_gate_counter = 0;
    public bool v_time_level_0_timer_enabled = true;

    [Header("Time Level 1 Variables")]
    public float v_time_level_1_timer = 0.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_1_timer_rate = 15.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_1_timer_rate_default = 15.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_1_timer_rate_target = 15.0f;
    public bool v_time_level_1_timer_rate_altered = true;
    public bool v_time_level_1_timer_rate_instant = false;
    public float v_time_level_1_timer_rate_increment = 0.1f;
    public int v_time_level_1_timer_precise = 0;
    public bool v_time_level_1_timer_gate = false;
    public int v_time_level_1_timer_gate_counter = 0;
    public bool v_time_level_1_timer_enabled = true;

    [Header("Time Level 2 Variables")]
    public float v_time_level_2_timer = 0.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_2_timer_rate = 15.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_2_timer_rate_default = 15.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_2_timer_rate_target = 15.0f;
    public bool v_time_level_2_timer_rate_altered = true;
    public bool v_time_level_2_timer_rate_instant = false;
    public float v_time_level_2_timer_rate_increment = 0.1f;
    public int v_time_level_2_timer_precise = 0;
    public bool v_time_level_2_timer_gate = false;
    public int v_time_level_2_timer_gate_counter = 0;
    public bool v_time_level_2_timer_enabled = true;

    [Header("Time Level 3 Variables")]
    public float v_time_level_3_timer = 0.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_3_timer_rate = 15.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_3_timer_rate_default = 15.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_3_timer_rate_target = 15.0f;
    public bool v_time_level_3_timer_rate_altered = true;
    public bool v_time_level_3_timer_rate_instant = false;
    public float v_time_level_3_timer_rate_increment = 0.1f;
    public int v_time_level_3_timer_precise = 0;
    public bool v_time_level_3_timer_gate = false;
    public int v_time_level_3_timer_gate_counter = 0;
    public bool v_time_level_3_timer_enabled = true;

    [Header("Time Level 4 Variables")]
    public float v_time_level_4_timer = 0.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_4_timer_rate = 15.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_4_timer_rate_default = 15.0f;
    [Range(0.0f, 60.0f)]
    public float v_time_level_4_timer_rate_target = 15.0f;
    public bool v_time_level_4_timer_rate_altered = true;
    public bool v_time_level_4_timer_rate_instant = false;
    public float v_time_level_4_timer_rate_increment = 0.1f;
    public int v_time_level_4_timer_precise = 0;
    public bool v_time_level_4_timer_gate = false;
    public int v_time_level_4_timer_gate_counter = 0;
    public bool v_time_level_4_timer_enabled = true;

    public enum v_time_level_list
    {
        Level_0,
        Level_1,
        Level_2,
        Level_3,
        Level_4
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f_time_level_0_rate_controller();
        f_time_level_1_rate_controller();
        f_time_level_2_rate_controller();
        f_time_level_3_rate_controller();
        f_time_level_4_rate_controller();
        f_time_level_0_timer();
        f_time_level_1_timer();
        f_time_level_2_timer();
        f_time_level_3_timer();
        f_time_level_4_timer();
    }

    public bool f_time_level_gate_get(v_time_level_list sv_level)
    {
        if (sv_level == v_time_level_list.Level_0)
        {
            return v_time_level_0_timer_gate;
        }
        else if (sv_level == v_time_level_list.Level_1)
        {
            return v_time_level_1_timer_gate;
        }
        else if (sv_level == v_time_level_list.Level_2)
        {
            return v_time_level_2_timer_gate;
        }
        else if (sv_level == v_time_level_list.Level_3)
        {
            return v_time_level_3_timer_gate;
        }
        else if (sv_level == v_time_level_list.Level_4)
        {
            return v_time_level_4_timer_gate;
        }
        else
        {
            return false;
        }
    }
    
    public float f_time_level_timer_rate_get(v_time_level_list sv_level)
    {
        if (sv_level == v_time_level_list.Level_0)
        {
            return v_time_level_0_timer_rate;
        }
        else if (sv_level == v_time_level_list.Level_1)
        {
            return v_time_level_1_timer_rate;
        }
        else if (sv_level == v_time_level_list.Level_2)
        {
            return v_time_level_2_timer_rate;
        }
        else if (sv_level == v_time_level_list.Level_3)
        {
            return v_time_level_3_timer_rate;
        }
        else if (sv_level == v_time_level_list.Level_4)
        {
            return v_time_level_4_timer_rate;
        }
        else
        {
            return 0.0f;
        }
    }

    public void f_time_level_0_rate_controller()
    {
        float tv_target_val = 0.0f;
        if (v_time_level_0_timer_rate_altered)
        {
            tv_target_val = v_time_level_0_timer_rate_target;
        }
        else
        {
            tv_target_val = v_time_level_0_timer_rate_default;
        }

        if (v_time_level_0_timer_rate_instant)
        {
            if (v_time_level_0_timer_rate != tv_target_val)
            {
                v_time_level_0_timer_rate = tv_target_val;
            }
        }
        else
        {
            if (v_time_level_0_timer_rate != tv_target_val)
            {
                if (v_time_level_0_timer_rate > tv_target_val)
                {
                    if ((v_time_level_0_timer_rate - v_time_level_0_timer_rate_increment) < tv_target_val)
                    {
                        v_time_level_0_timer_rate = tv_target_val;
                    }
                    else
                    {
                        v_time_level_0_timer_rate -= v_time_level_0_timer_rate_increment;
                    }
                }
                else if (v_time_level_0_timer_rate < tv_target_val)
                {
                    if ((v_time_level_0_timer_rate + v_time_level_0_timer_rate_increment) > tv_target_val)
                    {
                        v_time_level_0_timer_rate = tv_target_val;
                    }
                    else
                    {
                        v_time_level_0_timer_rate += v_time_level_0_timer_rate_increment;
                    }
                }
            }
        }
    }

    public void f_time_level_1_rate_controller()
    {
        float tv_target_val = 0.0f;
        if (v_time_level_1_timer_rate_altered)
        {
            tv_target_val = v_time_level_1_timer_rate_target;
        }
        else
        {
            tv_target_val = v_time_level_1_timer_rate_default;
        }

        if (v_time_level_1_timer_rate_instant)
        {
            if (v_time_level_1_timer_rate != tv_target_val)
            {
                v_time_level_1_timer_rate = tv_target_val;
            }
        }
        else
        {
            if (v_time_level_1_timer_rate != tv_target_val)
            {
                if (v_time_level_1_timer_rate > tv_target_val)
                {
                    if ((v_time_level_1_timer_rate - v_time_level_1_timer_rate_increment) < tv_target_val)
                    {
                        v_time_level_1_timer_rate = tv_target_val;
                    }
                    else
                    {
                        v_time_level_1_timer_rate -= v_time_level_1_timer_rate_increment;
                    }
                }
                else if (v_time_level_1_timer_rate < tv_target_val)
                {
                    if ((v_time_level_1_timer_rate + v_time_level_1_timer_rate_increment) > tv_target_val)
                    {
                        v_time_level_1_timer_rate = tv_target_val;
                    }
                    else
                    {
                        v_time_level_1_timer_rate += v_time_level_1_timer_rate_increment;
                    }
                }
            }
        }
    }

    public void f_time_level_2_rate_controller()
    {
        float tv_target_val = 0.0f;
        if (v_time_level_2_timer_rate_altered)
        {
            tv_target_val = v_time_level_2_timer_rate_target;
        }
        else
        {
            tv_target_val = v_time_level_2_timer_rate_default;
        }

        if (v_time_level_2_timer_rate_instant)
        {
            if (v_time_level_2_timer_rate != tv_target_val)
            {
                v_time_level_2_timer_rate = tv_target_val;
            }
        }
        else
        {
            if (v_time_level_2_timer_rate != tv_target_val)
            {
                if (v_time_level_2_timer_rate > tv_target_val)
                {
                    if ((v_time_level_2_timer_rate - v_time_level_2_timer_rate_increment) < tv_target_val)
                    {
                        v_time_level_2_timer_rate = tv_target_val;
                    }
                    else
                    {
                        v_time_level_2_timer_rate -= v_time_level_2_timer_rate_increment;
                    }
                }
                else if (v_time_level_2_timer_rate < tv_target_val)
                {
                    if ((v_time_level_2_timer_rate + v_time_level_2_timer_rate_increment) > tv_target_val)
                    {
                        v_time_level_2_timer_rate = tv_target_val;
                    }
                    else
                    {
                        v_time_level_2_timer_rate += v_time_level_2_timer_rate_increment;
                    }
                }
            }
        }
    }

    public void f_time_level_3_rate_controller()
    {
        float tv_target_val = 0.0f;
        if (v_time_level_3_timer_rate_altered)
        {
            tv_target_val = v_time_level_3_timer_rate_target;
        }
        else
        {
            tv_target_val = v_time_level_3_timer_rate_default;
        }

        if (v_time_level_3_timer_rate_instant)
        {
            if (v_time_level_3_timer_rate != tv_target_val)
            {
                v_time_level_3_timer_rate = tv_target_val;
            }
        }
        else
        {
            if (v_time_level_3_timer_rate != tv_target_val)
            {
                if (v_time_level_3_timer_rate > tv_target_val)
                {
                    if ((v_time_level_3_timer_rate - v_time_level_3_timer_rate_increment) < tv_target_val)
                    {
                        v_time_level_3_timer_rate = tv_target_val;
                    }
                    else
                    {
                        v_time_level_3_timer_rate -= v_time_level_3_timer_rate_increment;
                    }
                }
                else if (v_time_level_3_timer_rate < tv_target_val)
                {
                    if ((v_time_level_3_timer_rate + v_time_level_3_timer_rate_increment) > tv_target_val)
                    {
                        v_time_level_3_timer_rate = tv_target_val;
                    }
                    else
                    {
                        v_time_level_3_timer_rate += v_time_level_3_timer_rate_increment;
                    }
                }
            }
        }
    }

    public void f_time_level_4_rate_controller()
    {
        float tv_target_val = 0.0f;
        if (v_time_level_4_timer_rate_altered)
        {
            tv_target_val = v_time_level_4_timer_rate_target;
        }
        else
        {
            tv_target_val = v_time_level_4_timer_rate_default;
        }

        if (v_time_level_4_timer_rate_instant)
        {
            if (v_time_level_4_timer_rate != tv_target_val)
            {
                v_time_level_4_timer_rate = tv_target_val;
            }
        }
        else
        {
            if (v_time_level_4_timer_rate != tv_target_val)
            {
                if (v_time_level_4_timer_rate > tv_target_val)
                {
                    if ((v_time_level_4_timer_rate - v_time_level_4_timer_rate_increment) < tv_target_val)
                    {
                        v_time_level_4_timer_rate = tv_target_val;
                    }
                    else
                    {
                        v_time_level_4_timer_rate -= v_time_level_4_timer_rate_increment;
                    }
                }
                else if (v_time_level_4_timer_rate < tv_target_val)
                {
                    if ((v_time_level_4_timer_rate + v_time_level_4_timer_rate_increment) > tv_target_val)
                    {
                        v_time_level_4_timer_rate = tv_target_val;
                    }
                    else
                    {
                        v_time_level_4_timer_rate += v_time_level_4_timer_rate_increment;
                    }
                }
            }
        }
    }

    public void f_time_level_0_timer()
    {
        if (v_time_level_0_timer_enabled)
        {
            v_time_level_0_timer += (Time.deltaTime * v_time_level_0_timer_rate);

            if ((int)v_time_level_0_timer != v_time_level_0_timer_precise)
            {
                v_time_level_0_timer_gate = true;
                v_time_level_0_timer_precise = (int)v_time_level_0_timer;
            }
            else
            {
                v_time_level_0_timer_gate = false;
            }

            if (v_time_level_0_timer_gate)
            {
                v_time_level_0_timer_gate_counter += 1;
            }
        }
    }

    public void f_time_level_1_timer()
    {
        if (v_time_level_1_timer_enabled)
        {
            v_time_level_1_timer += (Time.deltaTime * v_time_level_1_timer_rate);

            if ((int)v_time_level_1_timer != v_time_level_1_timer_precise)
            {
                v_time_level_1_timer_gate = true;
                v_time_level_1_timer_precise = (int)v_time_level_1_timer;
            }
            else
            {
                v_time_level_1_timer_gate = false;
            }

            if (v_time_level_1_timer_gate)
            {
                v_time_level_1_timer_gate_counter += 1;
            }
        }
    }

    public void f_time_level_2_timer()
    {
        if (v_time_level_2_timer_enabled)
        {
            v_time_level_2_timer += (Time.deltaTime * v_time_level_2_timer_rate);

            if ((int)v_time_level_2_timer != v_time_level_2_timer_precise)
            {
                v_time_level_2_timer_gate = true;
                v_time_level_2_timer_precise = (int)v_time_level_2_timer;
            }
            else
            {
                v_time_level_2_timer_gate = false;
            }

            if (v_time_level_2_timer_gate)
            {
                v_time_level_2_timer_gate_counter += 1;
            }
        }
    }

    public void f_time_level_3_timer()
    {
        if (v_time_level_3_timer_enabled)
        {
            v_time_level_3_timer += (Time.deltaTime * v_time_level_3_timer_rate);

            if ((int)v_time_level_3_timer != v_time_level_3_timer_precise)
            {
                v_time_level_3_timer_gate = true;
                v_time_level_3_timer_precise = (int)v_time_level_3_timer;
            }
            else
            {
                v_time_level_3_timer_gate = false;
            }

            if (v_time_level_3_timer_gate)
            {
                v_time_level_3_timer_gate_counter += 1;
            }
        }
    }

    public void f_time_level_4_timer()
    {
        if (v_time_level_4_timer_enabled)
        {
            v_time_level_4_timer += (Time.deltaTime * v_time_level_4_timer_rate);

            if ((int)v_time_level_4_timer != v_time_level_4_timer_precise)
            {
                v_time_level_4_timer_gate = true;
                v_time_level_4_timer_precise = (int)v_time_level_4_timer;
            }
            else
            {
                v_time_level_4_timer_gate = false;
            }

            if (v_time_level_4_timer_gate)
            {
                v_time_level_4_timer_gate_counter += 1;
            }
        }
    }
}
