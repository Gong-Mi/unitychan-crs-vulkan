using UnityEngine;
using System.Collections;

public class GlobalConfig : MonoBehaviour
{
    public int targetFrameRate = 120;
    public int shaderLOD = 1000;

    void Awake()
    {
        // Adapt core utilization & thread priority
        Application.backgroundLoadingPriority = ThreadPriority.High;

        // Display refresh rate adaptation for Vulkan / 120Hz screens
        QualitySettings.vSyncCount = 0;
        int refreshRate = Screen.currentResolution.refreshRate;
        if (refreshRate >= 90)
        {
            Application.targetFrameRate = refreshRate;
        }
        else if (targetFrameRate > 0)
        {
            Application.targetFrameRate = targetFrameRate;
        }
        else
        {
            Application.targetFrameRate = 60;
        }
    }

    void Start()
    {
        Shader.globalMaximumLOD = shaderLOD; 
        Cursor.visible = false;
    }
}
