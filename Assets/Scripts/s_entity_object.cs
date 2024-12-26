using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_entity_tag_library;
using static s_entity_time_controller;

public class s_entity_object : MonoBehaviour
{
    [Header("Object Time Level Setup")]
    public GameObject v_entity_time_gameobject;
    public s_entity_time_controller v_entity_time_gameobject_script;
    public v_time_level_list v_object_time_level;

    [Header("Object Information Setup")]
    public GameObject v_entity_tag_gameobject;
    public s_entity_tag_library v_entity_tag_gameobject_script;
    public List<v_entity_tag_list> v_object_tag_list;
    public v_entity_list v_object_entity_type;

    [Header("Player Information Setup")]
    public GameObject v_entity_player_gameobject;
    public bool v_entity_player_gameobject_distance_relative;
    public float v_entity_player_gameobject_distance;
    public float v_entity_player_gameobject_distance_threshold;
    public bool v_entity_player_gameobject_tag_relative;
    public bool v_entity_player_gameobject_tag_birth_check = true;

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

        v_entity_player_gameobject = GameObject.Find("entity_player");
    }

    // Update is called once per frame
    void Update()
    {
        f_object_tag_manager();
        f_object_display_handler();
    }

    public void f_object_tag_manager()
    {
        if (v_object_entity_type == v_entity_list.entity_scene_mover)
        {
            if (v_entity_player_gameobject_distance_relative)
            {
                v_entity_player_gameobject_distance = Vector3.Distance(transform.position, v_entity_player_gameobject.transform.position);
                if (v_entity_player_gameobject_distance <= v_entity_player_gameobject_distance_threshold)
                {
                    if (transform.childCount < 1)
                    {
                        if (!v_object_tag_list.Contains(v_entity_tag_list.Birth) && !v_object_tag_list.Contains(v_entity_tag_list.Idle) && !v_object_tag_list.Contains(v_entity_tag_list.Dead))
                        {
                            v_object_tag_list.Add(v_entity_tag_list.Birth);
                        }
                    }
                    else
                    {
                        if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                        {
                            transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_loop = true;
                        }

                        if (v_object_tag_list.Contains(v_entity_tag_list.Birth))
                        {
                            if (transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_ended)
                            {
                                v_object_tag_list.Remove(v_entity_tag_list.Birth);
                                if (!v_object_tag_list.Contains(v_entity_tag_list.Idle) && !v_object_tag_list.Contains(v_entity_tag_list.Dead))
                                {
                                    v_object_tag_list.Add(v_entity_tag_list.Idle);
                                }
                            }
                        }
                        else if (v_object_tag_list.Contains(v_entity_tag_list.Dead))
                        {
                            if (!transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_ended)
                            {
                                v_object_tag_list.Remove(v_entity_tag_list.Dead);
                                if (!v_object_tag_list.Contains(v_entity_tag_list.Birth) && !v_object_tag_list.Contains(v_entity_tag_list.Idle))
                                {
                                    v_object_tag_list.Add(v_entity_tag_list.Idle);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (transform.childCount > 0)
                    {
                        if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                        {
                            transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_loop = false;
                        }

                        if (v_object_tag_list.Contains(v_entity_tag_list.Birth))
                        {
                            if (transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_ended)
                            {
                                v_object_tag_list.Remove(v_entity_tag_list.Birth);
                                if (!v_object_tag_list.Contains(v_entity_tag_list.Idle) && !v_object_tag_list.Contains(v_entity_tag_list.Dead))
                                {
                                    v_object_tag_list.Add(v_entity_tag_list.Idle);
                                }
                            }
                        }
                        else if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                        {
                            if (transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_ended)
                            {
                                v_object_tag_list.Remove(v_entity_tag_list.Idle);
                                if (!v_object_tag_list.Contains(v_entity_tag_list.Birth) && !v_object_tag_list.Contains(v_entity_tag_list.Dead))
                                {
                                    v_object_tag_list.Add(v_entity_tag_list.Dead);
                                }
                            }
                        }
                        else if (v_object_tag_list.Contains(v_entity_tag_list.Dead))
                        {
                            if (transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_ended)
                            {
                                v_object_tag_list.Remove(v_entity_tag_list.Dead);
                            }
                        }
                    }
                }
            }
            else
            {
                if (transform.childCount > 0)
                {
                    if (v_object_tag_list.Contains(v_entity_tag_list.Birth))
                    {
                        if (transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_ended)
                        {
                            v_object_tag_list.Remove(v_entity_tag_list.Birth);
                            if (!v_object_tag_list.Contains(v_entity_tag_list.Idle))
                            {
                                v_object_tag_list.Add(v_entity_tag_list.Idle);
                            }
                        }
                    }
                }
            }
        }
        else if (v_object_entity_type == v_entity_list.entity_mote_crystal)
        {
            if (v_entity_player_gameobject_tag_relative)
            {
                if (v_entity_player_gameobject.GetComponent<s_entity_player>().v_player_tag_list.Contains(v_entity_tag_list.Birth))
                {
                    if (v_entity_player_gameobject_tag_birth_check)
                    {
                        if (transform.childCount < 1)
                        {
                            if (!v_object_tag_list.Contains(v_entity_tag_list.PerformingAction1))
                            {
                                v_entity_player_gameobject_tag_birth_check = false;
                                v_object_tag_list.Add(v_entity_tag_list.PerformingAction1);
                            }
                        }
                        else
                        {
                            if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                            {
                                transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_float_end_enabled = true;
                            }

                            if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                            {
                                if (transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_float_ended)
                                {
                                    v_object_tag_list.Remove(v_entity_tag_list.Idle);
                                    if (!v_object_tag_list.Contains(v_entity_tag_list.PerformingAction1))
                                    {
                                        v_object_tag_list.Add(v_entity_tag_list.PerformingAction1);
                                    }
                                }
                            }
                            else if (v_object_tag_list.Contains(v_entity_tag_list.PerformingAction1))
                            {
                                v_entity_player_gameobject_tag_birth_check = false;
                                if (transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_ended)
                                {
                                    v_object_tag_list.Remove(v_entity_tag_list.PerformingAction1);
                                    if (!v_object_tag_list.Contains(v_entity_tag_list.Idle))
                                    {
                                        v_object_tag_list.Add(v_entity_tag_list.Idle);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (transform.childCount < 1)
                        {
                            if (!v_object_tag_list.Contains(v_entity_tag_list.Idle))
                            {
                                v_object_tag_list.Add(v_entity_tag_list.Idle);
                            }
                        }
                        else
                        {
                            if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                            {
                                transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_float_end_enabled = false;
                            }

                            if (v_object_tag_list.Contains(v_entity_tag_list.PerformingAction1))
                            {
                                if (transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_ended)
                                {
                                    v_object_tag_list.Remove(v_entity_tag_list.PerformingAction1);
                                    if (!v_object_tag_list.Contains(v_entity_tag_list.Idle))
                                    {
                                        v_object_tag_list.Add(v_entity_tag_list.Idle);
                                    }
                                }
                            }
                            else if (!v_object_tag_list.Contains(v_entity_tag_list.Idle))
                            {
                                v_object_tag_list.Add(v_entity_tag_list.Idle);
                            }
                        }
                    }
                }
                else
                {
                    v_entity_player_gameobject_tag_birth_check = true;
                    if (transform.childCount < 1)
                    {
                        if (!v_object_tag_list.Contains(v_entity_tag_list.Idle))
                        {
                            v_object_tag_list.Add(v_entity_tag_list.Idle);
                        }
                    }
                    else
                    {
                        if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                        {
                            transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_float_end_enabled = false;
                        }

                        if (v_object_tag_list.Contains(v_entity_tag_list.PerformingAction1))
                        {
                            if (transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_ended)
                            {
                                v_object_tag_list.Remove(v_entity_tag_list.PerformingAction1);
                                if (!v_object_tag_list.Contains(v_entity_tag_list.Idle))
                                {
                                    v_object_tag_list.Add(v_entity_tag_list.Idle);
                                }
                            }
                        }
                        else if (!v_object_tag_list.Contains(v_entity_tag_list.Idle))
                        {
                            v_object_tag_list.Add(v_entity_tag_list.Idle);
                        }
                    }
                }
            }
            else
            {
                v_entity_player_gameobject_tag_birth_check = true;
                if (transform.childCount < 1)
                {
                    if (!v_object_tag_list.Contains(v_entity_tag_list.Idle))
                    {
                        v_object_tag_list.Add(v_entity_tag_list.Idle);
                    }
                }
                else
                {
                    if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                    {
                        transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_float_end_enabled = false;
                    }

                    if (v_object_tag_list.Contains(v_entity_tag_list.PerformingAction1))
                    {
                        if (transform.GetChild(0).gameObject.GetComponent<s_entity_animation_handler>().v_sprite_frame_ended)
                        {
                            v_object_tag_list.Remove(v_entity_tag_list.PerformingAction1);
                            if (!v_object_tag_list.Contains(v_entity_tag_list.Idle))
                            {
                                v_object_tag_list.Add(v_entity_tag_list.Idle);
                            }
                        }
                    }
                    else if (!v_object_tag_list.Contains(v_entity_tag_list.Idle))
                    {
                        v_object_tag_list.Add(v_entity_tag_list.Idle);
                    }
                }
            }
        }
    }

    public void f_object_display_handler()
    {
        if (v_object_entity_type == v_entity_list.entity_scene_mover && v_object_tag_list.Count > 0)
        {
            if (transform.childCount == 1)
            {
                if (v_object_tag_list.Contains(v_entity_tag_list.Birth))
                {
                    GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_scene_mover_gameobject_birth;
                    if (!transform.GetChild(0).gameObject.transform.name.Contains(tv_target_gameobject.transform.name))
                    {
                        Destroy(transform.GetChild(0).gameObject);
                        Instantiate(tv_target_gameobject, transform);
                    }
                }
                else if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                {
                    GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_scene_mover_gameobject_idle;
                    if (!transform.GetChild(0).gameObject.transform.name.Contains(tv_target_gameobject.transform.name))
                    {
                        Destroy(transform.GetChild(0).gameObject);
                        Instantiate(tv_target_gameobject, transform);
                    }
                }
                else if (v_object_tag_list.Contains(v_entity_tag_list.Dead))
                {
                    GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_scene_mover_gameobject_death;
                    if (!transform.GetChild(0).gameObject.transform.name.Contains(tv_target_gameobject.transform.name))
                    {
                        Destroy(transform.GetChild(0).gameObject);
                        Instantiate(tv_target_gameobject, transform);
                    }
                }
                else
                {
                    foreach (Transform child in transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
            else
            {
                if (transform.childCount > 1)
                {
                    foreach (Transform child in transform)
                    {
                        Destroy(child.gameObject);
                    }
                }

                if (v_object_tag_list.Contains(v_entity_tag_list.Birth))
                {
                    GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_scene_mover_gameobject_birth;
                    Instantiate(tv_target_gameobject, transform);
                }
                else if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                {
                    GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_scene_mover_gameobject_idle;
                    Instantiate(tv_target_gameobject, transform);
                }
                else if (v_object_tag_list.Contains(v_entity_tag_list.Dead))
                {
                    GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_scene_mover_gameobject_death;
                    Instantiate(tv_target_gameobject, transform);
                }
            }
        }
        else if (v_object_entity_type == v_entity_list.entity_mote_crystal && v_object_tag_list.Count > 0)
        {
            if (transform.childCount == 1)
            {
                if (v_object_tag_list.Contains(v_entity_tag_list.PerformingAction1))
                {
                    GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_mote_crystal_gameobject_action1;
                    if (!transform.GetChild(0).gameObject.transform.name.Contains(tv_target_gameobject.transform.name))
                    {
                        Destroy(transform.GetChild(0).gameObject);
                        Instantiate(tv_target_gameobject, transform);
                    }
                }
                else if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                {
                    GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_mote_crystal_gameobject_idle;
                    if (!transform.GetChild(0).gameObject.transform.name.Contains(tv_target_gameobject.transform.name))
                    {
                        Destroy(transform.GetChild(0).gameObject);
                        Instantiate(tv_target_gameobject, transform);
                    }
                }
                else
                {
                    foreach (Transform child in transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
            else
            {
                if (transform.childCount > 1)
                {
                    foreach (Transform child in transform)
                    {
                        Destroy(child.gameObject);
                    }
                }

                if (v_object_tag_list.Contains(v_entity_tag_list.PerformingAction1))
                {
                    GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_mote_crystal_gameobject_action1;
                    Instantiate(tv_target_gameobject, transform);
                }
                else if (v_object_tag_list.Contains(v_entity_tag_list.Idle))
                {
                    GameObject tv_target_gameobject = v_entity_tag_gameobject_script.v_entity_mote_crystal_gameobject_idle;
                    Instantiate(tv_target_gameobject, transform);
                }
            }
        }
        else
        {
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<s_entity_animation_handler>().v_entity_time_level = v_object_time_level;
            }
        }
    }
}
