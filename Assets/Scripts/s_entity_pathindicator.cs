using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_entity_tag_library;

public class s_entity_pathindicator : MonoBehaviour
{
    [Header("Path Indicator Alpha Render Variables")]
    public SpriteRenderer v_pathindicator_sprite_renderer;
    [Range(0.0f, 1.0f)]
    public float v_pathindicator_sprite_renderer_alpha = 0.0f;
    [Range(0.0f, 1.0f)]
    public float v_pathindicator_sprite_renderer_alpha_target = 0.5f;
    [Range(0.0f, 1.0f)]
    public float v_pathindicator_sprite_renderer_alpha_target_max = 0.5f;
    public float v_pathindicator_sprite_renderer_alpha_increment = 0.01f;
    public float v_pathindicator_sprite_renderer_color_red = 0.0f;
    public float v_pathindicator_sprite_renderer_color_green = 0.0f;
    public float v_pathindicator_sprite_renderer_color_blue = 0.0f;

    [Header("Path Indicator Alpha Render Key Bind Setup")]
    public string v_key_manager_gameobject_name;
    public GameObject v_key_manager_gameobject;
    public bool v_pathindicator_alpha_render;
    public KeyCode v_pathindicator_alpha_render_key;
    public v_key_press_mode_list v_pathindicator_alpha_render_key_press_mode;

    [Header("Path Indicator Setup")]
    public bool v_pathindicator_walkable;
    public bool v_pathindicator_flyable;

    [Header("Path Indicator Collider Variables")]
    public List<GameObject> v_pathindicator_collider_current_collisions_list;

    [Header("Path Indicator Scene Mover Setup")]
    public bool v_pathindicator_mover;
    public GameObject v_pathindicator_mover_gameobject_destination;

    // Start is called before the first frame update
    void Start()
    {
        v_pathindicator_sprite_renderer = transform.gameObject.GetComponent<SpriteRenderer>();
        f_pathindicator_render_key_press_setup_refresh();
    }

    // Update is called once per frame
    void Update()
    {
        v_pathindicator_alpha_render = f_pathindicator_render_key_press_mode_controller(v_pathindicator_alpha_render);
        f_pathindicator_render_controller();
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        if (!v_pathindicator_collider_current_collisions_list.Contains(sv_other_object.gameObject))
        {
            v_pathindicator_collider_current_collisions_list.Add(sv_other_object.gameObject);
        }
    }

    private void OnTriggerStay(Collider sv_other_object)
    {

    }

    private void OnTriggerExit(Collider sv_other_object)
    {
        if (v_pathindicator_collider_current_collisions_list.Count > 0)
        {
            if (v_pathindicator_collider_current_collisions_list.Contains(sv_other_object.gameObject))
            {
                v_pathindicator_collider_current_collisions_list.Remove(sv_other_object.gameObject);
            }
        }
    }

    public void f_pathindicator_render_key_press_setup_refresh()
    {
        v_key_manager_gameobject = GameObject.Find(v_key_manager_gameobject_name);
        if (v_key_manager_gameobject != null)
        {
            if (v_key_manager_gameobject.GetComponent<s_entity_key_manager>() != null)
            {
                v_pathindicator_alpha_render_key = v_key_manager_gameobject.GetComponent<s_entity_key_manager>().v_pathindicator_alpha_render_key;
                v_pathindicator_alpha_render_key_press_mode = v_key_manager_gameobject.GetComponent<s_entity_key_manager>().v_pathindicator_alpha_render_key_press_mode;
            }
        }
    }

    public bool f_pathindicator_render_key_press_mode_controller(bool sv_pathindicator_render)
    {
        if (v_pathindicator_alpha_render_key_press_mode.Equals(v_key_press_mode_list.Toggle))
        {
            if (Input.GetKeyDown(v_pathindicator_alpha_render_key))
            {
                return (!sv_pathindicator_render);
            }
            else
            {
                return (sv_pathindicator_render);
            }
        }
        else if (v_pathindicator_alpha_render_key_press_mode.Equals(v_key_press_mode_list.Hold))
        {
            if (Input.GetKey(v_pathindicator_alpha_render_key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return (sv_pathindicator_render);
        }
    }

    private void f_pathindicator_render_controller()
    {
        if (v_pathindicator_sprite_renderer_alpha != v_pathindicator_sprite_renderer_alpha_target)
        {
            if (v_pathindicator_sprite_renderer_alpha > v_pathindicator_sprite_renderer_alpha_target)
            {
                if ((v_pathindicator_sprite_renderer_alpha - v_pathindicator_sprite_renderer_alpha_increment) < v_pathindicator_sprite_renderer_alpha_target)
                {
                    v_pathindicator_sprite_renderer_alpha = v_pathindicator_sprite_renderer_alpha_target;
                }
                else
                {
                    v_pathindicator_sprite_renderer_alpha -= v_pathindicator_sprite_renderer_alpha_increment;
                }
            }
            else if (v_pathindicator_sprite_renderer_alpha < v_pathindicator_sprite_renderer_alpha_target)
            {
                if ((v_pathindicator_sprite_renderer_alpha + v_pathindicator_sprite_renderer_alpha_increment) > v_pathindicator_sprite_renderer_alpha_target)
                {
                    v_pathindicator_sprite_renderer_alpha = v_pathindicator_sprite_renderer_alpha_target;
                }
                else
                {
                    v_pathindicator_sprite_renderer_alpha += v_pathindicator_sprite_renderer_alpha_increment;
                }
            }
        }

        v_pathindicator_sprite_renderer_color_red = v_pathindicator_sprite_renderer.color.r;
        v_pathindicator_sprite_renderer_color_green = v_pathindicator_sprite_renderer.color.g;
        v_pathindicator_sprite_renderer_color_blue = v_pathindicator_sprite_renderer.color.b;
        v_pathindicator_sprite_renderer.color = new Color(v_pathindicator_sprite_renderer.color.r, v_pathindicator_sprite_renderer.color.g, v_pathindicator_sprite_renderer.color.b, v_pathindicator_sprite_renderer_alpha);

        if (v_pathindicator_alpha_render)
        {
            v_pathindicator_sprite_renderer_alpha_target = v_pathindicator_sprite_renderer_alpha_target_max;
        }
        else
        {
            v_pathindicator_sprite_renderer_alpha_target = 0;
        }
    }
}
