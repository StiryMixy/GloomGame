using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_tag_library : MonoBehaviour
{
    public enum v_tags_timer_level_list
    {
        Basic,
        Trained,
        Mutated,
        Magical,
        Omniscient,
        Metaphysical
    };

    public enum v_tags_sprite_entity_list
    {
        entity_null,
        entity_mote,
        entity_sophi_bloody,
    };

    public enum v_tags_sprite_orientation_list
    {
        Front,
        Back,
        None,
    };

    public enum v_tags_sprite_profile_list
    {
        Right,
        Left,
        None,
    };

    public enum v_tags_sprite_state_list
    {
        Idle,
        Walk,
        Run,
        Dodge,
        None,
    };

    public enum v_tags_movement_mode_list
    {
        Walking,
        Flying,
        WalkingAndFlying,
        None,
    };

    public enum v_tags_key_press_mode_list
    {
        Toggle,
        Hold,
    };

    public enum v_tags_entity_direction_list
    {
        Backward,
        Forward,
        Right,
        Left,
    };
}
