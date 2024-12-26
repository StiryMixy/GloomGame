using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_scene_handler_ground0 : MonoBehaviour
{
    [Header("Scene Handler Setup")]
    public bool v_scene_enabled = true;
    public string v_scene_target;

    // Start is called before the first frame update
    void Start()
    {
        if (v_scene_enabled)
        {
            SceneManager.LoadScene(sceneName: v_scene_target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
