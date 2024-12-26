using System.Collections.Generic;
using UnityEngine;

public class s_entity_player_movement_sampler : MonoBehaviour
{
    [Header("Player Movement Sampler Collider Variables")]
    public List<GameObject> v_player_movement_sampler_collider_current_collisions_list;
    public GameObject v_player_movement_sampler_parent_gameobject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider sv_other_object)
    {
        if (!v_player_movement_sampler_collider_current_collisions_list.Contains(sv_other_object.gameObject) && sv_other_object.gameObject != v_player_movement_sampler_parent_gameobject)
        {
            v_player_movement_sampler_collider_current_collisions_list.Add(sv_other_object.gameObject);
        }
    }

    private void OnTriggerStay(Collider sv_other_object)
    {

    }

    private void OnTriggerExit(Collider sv_other_object)
    {
        if (v_player_movement_sampler_collider_current_collisions_list.Count > 0)
        {
            if (v_player_movement_sampler_collider_current_collisions_list.Contains(sv_other_object.gameObject))
            {
                v_player_movement_sampler_collider_current_collisions_list.Remove(sv_other_object.gameObject);
            }
        }
    }

}
