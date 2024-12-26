using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_design_sorter : MonoBehaviour
{
    [Header("Sorting Order Setup")]
    public int v_sort_multiplier;
    public bool v_enable_parent = false;
    public bool v_enable_parent_root = true;
    public GameObject v_available_parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (v_enable_parent)
        {
            if (v_enable_parent_root)
            {
                v_available_parent = transform.root.gameObject;
            }
            else
            {
                v_available_parent = transform.parent.gameObject;
            }

            if (v_available_parent != null)
            {
                this.transform.GetComponent<SpriteRenderer>().sortingOrder = (int)(v_available_parent.transform.position.z * v_sort_multiplier);
            }
            else
            {
                this.transform.GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.z * v_sort_multiplier);
            }
        }
        else
        {
            this.transform.GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.z * v_sort_multiplier);
        }
    }
}
