using System;
using System.Collections.Generic;
using System.Text;

namespace QuizSystemApp_LMS.NewFolder;

public class User //Luan McShane B00983827
{
    public int UserID { get; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public User(int userID, string username, string password, string email, string role)
    {
        UserID = userID;
        Username = username;
        Password = password;
        Email = email;
        Role = role;
    }

    public virtual void UpdateProfile()
    {
        // Optional for later tasks
    }

    public virtual bool Login(string username, string password)
        => Username == username && Password == password;

    public virtual void Logout()
    {
        // Optional for later tasks
    }
}
