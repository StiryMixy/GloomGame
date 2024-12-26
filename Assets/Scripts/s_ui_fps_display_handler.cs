using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class s_ui_fps_display_handler : MonoBehaviour
{
    [Header("FPS Display Setup")]
    public TextMeshProUGUI v_fps_text;
    public float v_fps_poll_time = 1f;
    public float v_fps_time;
    public int v_fps_frame_count;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        v_fps_time += Time.deltaTime;
        v_fps_frame_count++;
        if (v_fps_time >= v_fps_poll_time)
        {
            int sv_framerate = Mathf.RoundToInt(v_fps_frame_count / v_fps_time);
            v_fps_text.text = sv_framerate.ToString();

            v_fps_time -= v_fps_poll_time;
            v_fps_frame_count = 0;
        }
    }
}
