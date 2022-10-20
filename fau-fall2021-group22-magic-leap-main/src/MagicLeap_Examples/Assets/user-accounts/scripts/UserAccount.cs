using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database
{
    public class UserAccount
    {
        public string _userName;
        public string _password;

        public UserAccount(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public static UserAccount getFakeAccount()
        {
            return new UserAccount("katie5", "pass345");
        }
    }

   
}