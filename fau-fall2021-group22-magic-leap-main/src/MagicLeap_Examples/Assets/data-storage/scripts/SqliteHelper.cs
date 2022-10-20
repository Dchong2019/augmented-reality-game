using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

namespace Database
{
    public class SqliteHelper
    {
        //variables
        private const string database = "MagicLeapBaseballDB";
        private const String Tag = "Riz: LocationDb:\t";
        private string db_string;
        public IDbConnection db_connection;

        //st up connection for database
        public SqliteHelper()
        {

            db_string = "URI=file:" + Application.persistentDataPath + "/" + "MagicLeapBaseBallDB";
            Debug.Log("db_string" + db_string);
            db_connection = new SqliteConnection(db_string);
            db_connection.Open();
        }

        //closes connection
        ~SqliteHelper()
        {
            db_connection.Close();
        }

        //virtual functions for userAccountTable 
        public virtual IDataReader getUserName(string userName)
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        public virtual IDataReader getPassword(string password)
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        public virtual void deleteUser(string userName)
        {
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }

        //helper functions
        public IDbCommand getDbCommand()
        {
            return db_connection.CreateCommand();
        }

        

        public void close()
        {
            db_connection.Close();
        }
        
    }
}

