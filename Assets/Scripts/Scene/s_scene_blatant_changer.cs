using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_scene_blatant_changer : MonoBehaviour
{
    [Header("Scene Blatant Changer Setup")]
    [SerializeField] public bool v_scene_blatant_changer_enabled = true;
    [SerializeField] public string v_scene_blatant_changer_target;

    void Start()
    {
        if (v_scene_blatant_changer_enabled)
        {
            SceneManager.LoadScene(sceneName: v_scene_blatant_changer_target);
        }
    }
}
