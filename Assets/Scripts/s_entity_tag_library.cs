using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_entity_tag_library : MonoBehaviour
{
    [Header("Entity Mote Game Object List")]
    public GameObject v_entity_mote_gameobject_birth;
    public GameObject v_entity_mote_gameobject_idle;

    [Header("Entity Scene Mover Game Object List")]
    public GameObject v_entity_scene_mover_gameobject_birth;
    public GameObject v_entity_scene_mover_gameobject_idle;
    public GameObject v_entity_scene_mover_gameobject_death;

    [Header("Entity Mote Crystal Game Object List")]
    public GameObject v_entity_mote_crystal_gameobject_idle;
    public GameObject v_entity_mote_crystal_gameobject_action1;

    public enum v_entity_list
    {
        entity_none,
        entity_mote,
        entity_scene_mover,
        entity_mote_crystal
    };

    public enum v_entity_tag_list
    {
        CanWalk,
        CanFly,
        Birth,
        Idle,
        IsWalking,
        IsFlying,
        Dead,
        Dying,
        Alive,
        PerformingAction1
    };

    public enum v_movement_mode_list
    {
        Walking,
        Flying,
        None
    };

    public enum v_key_press_mode_list
    {
        Toggle,
        Hold
    };
}
