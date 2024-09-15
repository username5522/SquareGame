using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [System.Serializable]
    public class SceneReference
    {
        public string sceneName;
        public int sceneIndex;
    }

    [SerializeField] private SceneReference tutorialScene;
    [SerializeField] private SceneReference gameScene;

    public void ExitButton() => Application.Quit();
    public void TutorialButton() => LoadScene(tutorialScene);
    public void StartButton() => LoadScene(gameScene);

    private void LoadScene(SceneReference scene)
    {
        if (!string.IsNullOrEmpty(scene.sceneName))
            SceneManager.LoadScene(scene.sceneName);
        else SceneManager.LoadScene(scene.sceneIndex);
    }
}