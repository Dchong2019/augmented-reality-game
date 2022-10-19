using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
using System;

public class TestScores : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {   //testing add users
        DBConnection newScores = new DBConnection();

        newScores.addScoresToTable(new ScoresClass("jay", "2/15/22", "90", "50", "50", "95", "0"));
        newScores.addScoresToTable(new ScoresClass("molly", "2/15/22", "90", "50", "50", "95", "0"));
        newScores.close();

        //reading all users
        DBConnection score = new DBConnection();
        print(score.getAllScores());

        /*DBConnection dbconnect = new DBConnection();
        System.Data.IDataReader reader = dbconnect.getAllScores();

        List<ScoresClass> myListScores = new List<ScoresClass>();
        while (reader.Read())
        {
            ScoresClass entity = new ScoresClass(reader[0].ToString(),
                                    reader[1].ToString(),
                                    reader[2].ToString(),
                                    reader[3].ToString(),
                                    reader[4].ToString(),
                                    reader[5].ToString(),
                                    reader[6].ToString());
            
            Debug.Log("username: " + entity._userName);
            Debug.Log("date: " + entity._date);
            Debug.Log("points: " + entity._points);
            Debug.Log("perfectCatches: " + entity._perfectCatches);
            Debug.Log("thrown: " + entity._thrown);
            Debug.Log("caught: " + entity._caught);
            Debug.Log("perfectGame: " + entity._perfectGame);
            myListScores.Add(entity);
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
