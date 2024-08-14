using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle vSyncToggle;
    public Slider maxFPSSlider;
    public TMP_Text maxFPSText;
    public Dropdown resolutionDropdown;
    public FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;

    private void Awake()
    {
        LoadSettings();

        vSyncToggle.onValueChanged.AddListener(ToggleVSync);
        maxFPSSlider.onValueChanged.AddListener(SetMaxFPS);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        SetupResolutionDropdown();
    }

    private void SetupResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        Resolution[] resolutions = Screen.resolutions;
        int maxResolutions = Mathf.Min(resolutions.Length, 20);
        for (int i = 0; i < maxResolutions; i++)
        {
            Resolution resolution = resolutions[i];
            Dropdown.OptionData option = new Dropdown.OptionData(
                string.Format("{0}x{1}", resolution.width, resolution.height)
            );
            resolutionDropdown.options.Add(option);
        }

        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void LoadSettings()
    {
        bool vSyncOn = PlayerPrefs.GetInt("VSync", 1) == 1;
        vSyncToggle.isOn = vSyncOn;
        QualitySettings.vSyncCount = vSyncOn ? 1 : 0;

        float maxFPS = PlayerPrefs.GetFloat("MaxFPS", 60f);
        maxFPSSlider.value = maxFPS;
        Application.targetFrameRate = (int)maxFPS;
        maxFPSText.text = "FPS Limiter - " + maxFPS.ToString();

        int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        SetResolution(resolutionIndex);
    }

    private void ToggleVSync(bool isOn)
    {
        QualitySettings.vSyncCount = isOn ? 1 : 0;
        PlayerPrefs.SetInt("VSync", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void SetMaxFPS(float value)
    {
        Application.targetFrameRate = (int)value;
        maxFPSText.text = "FPS Limiter - " + value.ToString();
        PlayerPrefs.SetFloat("MaxFPS", value);
        PlayerPrefs.Save();
    }

    private void SetResolution(int index)
    {
        Resolution[] resolutions = Screen.resolutions;
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, fullScreenMode);
        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.Save();
    }
}