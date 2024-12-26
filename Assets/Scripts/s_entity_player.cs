using System.Collections.Generic;
using UnityEngine;
using static s_entity_tag_library;
using static s_entity_time_controller;

public class s_entity_player : MonoBehaviour
{
    [Header("Player Time Level Setup")]
    public GameObject v_entity_time_gameobject;
    public s_entity_time_controller v_entity_time_gameobject_script;
    public v_time_level_list v_player_time_level;

    [Header("Player Information Setup")]
    public GameObject v_entity_tag_gameobject;
    public s_entity_tag_library v_entity_tag_gameobject_script;
    public List<v_entity_tag_list> v_player_tag_list;
    public v_entity_list v_player_entity_type;

    [Header("Camera Information Setup")]
    public GameObject v_player_camera_gameobject;

    [Header("Camera Joystick Information Setup")]
    public GameObject v_player_camera_joystick_gameobject;

    [Header("Player Display Setup")]
    public GameObject v_player_sprite_display_gameobject;

    [Header("Player Collider Variables")]
    public List<GameObject> v_player_collider_current_collisions_list;

    [Header("Player Movement Variables")]
    public GameObject v_player_movement_sampler_gameobject;
    public s_entity_player_movement_sampler v_player_movement_sampler_gameobject_script;
    public string v_path_indicators_list_gameobject_name;
    public List<GameObject> v_player_path_indicators_list;
    public float v_player_move_speed = 0.0f;
    public string v_player_last_faced_direction = "down";
    public v_movement_mode_list v_player_current_movement_mode;
    public List<KeyCode> v_player_movement_key_list;
    public int v_player_movement_key_list_index = 0;
    public bool v_player_movement_key_list_index_direction_bool = false;
    public List<Vector3> v_player_movement_position_list;
    public float v_player_movement_position_distance_threshold;
    public float v_player_movement_position_list_clear_counter;

    [Header("Player Collider Box Variables")]
    public BoxCollider v_player_collider;
    public Vector3 v_player_collider_size_default;
    public Vector3 v_player_collider_size_mote;

    [Header("Player Movement Key Bind Setup")]
    public string v_key_manager_gameobject_name;
    public GameObject v_key_manager_gameobject;
    public KeyCode v_player_move_up_key;
    public KeyCode v_player_move_left_key;
    public KeyCode v_player_move_down_key;
    public KeyCode v_player_move_right_key;

    [Header("Player Debug Setup")]
    public bool v_debug_render_enabled;
    public List<GameObject> v_debug_player_gameobjects;

    // Start is called before the first frame update
    void Start()
    {
        v_entity_time_gameobject = GameObject.Find("entity_time_controller");
        if (v_entity_time_gameobject != null)
        {
            v_entity_time_gameobject_script = v_entity_time_gameobject.GetComponent<s_entity_time_controller>();
        }

        v_entity_tag_gameobject = GameObject.Find("entity_tag_library");
        if (v_entity_tag_gameobject != null)
        {
            v_entity_tag_gameobject_script = v_entity_tag_gameobject.GetComponent<s_entity_tag_library>();
        }

        v_player_camera_gameobject = GameObject.Find("entity_camera");
        v_player_camera_joystick_gameobject = GameObject.Find("entity_camera_joystick");

        v_player_collider = transform.GetComponent<BoxCollider>();
        v_player_movement_sampler_gameobject_script = v_player_movement_sampler_gameobject.GetComponent<s_entity_player_movement_sampler>();
    }

    // Update is called once per frame
    void Update()
    {
        f_player_movement_keys_setup_refresh();
        f_player_path_indicators_list_refresh();

        f_player_tag_manager();
        f_player_display_handler();

        f_player_movement_handler();

        f_player_scene_mover_handler();

        f_player_collider_size_handler();

        f_player_debug_renderer_controller(v_debug_render_enabled);
    }

    public void f_player_tag_manager()
    {
        if (v_player_entity_type == v_entity_list.entity_mote)
        {
            if
            (
                v_player_tag_list.Contains(v_entity_tag_list.Alive) && 
                v_player_tag_list.Contains(v_entity_tag_list.Birth)
            )
            {
                if (v_player_sprite_display_gameobject.transform.childCount == 1)
                {
                    if (v_player_sprite_display_gameobject.transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_ended)
                    {
                        v_player_tag_list.Remove(v_entity_tag_list.Birth);
                        if (!v_player_tag_list.Contains(v_entity_tag_list.Idle))
                        {
                            v_player_tag_list.Add(v_entity_tag_list.Idle);
                        }
                        if (!v_player_tag_list.Contains(v_entity_tag_list.CanWalk))
                        {
                            v_player_tag_list.Add(v_entity_tag_list.CanWalk);
                        }
                    }
                }
            }
        }
    }

    public void f_player_display_handler()
    {
        if (v_player_sprite_display_gameobject != null)
        {
            if (v_player_entity_type == v_entity_list.entity_mote && v_player_tag_list.Count > 0)
            {
                if (v_player_sprite_display_gameobject.transform.childCount == 1)
                {
                    if (v_player_tag_list.Contains(v_entity_tag_list.Dead))
                    {
                        foreach (Transform child in v_player_sprite_display_gameobject.transform)
                        {
                            Destroy(child.gameObject);
                        }
                    }
                    else if (v_player_tag_list.Contains(v_entity_tag_list.Alive) && v_player_tag_list.Contains(v_entity_tag_list.Birth))
                    {
                        GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_mote_gameobject_birth;
                        if (!v_player_sprite_display_gameobject.transform.GetChild(0).gameObject.transform.name.Contains(tv_target_gameobject.transform.name))
                        {
                            Destroy(v_player_sprite_display_gameobject.transform.GetChild(0).gameObject);
                            Instantiate(tv_target_gameobject, v_player_sprite_display_gameobject.transform);
                        }
                    }
                    else if (v_player_tag_list.Contains(v_entity_tag_list.Alive) && v_player_tag_list.Contains(v_entity_tag_list.Idle))
                    {
                        GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_mote_gameobject_idle;
                        if (!v_player_sprite_display_gameobject.transform.GetChild(0).gameObject.transform.name.Contains(tv_target_gameobject.transform.name))
                        {
                            Destroy(v_player_sprite_display_gameobject.transform.GetChild(0).gameObject);
                            Instantiate(tv_target_gameobject, v_player_sprite_display_gameobject.transform);
                        }
                    }
                    else
                    {
                        foreach (Transform child in v_player_sprite_display_gameobject.transform)
                        {
                            Destroy(child.gameObject);
                        }
                    }
                }
                else
                {
                    if (v_player_sprite_display_gameobject.transform.childCount > 1)
                    {
                        foreach (Transform child in v_player_sprite_display_gameobject.transform)
                        {
                            Destroy(child.gameObject);
                        }
                    }

                    if (v_player_tag_list.Contains(v_entity_tag_list.Dead))
                    {
                        
                    }
                    else if (v_player_tag_list.Contains(v_entity_tag_list.Alive) && v_player_tag_list.Contains(v_entity_tag_list.Birth))
                    {
                        GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_mote_gameobject_birth;
                        Instantiate(tv_target_gameobject, v_player_sprite_display_gameobject.transform);
                    }
                    else if (v_player_tag_list.Contains(v_entity_tag_list.Alive) && v_player_tag_list.Contains(v_entity_tag_list.Idle))
                    {
                        GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_mote_gameobject_idle;
                        Instantiate(tv_target_gameobject, v_player_sprite_display_gameobject.transform);
                    }
                }
            }
            else
            {
                if (v_player_sprite_display_gameobject.transform.childCount > 0)
                {
                    foreach (Transform child in v_player_sprite_display_gameobject.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
            }

            if (v_player_sprite_display_gameobject.transform.childCount > 0)
            {
                foreach (Transform child in v_player_sprite_display_gameobject.transform)
                {
                    child.gameObject.GetComponent<s_entity_animation_handler>().v_entity_time_level = v_player_time_level;
                }
            }
        }
    }

    public void f_player_movement_handler()
    {
        f_player_movement_command_verify(v_player_move_up_key);
        f_player_movement_command_verify(v_player_move_left_key);
        f_player_movement_command_verify(v_player_move_down_key);
        f_player_movement_command_verify(v_player_move_right_key);

        if
        (
            (
                v_player_tag_list.Contains(v_entity_tag_list.CanWalk) ||
                v_player_tag_list.Contains(v_entity_tag_list.CanFly)
            )
        )
        {
            if (v_player_movement_key_list.Count >= 1)
            {
                if (v_player_movement_key_list_index >= v_player_movement_key_list.Count)
                {
                    v_player_movement_key_list_index = 0;
                }

                if (v_player_movement_key_list[v_player_movement_key_list_index] == v_player_move_up_key)
                {
                    v_player_movement_sampler_gameobject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + -1);
                    v_player_last_faced_direction = "up";
                }
                else if (v_player_movement_key_list[v_player_movement_key_list_index] == v_player_move_left_key)
                {
                    v_player_movement_sampler_gameobject.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                    v_player_last_faced_direction = "left";
                }
                else if (v_player_movement_key_list[v_player_movement_key_list_index] == v_player_move_down_key)
                {
                    v_player_movement_sampler_gameobject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                    v_player_last_faced_direction = "down";
                }
                else if (v_player_movement_key_list[v_player_movement_key_list_index] == v_player_move_right_key)
                {
                    v_player_movement_sampler_gameobject.transform.position = new Vector3(transform.position.x + -1, transform.position.y, transform.position.z);
                    v_player_last_faced_direction = "right";
                }
                else
                {
                    v_player_movement_sampler_gameobject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                }

                if (v_player_movement_sampler_gameobject_script.v_player_movement_sampler_collider_current_collisions_list.Count == 0)
                {
                    if (v_player_movement_key_list_index_direction_bool)
                    {
                        if ((v_player_movement_key_list_index - 1) >= 0)
                        {
                            v_player_movement_key_list_index -= 1;
                        }
                    }
                    else
                    {
                        if ((v_player_movement_key_list_index + 1) < v_player_movement_key_list.Count)
                        {
                            v_player_movement_key_list_index += 1;
                        }
                    }

                    if (v_player_movement_key_list_index >= (v_player_movement_key_list.Count - 1))
                    {
                        v_player_movement_key_list_index_direction_bool = true;
                    }
                    else if (v_player_movement_key_list_index <= 0)
                    {
                        v_player_movement_key_list_index_direction_bool = false;
                    }
                }
                else
                {
                    if ((v_player_movement_key_list_index - 1) >= 0)
                    {
                        v_player_movement_key_list_index -= 1;
                    }
                }
            }
            else
            {
                v_player_movement_key_list_index = 0;
                v_player_movement_sampler_gameobject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }

        f_player_movement_list_reader();
        f_player_movement_direction_verify();
    }

    public void f_player_movement_command_verify(KeyCode sv_key)
    {
        if (Input.GetKey(sv_key))
        {
            if (!v_player_movement_key_list.Contains(sv_key))
            {
                v_player_movement_key_list.Insert(0, sv_key);
            }
        }
        else
        {
            if (v_player_movement_key_list.Contains(sv_key))
            {
                v_player_movement_key_list.Remove(sv_key);
            }
        }
    }

    public void f_player_movement_direction_verify()
    {
        if (v_player_path_indicators_list.Count > 0)
        {
            if (v_player_movement_key_list.Count > 0)
            {
                if (v_player_movement_position_list.Count == 0)
                {
                    if (v_player_movement_sampler_gameobject_script.v_player_movement_sampler_collider_current_collisions_list.Count > 0)
                    {
                        foreach (GameObject lv_gameobject in v_player_path_indicators_list)
                        {
                            foreach (GameObject lv_sampler_gameobject in v_player_movement_sampler_gameobject_script.v_player_movement_sampler_collider_current_collisions_list)
                            {
                                if (lv_sampler_gameobject == lv_gameobject)
                                {
                                    if (lv_gameobject.transform.position != transform.position)
                                    {
                                        if
                                        (
                                            (
                                                lv_gameobject.GetComponent<s_entity_pathindicator>().v_pathindicator_walkable &&
                                                v_player_tag_list.Contains(v_entity_tag_list.CanWalk) &&
                                                v_player_current_movement_mode == v_movement_mode_list.Walking
                                            ) ||
                                            (
                                                lv_gameobject.GetComponent<s_entity_pathindicator>().v_pathindicator_flyable &&
                                                v_player_tag_list.Contains(v_entity_tag_list.CanFly) &&
                                                v_player_current_movement_mode == v_movement_mode_list.Flying
                                            )
                                        )
                                        {
                                            if (!v_player_movement_position_list.Contains(lv_gameobject.transform.position))
                                            {
                                                v_player_movement_position_list.Add(lv_gameobject.transform.position);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (v_player_movement_position_list.Count == 1)
                {
                    if (v_player_movement_sampler_gameobject_script.v_player_movement_sampler_collider_current_collisions_list.Count > 0)
                    {
                        foreach (GameObject lv_gameobject in v_player_path_indicators_list)
                        {
                            foreach (GameObject lv_sampler_gameobject in v_player_movement_sampler_gameobject_script.v_player_movement_sampler_collider_current_collisions_list)
                            {
                                if (lv_sampler_gameobject == lv_gameobject)
                                {
                                    if (lv_gameobject.transform.position != transform.position)
                                    {
                                        if
                                        (
                                            (
                                                lv_gameobject.GetComponent<s_entity_pathindicator>().v_pathindicator_walkable &&
                                                v_player_tag_list.Contains(v_entity_tag_list.CanWalk) &&
                                                v_player_current_movement_mode == v_movement_mode_list.Walking
                                            ) ||
                                            (
                                                lv_gameobject.GetComponent<s_entity_pathindicator>().v_pathindicator_flyable &&
                                                v_player_tag_list.Contains(v_entity_tag_list.CanFly) &&
                                                v_player_current_movement_mode == v_movement_mode_list.Flying
                                            )
                                        )
                                        {
                                            if (!v_player_movement_position_list.Contains(lv_gameobject.transform.position))
                                            {
                                                if (v_player_movement_position_list[0] == transform.position || (Vector3.Distance(transform.position, v_player_movement_position_list[0]) < v_player_movement_position_distance_threshold))
                                                {
                                                    v_player_movement_position_list[0] = lv_gameobject.transform.position;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (v_player_movement_position_list[0] == transform.position)
                    {
                        v_player_movement_position_list.Clear();
                        v_player_movement_position_list_clear_counter += 1;
                    }
                }
            }
            else
            {
                if (v_player_movement_position_list.Count > 0)
                {
                    if (v_player_movement_position_list[0] == transform.position)
                    {
                        v_player_movement_position_list.Clear();
                        v_player_movement_position_list_clear_counter += 1;
                    }
                }
            }
        }
    }

    public void f_player_movement_list_reader()
    {
        if (v_player_movement_position_list.Count > 0)
        {
            float tv_determined_speed = (float)(v_player_move_speed * 0.00001);
            f_player_move_towards(v_player_movement_position_list[0], tv_determined_speed, v_entity_time_gameobject_script.f_time_level_timer_rate_get(v_player_time_level));
        }
    }
    
    public void f_player_move_towards(Vector3 sv_target_position, float sv_move_speed, float sv_timer_rate)
    {
        if (!transform.position.Equals(sv_target_position))
        {
            transform.position = Vector3.MoveTowards(transform.position, sv_target_position, sv_move_speed * sv_timer_rate);
        }
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        if (!v_player_collider_current_collisions_list.Contains(sv_other_object.gameObject) && sv_other_object.gameObject != v_player_movement_sampler_gameobject)
        {
            v_player_collider_current_collisions_list.Add(sv_other_object.gameObject);
        }
    }

    private void OnTriggerStay(Collider sv_other_object)
    {

    }

    private void OnTriggerExit(Collider sv_other_object)
    {
        if (v_player_collider_current_collisions_list.Count > 0)
        {
            if (v_player_collider_current_collisions_list.Contains(sv_other_object.gameObject))
            {
                v_player_collider_current_collisions_list.Remove(sv_other_object.gameObject);
            }
        }
    }

    public void f_player_movement_keys_setup_refresh()
    {
        v_key_manager_gameobject = GameObject.Find(v_key_manager_gameobject_name);
        if (v_key_manager_gameobject != null)
        {
            if (v_key_manager_gameobject.GetComponent<s_entity_key_manager>() != null)
            {
                v_player_move_up_key = v_key_manager_gameobject.GetComponent<s_entity_key_manager>().v_player_move_up_key;
                v_player_move_left_key = v_key_manager_gameobject.GetComponent<s_entity_key_manager>().v_player_move_left_key;
                v_player_move_down_key = v_key_manager_gameobject.GetComponent<s_entity_key_manager>().v_player_move_down_key;
                v_player_move_right_key = v_key_manager_gameobject.GetComponent<s_entity_key_manager>().v_player_move_right_key;
            }
        }
    }

    public void f_player_collider_size_handler()
    {
        if (v_player_entity_type == v_entity_list.entity_mote)
        {
            v_player_collider.size = v_player_collider_size_mote;
        }
        else
        {
            v_player_collider.size = v_player_collider_size_default;
        }
    }

    public void f_player_scene_mover_handler()
    {
        if (v_player_collider_current_collisions_list.Count > 0)
        {
            foreach (GameObject lv_gameobject in v_player_collider_current_collisions_list)
            {
                if (v_player_collider_current_collisions_list.Contains(lv_gameobject))
                {
                    if (lv_gameobject.GetComponent<s_entity_pathindicator>().v_pathindicator_mover)
                    {
                        v_player_camera_gameobject.transform.position = lv_gameobject.GetComponent<s_entity_pathindicator>().v_pathindicator_mover_gameobject_destination.transform.position;
                        v_player_camera_joystick_gameobject.transform.position = lv_gameobject.GetComponent<s_entity_pathindicator>().v_pathindicator_mover_gameobject_destination.transform.position;
                        transform.position = lv_gameobject.GetComponent<s_entity_pathindicator>().v_pathindicator_mover_gameobject_destination.transform.position;
                        v_player_movement_sampler_gameobject.transform.localPosition = Vector3.zero;
                        v_player_movement_position_list.Clear();
                        v_player_movement_key_list.Clear();
                    }
                }
            }
        }
    }

    public void f_player_path_indicators_list_refresh()
    {
        GameObject tv_path_indicators_list_gameobject = GameObject.Find(v_path_indicators_list_gameobject_name);
        if (tv_path_indicators_list_gameobject != null)
        {
            foreach (Transform child in tv_path_indicators_list_gameobject.transform)
            {
                if (!v_player_path_indicators_list.Contains(child.gameObject))
                {
                    v_player_path_indicators_list.Add(child.gameObject);
                }
            }
        }
    }

    private void f_player_debug_renderer_controller(bool sv_is_allowed)
    {
        foreach (GameObject item in v_debug_player_gameobjects)
        {
            foreach (Renderer r in item.GetComponentsInChildren<Renderer>())
            {
                r.enabled = sv_is_allowed;
            }
        }
    }
}
