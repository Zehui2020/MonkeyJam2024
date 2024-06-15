using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public static SceneManagment Instance;
    [SerializeField] private Animator fadeTransition;

    void Awake()
    {
        Instance = this;
    }

    public void FadeIn()
    {
        fadeTransition.SetTrigger("fadein");
    }

    public void FadeOut()
    {
        fadeTransition.SetTrigger("fadeout");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadNewScene(sceneName));
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(LoadMainMenuInstantly());
    }

    private IEnumerator LoadNewScene(string sceneName)
    {
        if (sceneName == "MainMenu")
            yield return new WaitForSeconds(2f);

        FadeOut();

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadSceneAsync(sceneName);
    }

    private IEnumerator LoadMainMenuInstantly()
    {
        FadeOut();

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadSceneAsync("MainMenu");
    }
}