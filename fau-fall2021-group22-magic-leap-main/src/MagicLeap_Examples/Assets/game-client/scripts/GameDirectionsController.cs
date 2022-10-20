using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;

public class GameDirectionsController : MonoBehaviour
{
    public GameObject ControllerInput;
    public GameObject SceneLoader;
    public GameObject GameDirectionsCanvas;

    //controller for input
    private MLInput.Controller _controller;

    // only call GetComponent once in Start()
    private SceneLoader scene_loader;
    // Start is called before the first frame update
    void Start()
    {
        _controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonUp += OnButtonUp;

        scene_loader = SceneLoader.GetComponent<SceneLoader>();
    }

    private void OnDestroy()
    {
        MLInput.OnControllerButtonUp -= OnButtonUp;
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.TriggerValue > 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(ControllerInput.transform.position, ControllerInput.transform.forward, out hit))
            {
                if (hit.transform.gameObject.name == "Start Game Btn")
                {
                    GameDirectionsCanvas.SetActive(false);
                    scene_loader.LoadScene("Pitch");
                }
          
            }
        }
    }

    void OnButtonUp(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.HomeTap)
        {
            GameDirectionsCanvas.SetActive(false);
            scene_loader.LoadScene("Game Settings");
        }
    }
}
