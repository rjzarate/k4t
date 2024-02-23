using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// used for tracking the progress of the loading bar (future use)
// Implement the set of LoadingScene objects from the LoadingScene scene to any scene that you want to exit from, 
//      and edit a button's "On Click" to load into the desired scene using said set of LoadingScene objects
public class LoadingScene : MonoBehaviour
{
    // instantiation of variables to reference the loading screen
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private Image LoadingBarFilled;

    // starts up the loading screen (sceneId can be found in File -> BuildSettings)
    public void LoadScene(string sceneName)
    {
        // attempt to load the screen parallel to other operations
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // continues up the loading screen
    // the desired scene's id is loaded when the "On Click" option for a button calls the set of LoadingScene objects
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // operation is run
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        LoadingScreen.SetActive(true);

        // updates the fullness of the bar while loading is being done
        while (!operation.isDone)
        {
            // grabs the percent of operation done for loading bar to fill
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progressValue);
            LoadingBarFilled.fillAmount = progressValue;
            
            // yields the operation to continue on a later frame (related to StartCoroutine)
            yield return null; 
        }
    }
}
