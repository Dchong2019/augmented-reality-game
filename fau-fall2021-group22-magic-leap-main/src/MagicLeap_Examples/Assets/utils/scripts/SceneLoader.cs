using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{   
    public GameObject loadingScreenCanvas;
    public Slider slider;

    public void LoadScene (string sceneName)
    {   

        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously (string sceneName) 
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreenCanvas.SetActive(true);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
    }
}
