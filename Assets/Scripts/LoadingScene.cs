using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// used for tracking the progress of the loading bar (future use)
public class LoadingScene : MonoBehaviour
{
    // instantiation of varibales to enable the loading screen
    public GameObject LoadingScreen;
    public Image LoadingBarFilled;

    // starts up the loading screen
    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    // continues up the loading screen
    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        LoadingScreen.SetActive(true);

        // updates the fullness of the bar
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFilled.fillAmount = progressValue;

            yield return null;
        }
    }
}
