using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;


public class GameSettingsController : MonoBehaviour
{
    public GameObject ControllerInput;
    public GameObject SceneLoader;
    public GameObject GameSettingsCanvas;
    public GameObject DirectionTgl30;
    public GameObject DirectionTgl45;
    public GameObject DirectionTgl90;
    public GameObject SlowTgl;
    public GameObject FastTgl;
    public GameObject TimeTgl30;
    public GameObject TimeTgl90;
    public GameObject TimeTgl180;
    public GameObject LeftToggle;
    public GameObject RightToggle;


    //controller for input
    private MLInput.Controller _controller;

    // only call GetComponent once in Start()
    private SceneLoader scene_loader;

    private Toggle direction_toggle_30;
    private Toggle direction_toggle_45;
    private Toggle direction_toggle_90;

    private Toggle slow_toggle;
    private Toggle fast_toggle;

    private Toggle time_toggle_30;
    private Toggle time_toggle_90;
    private Toggle time_toggle_180;

    // toggles for choosing direction side(s)
    private Toggle left_toggle;
    private Toggle right_toggle;

    // Start is called before the first frame update
    void Start()
    {
        _controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonUp += OnButtonUp;
        scene_loader = SceneLoader.GetComponent<SceneLoader>();

        direction_toggle_30 = DirectionTgl30.GetComponent<Toggle>();
        direction_toggle_45 = DirectionTgl45.GetComponent<Toggle>();
        direction_toggle_90 = DirectionTgl90.GetComponent<Toggle>();

        slow_toggle = SlowTgl.GetComponent<Toggle>();
        fast_toggle = FastTgl.GetComponent<Toggle>();

        time_toggle_30 = TimeTgl30.GetComponent<Toggle>();
        time_toggle_90 = TimeTgl90.GetComponent<Toggle>();
        time_toggle_180 = TimeTgl180.GetComponent<Toggle>();

        left_toggle = LeftToggle.GetComponent<Toggle>();
        right_toggle = RightToggle.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.TriggerValue > 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(ControllerInput.transform.position, ControllerInput.transform.forward, out hit))
            {
                if (hit.transform.gameObject.name == "30 Tgl")
                {
                    direction_toggle_30.isOn = !direction_toggle_30.isOn;
                }

                if (hit.transform.gameObject.name == "45 Tgl")
                {
                    direction_toggle_45.isOn = !direction_toggle_45.isOn;
                }

                if (hit.transform.gameObject.name == "90 Tgl")
                {
                    direction_toggle_90.isOn = !direction_toggle_90.isOn;
                }

                if (hit.transform.gameObject.name == "Slow Tgl")
                {
                    slow_toggle.isOn = !slow_toggle.isOn;
                }
                if (hit.transform.gameObject.name == "Fast Tgl")
                {
                    fast_toggle.isOn = !fast_toggle.isOn;
                }

                if (hit.transform.gameObject.name == "30 Sec Tgl")
                {
                    time_toggle_30.isOn = !time_toggle_30.isOn;
                }
                if (hit.transform.gameObject.name == "90 Sec Tgl")
                {
                    time_toggle_90.isOn = !time_toggle_90.isOn;
                }
                if (hit.transform.gameObject.name == "180 Sec Tgl")
                {
                    time_toggle_180.isOn = !time_toggle_180.isOn;
                }

                if (hit.transform.gameObject.name == "Left Side Toggle")
                {
                    left_toggle.isOn = !left_toggle.isOn;
                }

                if (hit.transform.gameObject.name == "Right Side Toggle")
                {
                    right_toggle.isOn = !right_toggle.isOn;
                }

                if (hit.transform.gameObject.name == "Start Game Btn")
                {
                    StartGame();
                }

            }
        }
    }

    void OnButtonUp(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.HomeTap)
        {
            GameSettingsCanvas.SetActive(false);
            scene_loader.LoadScene("MainMenu");
        }
    }

    void StartGame()
    {
        if (direction_toggle_30.isOn)
        {
            GameInfo.gameType = "30";
        }
        else if (direction_toggle_45.isOn)
        {
            GameInfo.gameType = "45";
        }
        else if (direction_toggle_90.isOn)
        {
            GameInfo.gameType = "90";
        }
        else
        {
            GameInfo.gameType = "random";
        }

        if (slow_toggle.isOn)
        {
            GameInfo.speed = "slow";
        }
        else
        {
            GameInfo.speed = "fast";
        }

        if (time_toggle_30.isOn)
        {
            GameInfo.length = 30;
        }
        else if (time_toggle_90.isOn)
        {
            GameInfo.length = 90;
        }
        else
        {
            GameInfo.length = 180;
        }

        // save toggle values
        GameInfo.leftSide = left_toggle.isOn;
        GameInfo.rightSide = right_toggle.isOn;

        GameSettingsCanvas.SetActive(false);
        // load next scene
        scene_loader.LoadScene("Game Directions");
    }

    void OnDestroy()
    {
        MLInput.OnControllerButtonUp -= OnButtonUp;
    }
}
