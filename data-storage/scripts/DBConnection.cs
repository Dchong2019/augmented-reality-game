using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class DBConnection : SqliteHelper
    {
        private const String Tag = "Riz: LocationDb:\t";

        //user account variables
        private const String userAccountTable = "userAccountTable";
        private const String KEY_USERNAME = "userName";
        private const String KEY_PASSWORD = "password";

        //Scores variables
        private const String scoresTable = "scoresTable";
        private const String KEY_DATE = "date";
        private const String KEY_POINTS = "points";
        private const String KEY_PERFECTCATCHES = "perfectCatches";
        private const String KEY_THROWN = "thrown";
        private const String KEY_CAUGHT = "caught";
        private const String KEY_PERFECTGAME = "perfectGame";

        //private const String[] userAccountColumns = new String[](KEY_USERNAME, KEY_PASSWORD);

        public DBConnection() : base()
        {
            //create userAccountTable
            IDbCommand dbcmd = getDbCommand();
            string createUserAccountTable = "CREATE TABLE IF NOT EXISTS userAccountTable(userName TEXT PRIMARY KEY, password TEXT)";
            dbcmd.CommandText = createUserAccountTable;
            dbcmd.ExecuteNonQuery();

            //create userAccountTable
            IDbCommand dbcmd2 = getDbCommand();
            string createScoresTable = "CREATE TABLE IF NOT EXISTS scoresTable(userName TEXT, " +
                "                                                              date TEXT, " +
                "                                                              points INTEGER, " +
                "                                                              perfectCatches INTEGER, " +
                "                                                              thrown INTEGER, " +
                "                                                              caught INTEGER, " +
                "                                                              perfectGame INTEGER, " +
                "                                                              FOREIGN KEY (userName) REFERENCES userAccountTable(userName)" +
                "                                                              ON DELETE CASCADE)";
            dbcmd2.CommandText = createScoresTable;
            dbcmd2.ExecuteNonQuery();

        }

        //function to add a new user into database
        public void addUserAccount(UserAccount user)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                        "INSERT INTO " + userAccountTable
                        + " ( "
                        + KEY_USERNAME + ", "
                        + KEY_PASSWORD + " ) "
                        + "VALUES ( '"
                        + user._userName + "', '"
                        + user._password + "' )";
            dbcmd.ExecuteNonQuery();

        }

        //function to delete a user based on username from database
        public override void deleteUser(string userName)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + userAccountTable + " WHERE " + KEY_USERNAME + " = '" + userName + "'";
            dbcmd.ExecuteNonQuery();
        }

        //function used to get username of player
        public override IDataReader getUserName(string userName)
        {
            return base.getUserName(userName);
        }

        //function user to get password of player
        public override IDataReader getPassword(string password)
        {
            return base.getPassword(password);
        }

        public List<UserAccount> getAllUsers()
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + userAccountTable;
            IDataReader reader = dbcmd.ExecuteReader();

            List<UserAccount> list_of_users = new List<UserAccount>();

            while (reader.Read())
            {
                UserAccount entity = new UserAccount(reader[0].ToString(),
                                        reader[1].ToString());

                //Debug.Log("username1: " + entity._userName);
                //Debug.Log("pass1: " + entity._password);
                list_of_users.Add(entity);
            }
            return list_of_users;
        }

        public List<ScoresClass> getAllScores()
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + scoresTable;
            IDataReader reader = dbcmd.ExecuteReader();

            List<ScoresClass> list_of_scores = new List<ScoresClass>();
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
                list_of_scores.Add(entity);
            }
            return list_of_scores;
        }

        public void addScoresToTable(ScoresClass scores)
        {
            IDbCommand dbcmd1 = getDbCommand();
            dbcmd1.CommandText =
                        "INSERT INTO " + scoresTable
                        + " ( "
                        + KEY_USERNAME + ", "
                        + KEY_DATE + ", "
                        + KEY_POINTS + ", "
                        + KEY_PERFECTCATCHES + ", "
                        + KEY_THROWN + ", "
                        + KEY_CAUGHT + ", "
                        + KEY_PERFECTGAME + " ) "
                        + "VALUES ( '"
                        + scores._userName + "', '"
                        + scores._date + "', '"
                        + scores._points + "', '"
                        + scores._perfectCatches + "', '"
                        + scores._thrown + "', '"
                        + scores._caught + "', '"
                        + scores._perfectGame + "' )";
            dbcmd1.ExecuteNonQuery();

        }
    }
}
