using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
using TMPro;

public class StartMenuController : MonoBehaviour
{   
    
    private MLInput.Controller controller;
    public GameObject StartMenuCanvas;
    public GameObject controllerInput;
    public GameObject sceneLoader;
    // Start is called before the first frame update
    void Start()
    {   
        controller = MLInput.GetController(MLInput.Hand.Left);
    }

    // Update is called once per frame
    void Update()
    {   
        if(controller.TriggerValue > 0.5f)
        {
            RaycastHit hit;
            if(Physics.Raycast(controllerInput.transform.position, controllerInput.transform.forward, out hit))
            {
                if(hit.transform.gameObject.name == "EnterButton")
                {
                    EnterApplication();
                }
            }

        }
        
        
    }

    void EnterApplication()
    {
        sceneLoader.GetComponent<SceneLoader>().LoadScene("MainMenu");
        
    }

    private void OnDestroy()
    {
        
    }
}
