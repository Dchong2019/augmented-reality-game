using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class PlayMenuController : MonoBehaviour
{   
    public GameObject PlayMenuCanvas;
    public GameObject ControllerInput;
    public GameObject SceneLoader;

    private MLInput.Controller _controller;
    private SceneLoader scene_loader;

    // Start is called before the first frame update
    void Start()
    {
        _controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonUp += OnButtonUp;

        scene_loader = SceneLoader.GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(_controller.TriggerValue > 0.5f)
        {
            RaycastHit hit;
            if(Physics.Raycast(ControllerInput.transform.position, ControllerInput.transform.forward, out hit))
            {
                if(hit.transform.gameObject.name == "PlayButton")
                {
                    PlayGame();
                }
                if(hit.transform.gameObject.name == "AccountButton")
                {
                    AccountScene();
                }
                if (hit.transform.gameObject.name == "Leaderboard Btn")
                {
                    LeaderboardScene();
                }

            }
        }
    }

    void OnButtonUp(byte controllerId, MLInput.Controller.Button button) {
        if (button == MLInput.Controller.Button.HomeTap) {
            PlayMenuCanvas.SetActive(false);
            scene_loader.LoadScene("Startup");
        }
    }

    void PlayGame()
    {
        PlayMenuCanvas.SetActive(false);
        scene_loader.LoadScene("Game Settings");
    }

    void AccountScene()
    {
        PlayMenuCanvas.SetActive(false);
        scene_loader.LoadScene("UserAccts");
    }


    private void OnDestroy()
    {
        MLInput.OnControllerButtonUp -= OnButtonUp;
    }

    private void LeaderboardScene()
    {
        PlayMenuCanvas.SetActive(false);
        scene_loader.LoadScene("Leaderboard");
    }
}
