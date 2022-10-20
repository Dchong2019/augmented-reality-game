using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;

public class TestUser : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //testing add users
        DBConnection newUser = new DBConnection();

        newUser.addUserAccount(new UserAccount("jay" , "pass1"));
        newUser.addUserAccount(new UserAccount("kelly", "pass1"));
        newUser.close();
             
        //testing delete user
        DBConnection deleteUserAccount = new DBConnection();

        deleteUserAccount.deleteUser("katie");
        deleteUserAccount.deleteUser("katie1");
        deleteUserAccount.close();

        //reading users 
        DBConnection readuser = new DBConnection();
        print(readuser.getAllUsers());
        /*DBConnection dbconnect2 = new DBConnection();
        System.Data.IDataReader reader1 = dbconnect2.getAllUsers();

        List<UserAccount> myList1 = new List<UserAccount>();
        while (reader1.Read())
        {
            UserAccount entity = new UserAccount(reader1[0].ToString(),
                                    reader1[1].ToString());

            Debug.Log("username1: " + entity._userName);
            Debug.Log("pass1: " + entity._password);
            myList1.Add(entity);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
