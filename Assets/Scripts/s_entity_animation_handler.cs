using System.Collections.Generic;
using UnityEngine;
using static s_entity_time_controller;

public class s_entity_animation_handler : MonoBehaviour
{
    [Header("Entity Game Object Setup")]
    public GameObject v_sprite_gameobject;
    public SpriteRenderer v_sprite_gameobject_sprite_renderer;
    public Sprite v_sprite_gameobject_sprite_renderer_sprite;

    [Header("Entity Time Level Setup")]
    public GameObject v_entity_time_gameobject;
    public s_entity_time_controller v_entity_time_gameobject_script;
    public v_time_level_list v_entity_time_level;
    public bool v_entity_time_level_gate;

    [Header("Entity Frames Setup")]
    public bool v_sprite_frame_update_enabled = true;
    public int v_sprite_frame_index = 0;
    public bool v_sprite_frame_ended = false;
    public bool v_sprite_frame_loop = true;
    public int v_sprite_frame_index_max_delay = 0;
    public int v_sprite_frame_index_max_delay_counter = 0;
    public List<Sprite> v_sprite_frame_list;

    [Header("Entity Frames Position Update Setup")]
    public bool v_sprite_frame_position_update_enabled = false;
    public bool v_sprite_frame_position_update_gate = false;
    public bool v_sprite_frame_position_restart_gate = false;
    public bool v_sprite_frame_position_update_end = false;
    public int v_sprite_frame_index_update_target = 0;
    public Vector3 v_sprite_frame_index_update_start_position = Vector3.zero;
    public Vector3 v_sprite_frame_index_update_target_position = Vector3.zero;
    public float v_sprite_frame_index_update_position_speed = 0.0f;
    public Vector3 v_sprite_frame_index_update_start_rotation = Vector3.zero;
    public Vector3 v_sprite_frame_index_update_target_rotation = Vector3.zero;
    public float v_sprite_frame_index_update_rotation_speed = 0.0f;

    [Header("Entity Shadow Setup")]
    public GameObject v_sprite_gameobject_shadow;
    public SpriteRenderer v_sprite_gameobject_shadow_sprite_renderer;
    public Sprite v_sprite_gameobject_shadow_sprite_renderer_sprite;
    public bool v_sprite_gameobject_shadow_enabled;
    public float v_sprite_gameobject_shadow_sprite_renderer_alpha = 0.0f;
    public float v_sprite_gameobject_shadow_sprite_renderer_alpha_target = 0.0f;
    public float v_sprite_gameobject_shadow_sprite_renderer_alpha_target_max = 0.0f;
    public float v_sprite_gameobject_shadow_sprite_renderer_alpha_counter = 0.0f;

    [Header("Entity Shadow Update Setup")]
    public bool v_sprite_shadow_position_update_enabled = false;
    public bool v_sprite_shadow_position_update_gate = false;
    public bool v_sprite_shadow_position_restart_gate = false;
    public int v_sprite_shadow_index_update_target = 0;

    [Header("Entity Float Effect Setup")]
    public bool v_float_enabled = false;
    public bool v_float_direction_check = true;
    public float v_float_target_distance_threshold = 0.0f;
    public float v_float_start_distance_threshold = 0.0f;
    public float v_float_distance_limit = 0.0f;
    public float v_float_distance_limit_min = 0.0f;
    public float v_float_distance_limit_max = 0.0f;
    public float v_float_smooth_time = 0.0f;
    public float v_float_smooth_time_min = 0.0f;
    public float v_float_smooth_time_max = 0.0f;
    public float v_float_y_velocity = 0.0f;
    public Vector3 v_entity_time_gameobject_saved_start_pos = Vector3.zero;
    public Vector3 v_entity_time_gameobject_saved_target_pos = Vector3.zero;

    [Header("Entity Float Effect End Setup")]
    public bool v_float_end_enabled = false;
    public float v_float_end_speed;
    public bool v_float_ended;

    // Start is called before the first frame update
    void Start()
    {
        v_entity_time_gameobject = GameObject.Find("entity_time_controller");
        if (v_entity_time_gameobject != null)
        {
            v_entity_time_gameobject_script = v_entity_time_gameobject.GetComponent<s_entity_time_controller>();
        }

        if (v_sprite_gameobject != null)
        {
            v_sprite_gameobject_sprite_renderer = v_sprite_gameobject.GetComponent<SpriteRenderer>();
            if (v_sprite_gameobject_sprite_renderer != null)
            {
                v_sprite_gameobject_sprite_renderer_sprite = v_sprite_gameobject_sprite_renderer.sprite;
            }
        }

        if (v_sprite_gameobject_shadow != null)
        {
            v_sprite_gameobject_shadow_sprite_renderer = v_sprite_gameobject_shadow.GetComponent<SpriteRenderer>();
            if (v_sprite_gameobject_shadow_sprite_renderer != null)
            {
                v_sprite_gameobject_shadow_sprite_renderer_sprite = v_sprite_gameobject_shadow_sprite_renderer.sprite;
            }
        }

        f_entity_float_effect_refresh();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null)
        {
            v_entity_time_level = v_time_level_list.Level_1;
        }
        
        v_entity_time_level_gate = v_entity_time_gameobject_script.f_time_level_gate_get(v_entity_time_level);

        if (v_entity_time_level_gate)
        {
            f_entity_index_handler();
            f_entity_position_update_handler();
            f_entity_float_effect_handler();
        }

        f_entity_shadow_handler();
        f_entity_shadow_update_handler();
    }

    void f_entity_index_handler()
    {
        if (v_sprite_frame_update_enabled)
        {
            if (v_sprite_frame_index_max_delay_counter <= 0)
            {
                if (v_sprite_frame_index < v_sprite_frame_list.Count)
                {
                    v_sprite_frame_index += 1;
                }

                if (v_sprite_frame_index >= v_sprite_frame_list.Count)
                {
                    if (v_sprite_frame_loop)
                    {
                        v_sprite_frame_ended = false;
                        v_sprite_frame_index = 0;
                    }
                    else
                    {
                        v_sprite_frame_ended = true;
                        v_sprite_frame_index = v_sprite_frame_list.Count - 1;
                    }
                }
                else
                {
                    v_sprite_frame_ended = false;
                }

                v_sprite_frame_index_max_delay_counter = v_sprite_frame_index_max_delay;
            }
            else if (v_sprite_frame_index_max_delay_counter > v_sprite_frame_index_max_delay)
            {
                v_sprite_frame_index_max_delay_counter = v_sprite_frame_index_max_delay;
            }
            else
            {
                v_sprite_frame_index_max_delay_counter -= 1;
            }
        }
        
        v_sprite_gameobject.GetComponent<SpriteRenderer>().sprite = v_sprite_frame_list[v_sprite_frame_index];
    }

    void f_entity_position_update_handler()
    {
        if (v_sprite_frame_position_update_enabled && v_sprite_frame_index == v_sprite_frame_index_update_target)
        {
            v_sprite_frame_position_update_gate = true;
        }

        if (v_sprite_frame_position_update_enabled && v_sprite_frame_index == 0)
        {
            v_sprite_frame_position_restart_gate = true;
        }

        if (v_sprite_frame_position_update_gate)
        {
            v_sprite_gameobject.transform.localPosition = Vector3.MoveTowards(v_sprite_gameobject.transform.localPosition, v_sprite_frame_index_update_target_position, v_sprite_frame_index_update_position_speed);
            v_sprite_gameobject.transform.localEulerAngles = Vector3.MoveTowards(v_sprite_gameobject.transform.localEulerAngles, v_sprite_frame_index_update_target_rotation, v_sprite_frame_index_update_rotation_speed);
        }

        if (v_sprite_frame_position_restart_gate)
        {
            v_sprite_gameobject.transform.localPosition = v_sprite_frame_index_update_start_position;
            v_sprite_gameobject.transform.localEulerAngles = v_sprite_frame_index_update_start_rotation;
            v_sprite_frame_position_update_gate = false;
            v_sprite_frame_position_restart_gate = false;
            v_sprite_frame_position_update_end = false;
        }

        if ((v_sprite_gameobject.transform.localPosition.Equals(v_sprite_frame_index_update_target_position)) && (v_sprite_gameobject.transform.localEulerAngles.Equals(v_sprite_frame_index_update_target_rotation)))
        {
            v_sprite_frame_position_update_gate = false;
            v_sprite_frame_position_update_end = true;
        }
    }

    void f_entity_shadow_handler()
    {
        if (v_sprite_gameobject_shadow != null && v_sprite_gameobject_shadow_sprite_renderer != null)
        {
            v_sprite_gameobject_shadow_sprite_renderer_alpha = v_sprite_gameobject_shadow_sprite_renderer.color.a;

            if (v_sprite_gameobject_shadow_sprite_renderer_alpha != v_sprite_gameobject_shadow_sprite_renderer_alpha_target)
            {
                if (v_sprite_gameobject_shadow_sprite_renderer_alpha > v_sprite_gameobject_shadow_sprite_renderer_alpha_target)
                {
                    if ((v_sprite_gameobject_shadow_sprite_renderer_alpha - v_sprite_gameobject_shadow_sprite_renderer_alpha_counter) < v_sprite_gameobject_shadow_sprite_renderer_alpha_target)
                    {
                        v_sprite_gameobject_shadow_sprite_renderer_alpha = v_sprite_gameobject_shadow_sprite_renderer_alpha_target;
                    }
                    else
                    {
                        v_sprite_gameobject_shadow_sprite_renderer_alpha -= v_sprite_gameobject_shadow_sprite_renderer_alpha_counter;
                    }
                }
                else if (v_sprite_gameobject_shadow_sprite_renderer_alpha < v_sprite_gameobject_shadow_sprite_renderer_alpha_target)
                {
                    if ((v_sprite_gameobject_shadow_sprite_renderer_alpha + v_sprite_gameobject_shadow_sprite_renderer_alpha_counter) > v_sprite_gameobject_shadow_sprite_renderer_alpha_target)
                    {
                        v_sprite_gameobject_shadow_sprite_renderer_alpha = v_sprite_gameobject_shadow_sprite_renderer_alpha_target;
                    }
                    else
                    {
                        v_sprite_gameobject_shadow_sprite_renderer_alpha += v_sprite_gameobject_shadow_sprite_renderer_alpha_counter;
                    }
                }
            }

            v_sprite_gameobject_shadow_sprite_renderer.color = new Color
            (
                v_sprite_gameobject_shadow_sprite_renderer.color.r,
                v_sprite_gameobject_shadow_sprite_renderer.color.g,
                v_sprite_gameobject_shadow_sprite_renderer.color.b,
                v_sprite_gameobject_shadow_sprite_renderer_alpha
            );

            if (v_sprite_gameobject_shadow_enabled)
            {
                v_sprite_gameobject_shadow_sprite_renderer_alpha_target = v_sprite_gameobject_shadow_sprite_renderer_alpha_target_max;
            }
            else
            {
                v_sprite_gameobject_shadow_sprite_renderer_alpha_target = 0;
            }
        }
    }
    
    void f_entity_shadow_update_handler()
    {
        if (v_sprite_shadow_position_update_enabled && v_sprite_frame_index == v_sprite_shadow_index_update_target)
        {
            v_sprite_shadow_position_update_gate = true;
        }

        if (v_sprite_shadow_position_update_enabled && v_sprite_frame_index == 0)
        {
            v_sprite_shadow_position_restart_gate = true;
        }

        if (v_sprite_shadow_position_update_gate)
        {
            v_sprite_gameobject_shadow_enabled = true;
            v_sprite_shadow_position_update_gate = false;
        }

        if (v_sprite_shadow_position_restart_gate)
        {
            v_sprite_gameobject_shadow_enabled = false;
            v_sprite_shadow_position_restart_gate = false;
        }
    }

    void f_entity_float_effect_refresh()
    {
        v_float_distance_limit = Random.Range(v_float_distance_limit_min, v_float_distance_limit_max);
        v_float_smooth_time = Random.Range(v_float_smooth_time_min, v_float_smooth_time_max);

        if (v_sprite_gameobject != null)
        {
            v_entity_time_gameobject_saved_start_pos = v_sprite_frame_index_update_start_position;
        }
    }
    
    void f_entity_float_effect_handler()
    {
        if (v_float_enabled)
        {
            if (v_sprite_gameobject != null)
            {
                v_entity_time_gameobject_saved_target_pos = new Vector3
                (
                    v_sprite_gameobject.transform.localPosition.x,
                    v_entity_time_gameobject_saved_start_pos.y + v_float_distance_limit,
                    v_sprite_gameobject.transform.localPosition.z
                );
            }

            if (v_float_end_enabled)
            {
                float tv_determined_speed = (float)(v_float_end_speed * 0.00001);
                f_sprite_float_end(v_entity_time_gameobject_saved_start_pos, tv_determined_speed, v_entity_time_gameobject_script.f_time_level_timer_rate_get(v_entity_time_level));
            }
            else
            {
                if (v_float_direction_check)
                {
                    float tv_new_pos = Mathf.SmoothDamp(v_sprite_gameobject.transform.localPosition.y, v_entity_time_gameobject_saved_target_pos.y, ref v_float_y_velocity, v_float_smooth_time);
                    v_sprite_gameobject.transform.localPosition = new Vector3(v_sprite_gameobject.transform.localPosition.x, tv_new_pos, v_sprite_gameobject.transform.localPosition.z);

                    if (Vector3.Distance(v_sprite_gameobject.transform.localPosition, v_entity_time_gameobject_saved_target_pos) < v_float_target_distance_threshold)
                    {
                        v_float_direction_check = false;
                    }
                }
                else
                {
                    float tv_new_pos = Mathf.SmoothDamp(v_sprite_gameobject.transform.localPosition.y, v_entity_time_gameobject_saved_start_pos.y, ref v_float_y_velocity, v_float_smooth_time);
                    v_sprite_gameobject.transform.localPosition = new Vector3(v_sprite_gameobject.transform.localPosition.x, tv_new_pos, v_sprite_gameobject.transform.localPosition.z);

                    if (Vector3.Distance(v_sprite_gameobject.transform.localPosition, v_entity_time_gameobject_saved_start_pos) < v_float_start_distance_threshold)
                    {
                        v_float_direction_check = true;
                    }
                }
            }
        }
    }

    public void f_sprite_float_end(Vector3 sv_target_position, float sv_move_speed, float sv_timer_rate)
    {
        if (!v_sprite_gameobject.transform.localPosition.Equals(sv_target_position))
        {
            v_sprite_gameobject.transform.localPosition = Vector3.MoveTowards(v_sprite_gameobject.transform.localPosition, sv_target_position, sv_move_speed * sv_timer_rate);
            v_float_ended = false;
        }
        else
        {
            v_float_ended = true;
        }
    }
}
