using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    private static bool isPaused = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadSettings();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        UpdateCursorVisibility();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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

    private void UpdateCursorVisibility()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return;
        }

        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateCursorVisibility();
    }

    public static void SetPauseState(bool paused)
    {
        isPaused = paused;
    }
}