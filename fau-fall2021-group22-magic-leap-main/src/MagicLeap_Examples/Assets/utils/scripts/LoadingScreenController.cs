using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LoadingScreenController : MonoBehaviour
{   
    public Slider slider;
    AsyncOperation loadingOperation;
    public GameObject loadingScreenCanvas;
    // Start is called before the first frame update
    void Start()
    {   
        loadingScreenCanvas.SetActive(true);
        slider.value = 0;
        loadingOperation = SceneManager.LoadSceneAsync("Pitch");
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    }
}
