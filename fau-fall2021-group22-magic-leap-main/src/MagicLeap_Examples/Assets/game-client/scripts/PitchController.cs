using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
using TMPro;
using Scoring;
using Database;
using System;

public class PitchController : MonoBehaviour
{
    private MLInput.Controller _controller;
    public GameObject controllerInput;

    public AudioSource src;
    public GameObject sceneLoader;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timerText;
    public BaseballGame game { get; set; }
    private int _lastScore;
    private int _currentScore;

    public GameObject launchPoint;
    public float timeBetweenPitches;
    public GameObject ball;
    public float launchAngle;
    private GameObject currentBall;
    public GameObject mainCamera;
    private bool firstBallThrown = false;

    // Start is called before the first frame update
    void Start()
    {
        _controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonUp += OnButtonUp;

        if (GameInfo.currentUser != null)
        {
            game = BaseballGame.New(GameInfo.currentUser);
        }
        else
        {
            game = BaseballGame.New("Guest");
        }
        
        _lastScore = 0;
        _currentScore = 0;
        StartCoroutine(reloadTimer(GameInfo.length));
        StartCoroutine(Pitch());
    }

    // Update is called once per frame
    void Update(){}


    private void OnDestroy()
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

    IEnumerator reloadTimer(float reloadTimeInSeconds)
    {
        float counter = 0;

        while (counter < reloadTimeInSeconds)
        {
            counter += Time.deltaTime;
            string newTime = (reloadTimeInSeconds - counter).ToString();
            string shortenedTime = "";
            foreach(char symbol in newTime)
            {
                if (symbol == '.')
                {
                    break;
                }
                shortenedTime += symbol;

            }
            timerText.text = shortenedTime;

            yield return null;
        }

        GameInfo.lastGame = game;
        DBConnection db = new DBConnection();
        ScoresClass dbScore = new ScoresClass(game.User, game.Date, game.Points.ToString(), game.PerfectCatches.ToString(), game.Thrown.ToString(), game.Caught.ToString(), game.PerfectGame);
        db.addScoresToTable(dbScore);
        sceneLoader.GetComponent<SceneLoader>().LoadScene("Game Results");
    }

    private IEnumerator Pitch()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenPitches);
            if (firstBallThrown)
            {
                if ((_currentScore - _lastScore) == 0)
                {
                    StartCoroutine(displayMessageText("Miss!"));
                }
            }
            else
            {
                firstBallThrown = true;
            }


            if (currentBall)
            {
                Destroy(currentBall);
            }

            // get direction to main camera to initially orient the ball
            var directionToMainCamera = mainCamera.transform.position - launchPoint.transform.position;
            var rotation = Quaternion.LookRotation(directionToMainCamera);

            if (ball)
            {
                currentBall = Instantiate(ball, launchPoint.transform.position, rotation);

            }

            updateGameScore();

        }   
    }

    // if catch occurs in Catch.cs script on hand keypoint objects
    public void incrementScoreVisually()
    {
        _currentScore++;
        displayScoreText();

        // display catch quality
        if (_currentScore - _lastScore == 1)
        {
            StartCoroutine(displayMessageText("Catch!"));
        } else if (_currentScore - _lastScore == 2)
        {
            StartCoroutine(displayMessageText("Perfect Catch!"));
        }
    }

    private void displayScoreText()
    {
        scoreText.text = _currentScore.ToString();
    }

    private void updateGameScore()
    {
        // back end
        game.Throw(_currentScore - _lastScore);

        // front end
        _lastScore = _currentScore;
    }

    IEnumerator displayMessageText(string message)
    {
        messageText.text = message;
        yield return new WaitForSeconds(2);
        messageText.text = null;
    }
}
