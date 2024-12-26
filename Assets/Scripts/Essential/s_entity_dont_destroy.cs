using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_entity_dont_destroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
