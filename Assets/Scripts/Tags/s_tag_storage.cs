using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static s_tag_library;

[Serializable]
public class sgvl_tag_storage
{
    [Header("Configurable Variables")]
    [SerializeField] public string v_tag_storage_gameobject_name;
    [Header("Reference Variables")]
    [SerializeField] public GameObject v_tag_storage_gameobject;
    [SerializeField] public s_tag_storage v_tag_storage_gameobject_script;
}

public class s_tag_storage : MonoBehaviour
{
    [Header("Tag Storage For Sprite Entity List")]
    [SerializeField] public List<GameObject> v_sprite_entity_list_object_setup;
    [SerializeField] public List<v_tags_sprite_entity_list> v_sprite_entity_list_index_setup;
}
