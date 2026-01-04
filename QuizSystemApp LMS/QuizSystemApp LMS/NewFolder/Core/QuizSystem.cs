using System;
using System.Collections.Generic;
using System.Text;

namespace QuizSystemApp_LMS.NewFolder.Core;
public class QuizSystem //Luan McShane B00983827
{
    public List<Admin> AdminUsers { get; private set; } = new();
    public List<Category> Categories { get; private set; } = new();

    public void LoadSampleData()
    {
        AdminUsers = Admin.CreateSampleAdmins();

        Categories = new List<Category>
        {
            new Category(1, "OOP Fundamentals", "Classes, objects, inheritance, polymorphism"),
            new Category(2, "Data Structures", "Arrays, lists, stacks, queues, trees"),
        };

        // sample questions under OOP Fundamentals
        var oop = Categories.First(c => c.CategoryID == 1);
        oop.Questions.Add(new Question(
            1,
            "What does OOP stand for?",
            new List<string> { "Object-Oriented Programming", "Open Online Protocol", "Operating Output Process", "Object-Only Paradigm" },
            "Object-Oriented Programming",
            "Easy"
        ));
    }

    public Admin? AuthenticateAdmin()
    {
        Console.Write("Admin username: ");
        var username = Console.ReadLine() ?? "";

        Console.Write("Admin password: ");
        var password = Console.ReadLine() ?? "";

        var admin = AdminUsers.FirstOrDefault(a => a.Username == username && a.Password == password);
        if (admin != null) admin.LoginDate = DateTime.Now;
        return admin;
    }
}
