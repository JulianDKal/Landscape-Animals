using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Material mat;
    public float transitionSpeed = -0.5f;
    private float threshold = 1.1f;
    public bool shouldFadeIn = true;

    private void Awake()
    {
        mat = GetComponent<Image>().material;
        FadeIn();
    }

    public void StartLevel(int levelIndex)
    {
        FadeOut();
        Game_Manager.instance.PlayLevel(levelIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToPrevScene()
    {
        SceneManager.UnloadSceneAsync("SettingsScene");
    }

    public void Settings()
    {
        SceneManager.LoadSceneAsync("SettingsScene", LoadSceneMode.Additive);
    }

    public void LevelSelectScreen()
    {
        FadeOut();
        Game_Manager.instance.StartCoroutine("LevelSelectScreen");
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        FadeOut();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        Game_Manager.instance.Resume();
    }

    public void FadeOut()
    {
        transitionSpeed *= -1;
        threshold = 1.1f;
        shouldFadeIn = true;
    }

    public void FadeIn()
    {
        transitionSpeed *= -1;
        threshold = -0.1f;
    }

    private void Update()
    {
        threshold += Time.deltaTime * transitionSpeed;
        if (!shouldFadeIn) threshold = 1.1f;
        mat.SetFloat("Threshold", threshold);
    }
}