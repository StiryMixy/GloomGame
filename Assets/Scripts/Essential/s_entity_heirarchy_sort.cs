using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_entity_heirarchy_sort : MonoBehaviour
{
    [Header("Entity Sort Order Setup")]
    [SerializeField] public int v_entity_sort_order;
    void Start()
    {
        transform.SetSiblingIndex(v_entity_sort_order);
    }
}
