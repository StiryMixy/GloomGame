using System;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class svl_sprite_frame
{
    [Header("Configurable Variables")]
    [SerializeField] public bool v_frame_play = true;
    [SerializeField] public bool v_frame_loops;
    [SerializeField] public bool v_frame_profile_inversion;
    [SerializeField] public float v_frame_scale;
    [SerializeField] public SpriteRenderer v_sprite_renderer;
    [SerializeField] public Material v_sprite_renderer_material;
    [SerializeField] public List<Sprite> v_frame_list;
    [Header("Reference Variables")]
    [SerializeField] public int v_frame_counter;
    [SerializeField] public int v_frame_counter_stay_timer;
    [SerializeField] public int v_frame_counter_stay_timer_randomized_gate;
    [SerializeField] public bool v_timer_stay_bool_frame_move_check;
    [SerializeField] public bool v_timer_stay_bool_frame_move_gate;
    [SerializeField] public v_tags_sprite_profile_list v_frame_sprite_profile;
}

[Serializable]
public class svl_sprite_alpha_element
{
    [SerializeField] public bool v_sprite_alpha_local;
    [SerializeField] public bool v_sprite_alpha_skip = true;
    [Space(10)]
    [SerializeField] public bool v_sprite_alpha_local_update_on_timer;
    [Range(0.0f, 1.0f)][SerializeField] public float v_sprite_alpha_target = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] public float v_sprite_alpha_target_max = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] public float v_sprite_alpha_target_min = 0.0f;
    [SerializeField] public float v_sprite_alpha_increment = 0.01f;
}

[Serializable]
public class svl_sprite_float_up_element
{
    [SerializeField] public bool v_sprite_float_up_local;
    [SerializeField] public bool v_sprite_float_up_skip = true;
    [Space(10)]
    [SerializeField] public bool v_sprite_float_up_auto_change_direction;
    [SerializeField] public bool v_sprite_float_up_rely_on_frame_change_for_direction_change;
    [SerializeField] public float v_sprite_float_up_distance_threshold = 0.0f;
    [Space(10)]
    [SerializeField] public float v_sprite_float_up_target_distance = 0.0f;
    [SerializeField] public bool v_sprite_float_up_randomize_distance;
    [SerializeField] public float v_sprite_float_up_target_distance_max = 0.0f;
    [Space(10)]
    [SerializeField] public float v_sprite_float_up_target_origin = 0.0f;
    [SerializeField] public bool v_sprite_float_up_randomize_origin;
    [SerializeField] public float v_sprite_float_up_target_origin_max = 0.0f;
    [Space(10)]
    [SerializeField] public float v_sprite_float_up_target_smoothness = 0.0f;
    [SerializeField] public bool v_sprite_float_up_randomize_smoothness;
    [SerializeField] public float v_sprite_float_up_target_smoothness_max = 0.0f;
    [Space(10)]
    [SerializeField] public bool v_sprite_float_direction_is_down_bool = false;
    [SerializeField] public bool v_sprite_float_up_bool_direction_change_check = false;
}

[Serializable]
public class svl_sprite_index_element
{
    [Header("Configurable Variables")]
    [SerializeField] public int v_timer_stay;
    [SerializeField] public bool v_timer_stay_randomized;
    [SerializeField] public int v_timer_stay_randomized_max;
    [SerializeField] public bool v_timer_stay_forever;
    [Space(10)]
    [SerializeField] public svl_sprite_alpha_element v_sprite_alpha_setup = new svl_sprite_alpha_element();
    [SerializeField] public svl_sprite_float_up_element v_sprite_float_up_setup = new svl_sprite_float_up_element();
}

[Serializable]
public class svl_sprite_alpha_handler
{
    [Header("Configurable Variables")]
    [SerializeField] public bool v_sprite_alpha_global;
    [Space(10)]
    [SerializeField] public bool v_sprite_alpha_update_on_timer;
    [Range(0.0f, 1.0f)][SerializeField] public float v_sprite_alpha_target = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] public float v_sprite_alpha_target_max = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] public float v_sprite_alpha_target_min = 0.0f;
    [SerializeField] public float v_sprite_alpha_increment = 0.01f;
    [Header("Reference Variables")]
    [Range(0.0f, 1.0f)][SerializeField] public float v_sprite_alpha = 0.0f;
    [SerializeField] public bool v_sprite_alpha_bool_frame_move_check;
}

[Serializable]
public class svl_sprite_float_up_handler
{
    [Header("Configurable Variables")]
    [SerializeField] public bool v_sprite_float_up_enabled;
    [Space(10)]
    [SerializeField] public bool v_sprite_float_up_global;
    [SerializeField] public bool v_sprite_float_up_auto_change_direction;
    [SerializeField] public bool v_sprite_float_up_rely_on_frame_change_for_direction_change;
    [SerializeField] public float v_sprite_float_up_distance_threshold = 0.0f;
    [Space(10)]
    [SerializeField] public float v_sprite_float_up_target_distance = 0.0f;
    [SerializeField] public bool v_sprite_float_up_randomize_distance;
    [SerializeField] public float v_sprite_float_up_target_distance_max = 0.0f;
    [Space(10)]
    [SerializeField] public float v_sprite_float_up_target_origin = 0.0f;
    [SerializeField] public bool v_sprite_float_up_randomize_origin;
    [SerializeField] public float v_sprite_float_up_target_origin_max = 0.0f;
    [Space(10)]
    [SerializeField] public float v_sprite_float_up_target_smoothness = 0.0f;
    [SerializeField] public bool v_sprite_float_up_randomize_smoothness;
    [SerializeField] public float v_sprite_float_up_target_smoothness_max = 0.0f;
    [Header("Reference Variables")]
    [SerializeField] public float v_sprite_float_up_distance = 0.0f;
    [SerializeField] public float v_sprite_float_up_smoothness = 0.0f;
    [SerializeField] public Vector3 v_sprite_float_up_vector_target = Vector3.zero;
    [SerializeField] public Vector3 v_sprite_float_up_vector_target_max = Vector3.zero;
    [SerializeField] public float v_sprite_float_up_y_vector_velocity = 0.0f;
    [SerializeField] public float v_sprite_float_up_current_distance = 0.0f;
    [SerializeField] public bool v_sprite_float_direction_is_down_bool = false;
    [SerializeField] public bool v_sprite_float_up_bool_direction_change_check = false;
    [SerializeField] public bool v_sprite_float_up_bool_frame_move_check;
    [SerializeField] public Vector3 v_sprite_default_position = Vector3.zero;
}

[Serializable]
public class svl_sprite_sort
{
    [Header("Configurable Variables")]
    [SerializeField] public int v_sort_modifier;
    [Space(10)]
    [SerializeField] public bool v_enable_parent = false;
    [SerializeField] public bool v_enable_parent_root = true;
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_available_parent;
}

public class s_sprite_handler : MonoBehaviour
{
    [Header("Sprite Handler Time Handler Setup")]
    [SerializeField] public svgl_time_handler v_sprite_time_handler_setup = new svgl_time_handler();

    [Header("Sprite Handler Frame Setup")]
    [SerializeField] public svl_sprite_frame v_sprite_frame_setup = new svl_sprite_frame();

    [Header("Sprite Handler Alpha Setup")]
    [SerializeField] public svl_sprite_alpha_handler v_sprite_alpha_setup = new svl_sprite_alpha_handler();

    [Header("Sprite Handler Alpha Setup")]
    [SerializeField] public svl_sprite_float_up_handler v_sprite_float_up_setup = new svl_sprite_float_up_handler();

    [Header("Sprite Handler Index Setup")]
    [SerializeField] public List<svl_sprite_index_element> v_sprite_index_setup = new List<svl_sprite_index_element>();

    [Header("Sprite Handler Sort Setup")]
    [SerializeField] public svl_sprite_sort v_sprite_sort_setup = new svl_sprite_sort();

    void Start()
    {
        f_sprite_handler_gameobject_finder();
        v_sprite_frame_setup.v_sprite_renderer.color = new Color(v_sprite_frame_setup.v_sprite_renderer.color.r, v_sprite_frame_setup.v_sprite_renderer.color.g, v_sprite_frame_setup.v_sprite_renderer.color.b, v_sprite_alpha_setup.v_sprite_alpha);
        v_sprite_float_up_setup.v_sprite_default_position = v_sprite_frame_setup.v_sprite_renderer.transform.localPosition;
    }

    void Update()
    {
        //SCALE START
        float tv_targetScale = v_sprite_frame_setup.v_frame_scale;
        if ((v_sprite_frame_setup.v_frame_sprite_profile.Equals(v_tags_sprite_profile_list.Left)) && (v_sprite_frame_setup.v_frame_profile_inversion))
        {
            v_sprite_frame_setup.v_sprite_renderer.transform.localScale = new Vector3(-tv_targetScale, tv_targetScale, tv_targetScale);
        }
        else
        {
            v_sprite_frame_setup.v_sprite_renderer.transform.localScale = new Vector3(tv_targetScale, tv_targetScale, tv_targetScale);
        }
        //SCALE END

        //MATERIAL START
        if (v_sprite_frame_setup.v_sprite_renderer_material)
        {
            v_sprite_frame_setup.v_sprite_renderer.material = v_sprite_frame_setup.v_sprite_renderer_material;
        }
        //MATERIAL END

        //FRAME INDEX START
        if (v_sprite_frame_setup.v_frame_play)
        {
            int tv_target_index = v_sprite_frame_setup.v_frame_counter;
            v_sprite_frame_setup.v_sprite_renderer.sprite = v_sprite_frame_setup.v_frame_list[tv_target_index];

            //INDEX ALPHA SECTION START
            if (v_sprite_alpha_setup.v_sprite_alpha_global)
            {
                v_sprite_alpha_setup.v_sprite_alpha_bool_frame_move_check = true;
            }
            else
            {
                if (v_sprite_index_setup[tv_target_index].v_sprite_alpha_setup.v_sprite_alpha_local)
                {
                    v_sprite_alpha_setup.v_sprite_alpha_update_on_timer = v_sprite_index_setup[tv_target_index].v_sprite_alpha_setup.v_sprite_alpha_local_update_on_timer;
                    v_sprite_alpha_setup.v_sprite_alpha_target = v_sprite_index_setup[tv_target_index].v_sprite_alpha_setup.v_sprite_alpha_target;
                    v_sprite_alpha_setup.v_sprite_alpha_target_max = v_sprite_index_setup[tv_target_index].v_sprite_alpha_setup.v_sprite_alpha_target_max;
                    v_sprite_alpha_setup.v_sprite_alpha_target_min = v_sprite_index_setup[tv_target_index].v_sprite_alpha_setup.v_sprite_alpha_target_min;
                    v_sprite_alpha_setup.v_sprite_alpha_increment = v_sprite_index_setup[tv_target_index].v_sprite_alpha_setup.v_sprite_alpha_increment;

                    if (!v_sprite_alpha_setup.v_sprite_alpha_bool_frame_move_check)
                    {
                        if (v_sprite_alpha_setup.v_sprite_alpha != v_sprite_index_setup[tv_target_index].v_sprite_alpha_setup.v_sprite_alpha_target)
                        {
                            v_sprite_alpha_setup.v_sprite_alpha_bool_frame_move_check = false;
                        }
                        else
                        {
                            v_sprite_alpha_setup.v_sprite_alpha_bool_frame_move_check = true;
                        }
                    }

                    if (v_sprite_index_setup[tv_target_index].v_sprite_alpha_setup.v_sprite_alpha_skip)
                    {
                        v_sprite_alpha_setup.v_sprite_alpha_bool_frame_move_check = true;
                    }
                }
                else
                {
                    v_sprite_alpha_setup.v_sprite_alpha_bool_frame_move_check = true;
                }
            }
            //INDEX ALPHA SECTION END

            //INDEX FLOAT UP SECTION START
            if (v_sprite_float_up_setup.v_sprite_float_up_global)
            {
                v_sprite_float_up_setup.v_sprite_float_up_bool_frame_move_check = true;
            }
            else
            {
                if (v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_local)
                {
                    if (v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_auto_change_direction)
                    {
                        v_sprite_float_up_setup.v_sprite_float_up_auto_change_direction = true;
                    }
                    else
                    {
                        v_sprite_float_up_setup.v_sprite_float_up_auto_change_direction = false;
                        v_sprite_float_up_setup.v_sprite_float_direction_is_down_bool = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_direction_is_down_bool;
                        v_sprite_float_up_setup.v_sprite_float_up_bool_direction_change_check = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_bool_direction_change_check;
                    }
                    v_sprite_float_up_setup.v_sprite_float_up_rely_on_frame_change_for_direction_change = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_rely_on_frame_change_for_direction_change;
                    v_sprite_float_up_setup.v_sprite_float_up_distance_threshold = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_distance_threshold;
                    v_sprite_float_up_setup.v_sprite_float_up_target_distance = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_target_distance;
                    v_sprite_float_up_setup.v_sprite_float_up_randomize_distance = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_randomize_distance;
                    v_sprite_float_up_setup.v_sprite_float_up_target_distance_max = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_target_distance_max;
                    v_sprite_float_up_setup.v_sprite_float_up_target_origin = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_target_origin;
                    v_sprite_float_up_setup.v_sprite_float_up_randomize_origin = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_randomize_origin;
                    v_sprite_float_up_setup.v_sprite_float_up_target_origin_max = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_target_origin_max;
                    v_sprite_float_up_setup.v_sprite_float_up_target_smoothness = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_target_smoothness;
                    v_sprite_float_up_setup.v_sprite_float_up_randomize_smoothness = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_randomize_smoothness;
                    v_sprite_float_up_setup.v_sprite_float_up_target_smoothness_max = v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_target_smoothness_max;

                    if (v_sprite_index_setup[tv_target_index].v_sprite_float_up_setup.v_sprite_float_up_skip)
                    {
                        v_sprite_float_up_setup.v_sprite_float_up_bool_frame_move_check = true;
                    }
                }
            }
            if (!v_sprite_float_up_setup.v_sprite_float_up_enabled)
            {
                v_sprite_float_up_setup.v_sprite_float_up_bool_frame_move_check = true;
            }
            //INDEX FLOAT UP SECTION END

            if (v_sprite_time_handler_setup.v_time_handler_script.f_time_level_gate_get(v_sprite_time_handler_setup.v_time_handler_level))
            {
                //INDEX STAY SECTION START
                if (!v_sprite_frame_setup.v_timer_stay_bool_frame_move_check)
                {
                    int tv_target_stay;
                    if (v_sprite_index_setup[tv_target_index].v_timer_stay_randomized)
                    {
                        if (v_sprite_frame_setup.v_frame_counter_stay_timer_randomized_gate < v_sprite_index_setup[tv_target_index].v_timer_stay)
                        {
                            v_sprite_frame_setup.v_frame_counter_stay_timer_randomized_gate = UnityEngine.Random.Range(v_sprite_index_setup[tv_target_index].v_timer_stay, v_sprite_index_setup[tv_target_index].v_timer_stay_randomized_max + 1);
                        }
                        tv_target_stay = v_sprite_frame_setup.v_frame_counter_stay_timer_randomized_gate;
                    }
                    else
                    {
                        tv_target_stay = v_sprite_index_setup[tv_target_index].v_timer_stay;
                    }

                    if (v_sprite_frame_setup.v_frame_counter_stay_timer <= tv_target_stay)
                    {
                        v_sprite_frame_setup.v_timer_stay_bool_frame_move_check = false;
                        v_sprite_frame_setup.v_frame_counter_stay_timer += 1;
                    }
                    else
                    {
                        v_sprite_frame_setup.v_timer_stay_bool_frame_move_check = true;
                        v_sprite_frame_setup.v_frame_counter_stay_timer = 0;
                        v_sprite_frame_setup.v_frame_counter_stay_timer_randomized_gate = 0;
                    }
                }
                //INDEX STAY SECTION END

                if ((v_sprite_frame_setup.v_timer_stay_bool_frame_move_check) && (v_sprite_alpha_setup.v_sprite_alpha_bool_frame_move_check) && (v_sprite_float_up_setup.v_sprite_float_up_bool_frame_move_check) && (!v_sprite_index_setup[tv_target_index].v_timer_stay_forever))
                {
                    v_sprite_frame_setup.v_timer_stay_bool_frame_move_check = false;
                    v_sprite_alpha_setup.v_sprite_alpha_bool_frame_move_check = false;
                    v_sprite_float_up_setup.v_sprite_float_up_bool_frame_move_check = false;
                    v_sprite_frame_setup.v_timer_stay_bool_frame_move_gate = true;

                    if (v_sprite_frame_setup.v_frame_counter < (v_sprite_index_setup.Count - 1))
                    {
                        v_sprite_frame_setup.v_frame_counter += 1;

                        f_sprite_index_element_counter_parameters_reset();
                    }
                    else
                    {
                        if (v_sprite_frame_setup.v_frame_loops)
                        {
                            v_sprite_frame_setup.v_frame_counter = 0;

                            f_sprite_index_element_counter_parameters_reset();
                        }
                    }
                }
                else
                {
                    v_sprite_frame_setup.v_timer_stay_bool_frame_move_gate = false;
                }
            }
        }
        //FRAME INDEX END

        //FRAME ALPHA START
        if (v_sprite_alpha_setup.v_sprite_alpha_update_on_timer)
        {
            if (v_sprite_time_handler_setup.v_time_handler_script.f_time_level_gate_get(v_sprite_time_handler_setup.v_time_handler_level))
            {
                f_sprite_handler_alpha_controller();
                v_sprite_frame_setup.v_sprite_renderer.color = new Color(v_sprite_frame_setup.v_sprite_renderer.color.r, v_sprite_frame_setup.v_sprite_renderer.color.g, v_sprite_frame_setup.v_sprite_renderer.color.b, v_sprite_alpha_setup.v_sprite_alpha);
            }
        }
        else
        {
            f_sprite_handler_alpha_controller();
            v_sprite_frame_setup.v_sprite_renderer.color = new Color(v_sprite_frame_setup.v_sprite_renderer.color.r, v_sprite_frame_setup.v_sprite_renderer.color.g, v_sprite_frame_setup.v_sprite_renderer.color.b, v_sprite_alpha_setup.v_sprite_alpha);
        }
        //FRAME ALPHA END

        //FRAME FLOAT EFFECT START
        f_sprite_handler_float_up_controller();
        //FRAME FLOAT EFFECT END

        f_sprite_handler_sorter();
    }

    public void f_sprite_handler_gameobject_finder()
    {
        v_sprite_time_handler_setup.v_time_handler_gameobject = GameObject.Find(v_sprite_time_handler_setup.v_time_handler_gameobject_name);
        v_sprite_time_handler_setup.v_time_handler_script = v_sprite_time_handler_setup.v_time_handler_gameobject.GetComponent<s_time_handler>();
    }

    public void f_sprite_handler_alpha_controller()
    {
        if (v_sprite_alpha_setup.v_sprite_alpha != v_sprite_alpha_setup.v_sprite_alpha_target)
        {
            if (v_sprite_alpha_setup.v_sprite_alpha > v_sprite_alpha_setup.v_sprite_alpha_target)
            {
                if ((v_sprite_alpha_setup.v_sprite_alpha - v_sprite_alpha_setup.v_sprite_alpha_increment) < v_sprite_alpha_setup.v_sprite_alpha_target)
                {
                    v_sprite_alpha_setup.v_sprite_alpha = v_sprite_alpha_setup.v_sprite_alpha_target;
                }
                else
                {
                    v_sprite_alpha_setup.v_sprite_alpha -= v_sprite_alpha_setup.v_sprite_alpha_increment;
                }
            }
            else if (v_sprite_alpha_setup.v_sprite_alpha < v_sprite_alpha_setup.v_sprite_alpha_target)
            {
                if ((v_sprite_alpha_setup.v_sprite_alpha + v_sprite_alpha_setup.v_sprite_alpha_increment) > v_sprite_alpha_setup.v_sprite_alpha_target)
                {
                    v_sprite_alpha_setup.v_sprite_alpha = v_sprite_alpha_setup.v_sprite_alpha_target;
                }
                else
                {
                    v_sprite_alpha_setup.v_sprite_alpha += v_sprite_alpha_setup.v_sprite_alpha_increment;
                }
            }
        }

        if (v_sprite_alpha_setup.v_sprite_alpha < v_sprite_alpha_setup.v_sprite_alpha_target_min)
        {
            v_sprite_alpha_setup.v_sprite_alpha = v_sprite_alpha_setup.v_sprite_alpha_target_min;
        }

        if (v_sprite_alpha_setup.v_sprite_alpha > v_sprite_alpha_setup.v_sprite_alpha_target_max)
        {
            v_sprite_alpha_setup.v_sprite_alpha = v_sprite_alpha_setup.v_sprite_alpha_target_max;
        }
    }

    public void f_sprite_handler_float_up_controller()
    {
        if (v_sprite_float_up_setup.v_sprite_float_up_enabled)
        {
            if (!v_sprite_float_up_setup.v_sprite_float_direction_is_down_bool)
            {
                if (v_sprite_float_up_setup.v_sprite_float_up_randomize_distance)
                {
                    if (v_sprite_float_up_setup.v_sprite_float_up_bool_direction_change_check)
                    {
                        v_sprite_float_up_setup.v_sprite_float_up_distance = UnityEngine.Random.Range(v_sprite_float_up_setup.v_sprite_float_up_target_distance, v_sprite_float_up_setup.v_sprite_float_up_target_distance_max);
                    }
                }
                else
                {
                    v_sprite_float_up_setup.v_sprite_float_up_distance = v_sprite_float_up_setup.v_sprite_float_up_target_distance;
                }
            }
            else
            {
                if (v_sprite_float_up_setup.v_sprite_float_up_randomize_origin)
                {
                    if (v_sprite_float_up_setup.v_sprite_float_up_bool_direction_change_check)
                    {
                        v_sprite_float_up_setup.v_sprite_float_up_distance = UnityEngine.Random.Range(v_sprite_float_up_setup.v_sprite_float_up_target_origin, v_sprite_float_up_setup.v_sprite_float_up_target_origin_max);
                    }
                }
                else
                {
                    v_sprite_float_up_setup.v_sprite_float_up_distance = 0;
                }
            }

            if (v_sprite_float_up_setup.v_sprite_float_up_randomize_smoothness)
            {
                if (v_sprite_float_up_setup.v_sprite_float_up_bool_direction_change_check)
                {
                    v_sprite_float_up_setup.v_sprite_float_up_smoothness = UnityEngine.Random.Range(v_sprite_float_up_setup.v_sprite_float_up_target_smoothness, v_sprite_float_up_setup.v_sprite_float_up_target_smoothness_max);
                }
            }
            else
            {
                v_sprite_float_up_setup.v_sprite_float_up_smoothness = v_sprite_float_up_setup.v_sprite_float_up_target_smoothness;
            }

            if (v_sprite_float_up_setup.v_sprite_float_up_bool_direction_change_check)
            {
                v_sprite_float_up_setup.v_sprite_float_up_bool_direction_change_check = false;
            }

            v_sprite_float_up_setup.v_sprite_float_up_vector_target_max = new Vector3(v_sprite_frame_setup.v_sprite_renderer.transform.localPosition.x, v_sprite_float_up_setup.v_sprite_float_up_distance, v_sprite_frame_setup.v_sprite_renderer.transform.localPosition.z);

            v_sprite_float_up_setup.v_sprite_float_up_vector_target = new Vector3(v_sprite_frame_setup.v_sprite_renderer.transform.localPosition.x, Mathf.SmoothDamp(v_sprite_frame_setup.v_sprite_renderer.transform.localPosition.y, v_sprite_float_up_setup.v_sprite_float_up_distance, ref v_sprite_float_up_setup.v_sprite_float_up_y_vector_velocity, v_sprite_float_up_setup.v_sprite_float_up_smoothness), v_sprite_frame_setup.v_sprite_renderer.transform.localPosition.z);

            if (v_sprite_time_handler_setup.v_time_handler_script.f_time_level_gate_get(v_sprite_time_handler_setup.v_time_handler_level))
            {
                v_sprite_frame_setup.v_sprite_renderer.transform.localPosition = v_sprite_float_up_setup.v_sprite_float_up_vector_target;
            }

            v_sprite_float_up_setup.v_sprite_float_up_current_distance = Vector3.Distance(v_sprite_frame_setup.v_sprite_renderer.transform.localPosition, v_sprite_float_up_setup.v_sprite_float_up_vector_target_max);
            if (v_sprite_float_up_setup.v_sprite_float_up_current_distance < v_sprite_float_up_setup.v_sprite_float_up_distance_threshold)
            {
                if (v_sprite_float_up_setup.v_sprite_float_up_auto_change_direction)
                {
                    if (v_sprite_float_up_setup.v_sprite_float_up_rely_on_frame_change_for_direction_change)
                    {
                        if (v_sprite_frame_setup.v_timer_stay_bool_frame_move_gate)
                        {
                            v_sprite_float_up_setup.v_sprite_float_direction_is_down_bool = !v_sprite_float_up_setup.v_sprite_float_direction_is_down_bool;
                        }
                    }
                    else
                    {
                        v_sprite_float_up_setup.v_sprite_float_direction_is_down_bool = !v_sprite_float_up_setup.v_sprite_float_direction_is_down_bool;
                    }
                    v_sprite_float_up_setup.v_sprite_float_up_bool_direction_change_check = true;
                }
                v_sprite_float_up_setup.v_sprite_float_up_bool_frame_move_check = true;
            }
        }
        else
        {
            if (Vector3.Distance(v_sprite_frame_setup.v_sprite_renderer.transform.localPosition, v_sprite_float_up_setup.v_sprite_default_position) > 0)
            {
                Vector3 tv_return_position = Vector3.MoveTowards(v_sprite_frame_setup.v_sprite_renderer.transform.localPosition, v_sprite_float_up_setup.v_sprite_default_position, v_sprite_time_handler_setup.v_time_handler_script.f_time_level_rate_get(v_sprite_time_handler_setup.v_time_handler_level) / 3);
                v_sprite_frame_setup.v_sprite_renderer.transform.localPosition = tv_return_position;
            }
        }
    }

    public void f_sprite_handler_sorter()
    {
        if (v_sprite_sort_setup.v_enable_parent)
        {
            if (v_sprite_sort_setup.v_enable_parent_root)
            {
                v_sprite_sort_setup.v_available_parent = transform.root.gameObject;
            }
            else
            {
                v_sprite_sort_setup.v_available_parent = transform.parent.gameObject;
            }

            v_sprite_frame_setup.v_sprite_renderer.sortingOrder = (int)((v_sprite_sort_setup.v_available_parent.transform.position.z * 100) + v_sprite_sort_setup.v_sort_modifier);
        }
        else
        {
            v_sprite_frame_setup.v_sprite_renderer.sortingOrder = (int)((transform.position.z * 100) + v_sprite_sort_setup.v_sort_modifier);
        }
    }

    public void f_sprite_index_element_counter_parameters_reset()
    {
        v_sprite_frame_setup.v_frame_counter_stay_timer = 0;
        v_sprite_frame_setup.v_frame_counter_stay_timer_randomized_gate = 0;
        v_sprite_frame_setup.v_timer_stay_bool_frame_move_check = false;
        v_sprite_alpha_setup.v_sprite_alpha_bool_frame_move_check = false;
        v_sprite_float_up_setup.v_sprite_float_up_bool_frame_move_check = false;
        v_sprite_frame_setup.v_timer_stay_bool_frame_move_gate = false;
    }

    public void f_sprite_skip_to_last_frame()
    {
        v_sprite_frame_setup.v_frame_counter = v_sprite_index_setup.Count - 1;
        f_sprite_index_element_counter_parameters_reset();
    }

    public void f_sprite_rewind_to_first_frame()
    {
        v_sprite_frame_setup.v_frame_counter = 0;
        f_sprite_index_element_counter_parameters_reset();
    }

    public bool f_sprite_reached_end_of_frames()
    {
        return (v_sprite_frame_setup.v_frame_counter >= (v_sprite_index_setup.Count - 1));
    }
}
