using System;
using System.Collections.Generic;
using System.Text;
using QuizSystemApp_LMS.NewFolder;

namespace QuizSystemApp_LMS.NewFolder;

public class Admin : User //Luan McShane B00983827
{
    public DateTime LoginDate { get; set; }

    public Admin(int userID, string username, string password, string email)
        : base(userID, username, password, email, role: "Admin")
    {
    }

    // UML has these; you can implement later if not required in Task 2
    public static List<Admin> CreateSampleAdmins()
    {
        return new List<Admin>
        {
            new Admin(1, "admin", "admin123", "admin@uni.ac.uk"),
        };
    }
}