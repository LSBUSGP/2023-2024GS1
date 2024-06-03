using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure to include this namespace for TextMeshProUGUI

public class Fps : MonoBehaviour
{
    private float fps;
    public TextMeshProUGUI fpsCounterText; // Corrected variable name and added public access specifier

    void Start()
    {
        InvokeRepeating("GetFps", 1f, 1f); // Adjusted the time parameters to match the method signature
    }

    void GetFps()
    {
        fps = 1f / Time.unscaledDeltaTime;
        fpsCounterText.text = "FPS: " + Mathf.Round(fps).ToString(); // Round the FPS value for readability
    }
}
