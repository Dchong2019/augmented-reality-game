using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
using TMPro;
using Database;
using System;

namespace GriffinWork
{
    public class UserInfo
    {
        public string _user { get; set; }
        public string _password { get; set; }
    }


    public class MainMenuController : MonoBehaviour
    {

        private MLInput.Controller controller;
        public GameObject headlockedCanvas;
        public GameObject controllerInput;
        public GameObject sceneLoader;
        public GameObject LoginCanvas;
        public GameObject RegisterCanvas;
        public GameObject AccountSettings;
        public GameObject LogoutCanvas;
        public GameObject ChangePassword;
        public GameObject RegisterInputCanvas;
        public GameObject RegisterPassinputCanvas;
        public GameObject LoginUsernameInputCanvas;
        public GameObject LoginPassInputCanvas;


        public GameObject ButtonTest;



        public GameObject RegisterinputFieldUsername;
        public GameObject RegisterinputFieldPassword;
        public GameObject LogininputFieldUsername;
        public GameObject LogininputFieldPassword;


        public TextMeshProUGUI LoginButtonText;

       
        // Start is called before the first frame update
        void Start()
        {
            //DBConnection user = new DBConnection();
            controller = MLInput.GetController(MLInput.Hand.Left);
            MLInput.OnControllerButtonUp += OnButtonUp;
            if(GameInfo.currentUser != null)
            {
                LoginButtonText.text = "Logout";
            }
            else { LoginButtonText.text = "Login"; }
        }

        // Update is called once per frame
        void Update()
        {
            if (controller.TriggerValue > 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(controllerInput.transform.position, controllerInput.transform.forward, out hit))
                {
                    if (hit.transform.gameObject.name == "LoginButton")
                    {
                        if(LoginButtonText.text == "Login")
                        { 
                            Login();
                        }
                        else
                        {
                            Logout(); //make sure we set current user name to null
                        }
                        

                    }
                    if (hit.transform.gameObject.name == "LogButton")
                    {
                        Log();
                    }
                    if (hit.transform.gameObject.name == "RegisterButton")
                    {
                        Register();
                    }
                    if (hit.transform.gameObject.name == "CreateButton")
                    {
                        CreateAccount();
                        //Login(); need to set previous canvas to false
                    }
                    if (hit.transform.gameObject.name == "BackButton")
                    {
                        Back();
                    }
                    if (hit.transform.gameObject.name == "AccountSettings")
                    {
                        AcctSettings();
                    }
                    if (hit.transform.gameObject.name == "DeleteAccount")
                    {
                        DeleteAccountScene();
                    }

                    if (hit.transform.gameObject.name == "ConfirmButton")
                    {
                        DeleteAccount();
                    }
                    if (hit.transform.gameObject.name == "ConfirmLogoutButton")
                    {
                        LogoutButton();
                    }


                }

            }


        }

   

        void OnButtonUp(byte controllerId, MLInput.Controller.Button button)
        {
            if (button == MLInput.Controller.Button.HomeTap)
            {
                sceneLoader.GetComponent<SceneLoader>().LoadScene("MainMenu");
            }
        }


        void LogoutButton()
        {
            if (GameInfo.currentUser != null)
            {
                GameInfo.currentUser = null;
            }
            LogoutCanvas.SetActive(false);
            headlockedCanvas.SetActive(true);
            LoginButtonText.text = "Login";
        }

        void Logout()
        {
            LogoutCanvas.SetActive(true);
        }
        //brings you to the Login screen
        void Login()
        {
            headlockedCanvas.SetActive(false);
            LoginCanvas.SetActive(true);
            LoginUsernameInputCanvas.SetActive(true);
            LoginPassInputCanvas.SetActive(true);

    }
        // will log the user into the account 
        void Log()
        {
            InputField UsernameLog = LogininputFieldUsername.GetComponent<InputField>();
            InputField PasswordLog = LogininputFieldPassword.GetComponent<InputField>();

            DBConnection GriffinUser = new DBConnection();


            List<UserAccount> UsernameList = GriffinUser.getAllUsers();
            
            foreach(UserAccount user in UsernameList)
            {             
                if(UsernameLog.text == user._userName)
                {
                    if(PasswordLog.text == user._password)
                    {
                        GameInfo.currentUser = user._userName;
                        break;
                    }
                }           
            }
            if (GameInfo.currentUser == null)
            {
                LogininputFieldUsername.GetComponent<InputField>().text = "User does not exist";
            }
            else
            {
                LoginCanvas.SetActive(false);
                LoginUsernameInputCanvas.SetActive(false);
                LoginPassInputCanvas.SetActive(false);
                sceneLoader.GetComponent<SceneLoader>().LoadScene("MainMenu");
            }
        }
        //brings user to a registration page
        void Register()
        {
            headlockedCanvas.SetActive(false);
            RegisterCanvas.SetActive(true);
            RegisterInputCanvas.SetActive(true);
            RegisterPassinputCanvas.SetActive(true);

        }
        public void CreateAccount()
        {
            InputField Username = RegisterinputFieldUsername.GetComponent<InputField>();
            InputField Password = RegisterinputFieldPassword.GetComponent<InputField>();
            //max username will be 10. only letters/numbers. Has to be Unique. if user exists. promt screen with username is already taken
            //password 10 . there are no spaces in either. Characters/symbols allowed

            if (Username.text.Length < 11 && Password.text.Length < 11 && !Username.text.Contains(" ") && !Password.text.Contains(" "))
            {
                DBConnection GriffinUser = new DBConnection();
                List<UserAccount> UsernameList = GriffinUser.getAllUsers();

                bool isTaken = false;
                foreach (UserAccount user in UsernameList)
                {
                    if (Username.text == user._userName)
                    {
                        RegisterinputFieldUsername.GetComponent<InputField>().text = "Username is taken";
                        isTaken = true;
                        break;
                    }
                }
                
                if (!isTaken)
                {
                    GriffinUser.addUserAccount(new UserAccount(Username.text, Password.text));
                    GameInfo.currentUser = Username.text;
                    RegisterCanvas.SetActive(false);
                    RegisterInputCanvas.SetActive(false);
                    RegisterPassinputCanvas.SetActive(false);
                    sceneLoader.GetComponent<SceneLoader>().LoadScene("MainMenu");
                }
            }
            else
            {
                RegisterinputFieldUsername.GetComponent<InputField>().text = "Username and password";
                RegisterinputFieldPassword.GetComponent<InputField>().text = "Must be <10 chars. No Spaces";             
            }
       
            
            ButtonTest.GetComponent<BoxCollider>().enabled = false;

        }
        void Back()
        {
            RegisterCanvas.SetActive(false);
            LoginCanvas.SetActive(false);
            ChangePassword.SetActive(false);
            AccountSettings.SetActive(false);
            RegisterInputCanvas.SetActive(false);
            RegisterPassinputCanvas.SetActive(false);
            LoginUsernameInputCanvas.SetActive(false);
            LoginPassInputCanvas.SetActive(false);
            headlockedCanvas.SetActive(true);

       
        }
        void AcctSettings()
        {
            headlockedCanvas.SetActive(false);
            AccountSettings.SetActive(true);
            RegisterInputCanvas.SetActive(false);
            RegisterPassinputCanvas.SetActive(false);
            LoginUsernameInputCanvas.SetActive(false);
            LoginPassInputCanvas.SetActive(false);
        }
        void DeleteAccountScene()
        {
            RegisterPassinputCanvas.SetActive(false);
            RegisterInputCanvas.SetActive(false);
            LoginUsernameInputCanvas.SetActive(false);
            LoginPassInputCanvas.SetActive(false);
            headlockedCanvas.SetActive(false);
            AccountSettings.SetActive(false);
            ChangePassword.SetActive(true);

        }
        void DeleteAccount()
        {
            DBConnection GriffinUser = new DBConnection();
            GriffinUser.deleteUser(GameInfo.currentUser); 
        }

        private void OnDestroy()
        {
            MLInput.OnControllerButtonUp -= OnButtonUp;
        }
    }
}