using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
using TMPro;
using Scoring;

public class GameResultsController : MonoBehaviour
{
    private MLInput.Controller controller;
    public GameObject gameResultsCanvas;
    public GameObject ExitGameCanvas;
    public GameObject sceneLoader;
    public GameObject controllerInput;
    public TextMeshProUGUI username;
    public TextMeshProUGUI catches;
    public TextMeshProUGUI perfectCatches;
    public TextMeshProUGUI score;
    public TextMeshProUGUI gameMessage;
    public TextMeshProUGUI time_text;
    public TextMeshProUGUI dir_text;
    public TextMeshProUGUI speed_text;

    private SceneLoader scene_loader;

    // Start is called before the first frame update
    void Start()
    {
        controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonUp += OnButtonUp;

        scene_loader = sceneLoader.GetComponent<SceneLoader>();
        populateCanvas();

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.TriggerValue > 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(controllerInput.transform.position, controllerInput.transform.forward, out hit))
            {
                if (hit.transform.gameObject.name == "Next Btn")
                {
                    gameResultsCanvas.SetActive(false);
                    ExitGameCanvas.SetActive(true);
                }
                if (hit.transform.gameObject.name == "Menu Btn")
                {
                    ExitGameCanvas.SetActive(false);
                    scene_loader.LoadScene("MainMenu");
                }
                if (hit.transform.gameObject.name == "Exit App Btn")
                {
                    Application.Quit();
                }
                if (hit.transform.gameObject.name == "Play Again Btn")
                {
                    ExitGameCanvas.SetActive(false);
                    scene_loader.LoadScene("Game Settings");
                }
            }

        }

        
    }

    private void populateCanvas()
    {
        if (GameInfo.currentUser != null)
        {
            username.text = GameInfo.currentUser;

        }else
        {
            username.text = "Player 1";
        }
        
        catches.text = GameInfo.lastGame.Caught.ToString();
        perfectCatches.text = GameInfo.lastGame.PerfectCatches.ToString();
        score.text = GameInfo.lastGame.Points.ToString();

        if (GameInfo.lastGame.PerfectGame == true)
        {
            gameMessage.text = "Perfect Game!";
        }
        else
        {
            gameMessage.text = "Good Game!";
        }

        speed_text.text = GameInfo.speed;

        string game_length_string = "";
        game_length_string += (GameInfo.length / 60).ToString() + ":" + (GameInfo.length % 60).ToString();
        time_text.text = game_length_string;

        if (GameInfo.gameType == "random")
        {
            dir_text.text = "random";
        }
        else
        {
            dir_text.text = GameInfo.gameType + " Degrees";
        }

    }
    void OnDestroy()
    {
        MLInput.OnControllerButtonUp -= OnButtonUp;
    }

    void OnButtonUp(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.HomeTap)
        {
            sceneLoader.GetComponent<SceneLoader>().LoadScene("MainMenu");
        }
    }
}
