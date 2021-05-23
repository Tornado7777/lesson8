using System;


namespace User_App
{
    public class UserLibrary
    {
        public string User_name { get; set; }
        public int User_age { get; set; }
        public string User_career { get; set; }
        public void User_App(string user_name, int user_age, string user_career)
        {
            User_name = user_name;
            User_age = user_age;
            User_career = user_career;
        }


    }
}
