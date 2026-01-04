using System;
using System.Collections.Generic;
using System.Text;

namespace QuizSystemApp_LMS.NewFolder.Core.Menus;
public class MainMenu //Luan McShane B00983827
{
    private readonly QuizSystem _system;

    public MainMenu(QuizSystem system)
    {
        _system = system;
    }

    public void Show()
    {
        while (true)
        {
            Console.WriteLine("\n=== QUIZ SYSTEM ===");
            Console.WriteLine("1) Admin");
            Console.WriteLine("2) Student (later)");
            Console.WriteLine("0) Exit");
            Console.Write("Choose: ");

            var choice = Console.ReadLine();

            if (choice == "0") return;

            if (choice == "1")
            {
                var admin = _system.AuthenticateAdmin();
                if (admin == null)
                {
                    Console.WriteLine("Invalid admin credentials.");
                    continue;
                }

                var adminMenu = new AdminMenu(_system, admin);
                adminMenu.Show();
            }
            else
            {
                Console.WriteLine("Not implemented yet.");
            }
        }
    }
}