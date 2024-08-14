using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        // VSync
        bool vSyncOn = PlayerPrefs.GetInt("VSync", 1) == 1;
        QualitySettings.vSyncCount = vSyncOn ? 1 : 0;

        // Max FPS
        float maxFPS = PlayerPrefs.GetFloat("MaxFPS", 60f);
        Application.targetFrameRate = (int)maxFPS;

        // Resolution
        int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        Resolution[] resolutions = Screen.resolutions;
        if (resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        }
    }
}