using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                pauseMenu.SetActive(true);
                SettingsManager.SetPauseState(true);
                return;
            }

            isPaused = false;
            SettingsManager.SetPauseState(false);
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
