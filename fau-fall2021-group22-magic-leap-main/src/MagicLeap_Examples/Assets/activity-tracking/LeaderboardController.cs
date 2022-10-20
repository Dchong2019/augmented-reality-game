using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
using TMPro;
using Scoring;
using System.Data.Common;
using Database;
using UnityEngine.UIElements;
using System;

public class LeaderboardController : MonoBehaviour
{
    private MLInput.Controller controller;

    public GameObject LeaderboardCanvas;
    public GameObject SceneLoader;
    public GameObject controllerInput;
    public GameObject NextButton;
    public GameObject TopButton;
    public TextMeshProUGUI scoreLabel1;
    public TextMeshProUGUI scoreLabel2;
    public TextMeshProUGUI scoreLabel3;
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    public TextMeshProUGUI score3;

    private SceneLoader sceneLoader;
    private List<ScoresClass> gameScores;
    private int maxIndex;
    private int nextIndex;

    // Start is called before the first frame update
    void Start()
    {
        controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonUp += OnButtonUp;

        sceneLoader = SceneLoader.GetComponent<SceneLoader>();

        DBConnection db = new DBConnection();
        gameScores = db.getAllScores();

        maxIndex = gameScores.Count - 1;
        nextIndex = maxIndex;
        StartCoroutine(NextPage());
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
                    StartCoroutine(NextPage());
                    
                }
                if (hit.transform.gameObject.name == "Top Btn")
                {
                    StartCoroutine(Top());
                }
                if (hit.transform.gameObject.name == "Main Menu Btn")
                {
                    LeaderboardCanvas.SetActive(false);
                    sceneLoader.LoadScene("MainMenu");
                }
            }
        }
    }

    IEnumerator NextPage()
    {
        NextButton.SetActive(false);
        int listCounter = 1;
        string label = "";
        string score = "";
        int startingIndex = nextIndex;

        scoreLabel1.text = label;
        score1.text = score;
        scoreLabel2.text = label;
        score2.text = score;
        scoreLabel3.text = label;
        score3.text = score;

        for (int i = startingIndex; i > startingIndex - 3; i--)
        {
            if (i >= 0)
            {
                label = gameScores[i]._userName + " " + gameScores[i]._date;
                score = "throws: " + gameScores[i]._thrown + " catches: " + gameScores[i]._caught + " (" + gameScores[i]._perfectCatches + " perfect)\nscore: " + gameScores[i]._points;
            }else
            {
                nextIndex = maxIndex;
                break;
            }

            if (listCounter == 1)
            {
                scoreLabel1.text = label;
                score1.text = score;
            }
            else if (listCounter == 2)
            {
                scoreLabel2.text = label;
                score2.text = score;
            }
            else
            {
                scoreLabel3.text = label;
                score3.text = score;
            }
            if (i != 0) { nextIndex--; }
            else
            {
                nextIndex = maxIndex;
                break;
            }
            listCounter++;
        }

        yield return new WaitForSeconds(1.5f);
        NextButton.SetActive(true);
    }
    IEnumerator Top()
    {
        TopButton.SetActive(false);
        int listCounter = 1;
        string label = "";
        string score = "";
        nextIndex = maxIndex;
        int startingIndex = nextIndex;

        scoreLabel1.text = label;
        score1.text = score;
        scoreLabel2.text = label;
        score2.text = score;
        scoreLabel3.text = label;
        score3.text = score;

        for (int i = startingIndex; i > startingIndex - 3; i--)
        {
            if (i >= 0)
            {
                label = gameScores[i]._userName + " " + gameScores[i]._date;
                score = "throws: " + gameScores[i]._thrown + " catches: " + gameScores[i]._caught + " (" + gameScores[i]._perfectCatches + " perfect)\nscore: " + gameScores[i]._points;
            }
            else
            {
                nextIndex = maxIndex;
                break;
            }

            if (listCounter == 1)
            {
                scoreLabel1.text = label;
                score1.text = score;
            }
            else if (listCounter == 2)
            {
                scoreLabel2.text = label;
                score2.text = score;
            }
            else
            {
                scoreLabel3.text = label;
                score3.text = score;
            }
            if (i != 0) { nextIndex--; }
            else
            {
                nextIndex = maxIndex;
                break;
            }
            listCounter++;
        }

        yield return new WaitForSeconds(1.5f);
        TopButton.SetActive(true);
    }

    void OnDestroy()
    {
        MLInput.OnControllerButtonUp -= OnButtonUp;
    }

    void OnButtonUp(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.HomeTap)
        {
            sceneLoader.LoadScene("MainMenu");
        }
    }
}
