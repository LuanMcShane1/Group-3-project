using System;
using System.Collections.Generic;
using System.Text;

namespace QuizSystemApp_LMS.NewFolder.Core.Menus;
public class AdminMenu //Luan McShane B00983827
{
    private readonly QuizSystem _system;
    private readonly Admin _admin;

    public AdminMenu(QuizSystem system, Admin admin)
    {
        _system = system;
        _admin = admin;
    }

    public void Show()
    {
        while (true)
        {
            Console.WriteLine($"\n=== ADMIN MENU (Logged in: {_admin.Username}) ===");
            Console.WriteLine("1) Manage Categories");
            Console.WriteLine("2) Manage Questions");
            Console.WriteLine("0) Logout");
            Console.Write("Choose: ");

            var choice = Console.ReadLine();
            if (choice == "0") return;

            if (choice == "1") ManageCategories();
            else if (choice == "2") ManageQuestions();
            else Console.WriteLine("Invalid choice.");
        }
    }

    private void ManageCategories()
    {
        while (true)
        {
            Console.WriteLine("\n--- CATEGORY MANAGEMENT ---");
            Console.WriteLine("1) View Categories");
            Console.WriteLine("2) Add Category");
            Console.WriteLine("3) Update Category");
            Console.WriteLine("4) Remove Category");
            Console.WriteLine("0) Back");
            Console.Write("Choose: ");

            var choice = Console.ReadLine();
            if (choice == "0") return;

            switch (choice)
            {
                case "1": ViewCategories(); break;
                case "2": AddCategory(); break;
                case "3": UpdateCategory(); break;
                case "4": RemoveCategory(); break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    private void ViewCategories()
    {
        Console.WriteLine("\nID | Name | Description | #Questions");
        foreach (var c in _system.Categories)
            Console.WriteLine($"{c.CategoryID} | {c.CategoryName} | {c.CategoryDescription} | {c.Questions.Count}");
    }

    private void AddCategory()
    {
        Console.Write("Category name: ");
        var name = Console.ReadLine() ?? "";

        Console.Write("Category description: ");
        var desc = Console.ReadLine() ?? "";

        var newId = _system.Categories.Count == 0 ? 1 : _system.Categories.Max(c => c.CategoryID) + 1;
        _system.Categories.Add(new Category(newId, name, desc));
        Console.WriteLine("Category added.");
    }

    private void UpdateCategory()
    {
        try
        {
            Console.Write("Enter CategoryID to update: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            var cat = _system.Categories.FirstOrDefault(c => c.CategoryID == id);
            if (cat == null) { Console.WriteLine("Category not found."); return; }

            Console.Write($"New name (Enter to keep '{cat.CategoryName}'): ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) cat.CategoryName = name;

            Console.Write($"New description (Enter to keep current): ");
            var desc = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(desc)) cat.CategoryDescription = desc;

            Console.WriteLine("Category updated.");
        }
        catch
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    private void RemoveCategory()
    {
        try
        {
            Console.Write("Enter CategoryID to remove: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            var cat = _system.Categories.FirstOrDefault(c => c.CategoryID == id);
            if (cat == null) { Console.WriteLine("Category not found."); return; }

            _system.Categories.Remove(cat);
            Console.WriteLine("Category removed.");
        }
        catch
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    private void ManageQuestions()
    {
        while (true)
        {
            Console.WriteLine("\n--- QUESTION MANAGEMENT ---");
            Console.WriteLine("1) View All Questions");
            Console.WriteLine("2) Add Question (to Category)");
            Console.WriteLine("3) Update Question");
            Console.WriteLine("4) Remove Question");
            Console.WriteLine("0) Back");
            Console.Write("Choose: ");

            var choice = Console.ReadLine();
            if (choice == "0") return;

            switch (choice)
            {
                case "1": ViewAllQuestions(); break;
                case "2": AddQuestion(); break;
                case "3": UpdateQuestion(); break;
                case "4": RemoveQuestion(); break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    private void ViewAllQuestions()
    {
        foreach (var cat in _system.Categories)
        {
            Console.WriteLine($"\nCategory: {cat.CategoryName} (ID {cat.CategoryID})");
            foreach (var q in cat.Questions)
            {
                Console.WriteLine($"  QID {q.QuestionID}: {q.QuestionText} [{q.QuestionDifficultyLevel}]");
                for (int i = 0; i < q.QuestionOptions.Count; i++)
                    Console.WriteLine($"    {i + 1}) {q.QuestionOptions[i]}");
                Console.WriteLine($"    Correct: {q.QuestionCorrectAnswer}");
            }
        }
    }

    private void AddQuestion()
    {
        try
        {
            ViewCategories();
            Console.Write("Enter CategoryID to add question to: ");
            var catId = int.Parse(Console.ReadLine() ?? "0");

            var cat = _system.Categories.FirstOrDefault(c => c.CategoryID == catId);
            if (cat == null) { Console.WriteLine("Category not found."); return; }

            Console.Write("Question text: ");
            var text = Console.ReadLine() ?? "";

            var options = new List<string>();
            for (int i = 1; i <= 4; i++)
            {
                Console.Write($"Option {i}: ");
                options.Add(Console.ReadLine() ?? "");
            }

            Console.Write("Correct answer (type exactly one option OR type 1-4): ");
            var correctInput = Console.ReadLine() ?? "";

            string correctAnswer = correctInput;
            if (int.TryParse(correctInput, out int idx) && idx >= 1 && idx <= 4)
                correctAnswer = options[idx - 1];

            Console.Write("Difficulty (Easy/Medium/Hard): ");
            var diff = Console.ReadLine() ?? "Easy";

            int newQId = GetNextQuestionId();
            cat.Questions.Add(new Question(newQId, text, options, correctAnswer, diff));
            Console.WriteLine("Question added.");
        }
        catch
        {
            Console.WriteLine("Invalid input. Please try again.");
        }
    }

    private int GetNextQuestionId()
    {
        var all = _system.Categories.SelectMany(c => c.Questions).ToList();
        return all.Count == 0 ? 1 : all.Max(q => q.QuestionID) + 1;
    }

    private Question? FindQuestionById(int qId, out Category? ownerCategory)
    {
        ownerCategory = null;
        foreach (var cat in _system.Categories)
        {
            var q = cat.Questions.FirstOrDefault(x => x.QuestionID == qId);
            if (q != null) { ownerCategory = cat; return q; }
        }
        return null;
    }

    private void UpdateQuestion()
    {
        try
        {
            Console.Write("Enter QuestionID to update: ");
            var qId = int.Parse(Console.ReadLine() ?? "0");

            var q = FindQuestionById(qId, out var cat);
            if (q == null || cat == null) { Console.WriteLine("Question not found."); return; }

            Console.Write($"New text (Enter to keep current): ");
            var text = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(text)) q.QuestionText = text;

            Console.Write("Update options? (y/n): ");
            var updateOpts = Console.ReadLine();
            if (updateOpts?.ToLower() == "y")
            {
                var options = new List<string>();
                for (int i = 1; i <= 4; i++)
                {
                    Console.Write($"Option {i}: ");
                    options.Add(Console.ReadLine() ?? "");
                }
                q.QuestionOptions = options;

                Console.Write("Correct answer (type exactly one option OR type 1-4): ");
                var correctInput = Console.ReadLine() ?? "";
                string correctAnswer = correctInput;
                if (int.TryParse(correctInput, out int idx) && idx >= 1 && idx <= 4)
                    correctAnswer = options[idx - 1];
                q.QuestionCorrectAnswer = correctAnswer;
            }

            Console.Write($"New difficulty (Enter to keep '{q.QuestionDifficultyLevel}'): ");
            var diff = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(diff)) q.QuestionDifficultyLevel = diff;

            Console.WriteLine("Question updated.");
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    private void RemoveQuestion()
    {
        try
        {
            Console.Write("Enter QuestionID to remove: ");
            var qId = int.Parse(Console.ReadLine() ?? "0");

            var q = FindQuestionById(qId, out var cat);
            if (q == null || cat == null) { Console.WriteLine("Question not found."); return; }

            cat.Questions.Remove(q);
            Console.WriteLine("Question removed.");
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }
}