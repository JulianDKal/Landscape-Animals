using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public static bool gameIsPaused = false;
    public int turnCount;
    public int pointCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void PlayLevel(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelIndex);
    }

    public void Pause()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
        {
            gameIsPaused = true;
            SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
            Time.timeScale = 0;
        }

    }

    public void Resume()
    {

        if (!SceneManager.GetSceneByName("SettingsScene").isLoaded)
        {
            gameIsPaused = false;
            SceneManager.UnloadSceneAsync("PauseMenu");
            Time.timeScale = 1;
        }
        else SceneManager.UnloadSceneAsync("SettingsScene");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused) Pause();
            else Resume();
        }
    }
}
