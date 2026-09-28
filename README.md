# QuizSystemApp LMS – Group 3

A console-based quiz management system for a Learning Management System (LMS), written in **C# (.NET 10)**. Administrators log in to create and manage quiz **categories** and multiple-choice **questions**. The design follows the group's UML class diagram, using classes, inheritance and associations.

> **Status:** in development. The Admin features are complete; the Student quiz mode is planned for a later stage.

---

## Features

### Admin
- **Admin login** – username and password check, with the login time recorded
- **Category management**
  - View all categories with their question counts
  - Add a new category (IDs are assigned automatically)
  - Update a category's name or description (press Enter to keep the current value)
  - Remove a category
- **Question management**
  - View every question grouped by category, with its options, correct answer and difficulty
  - Add a multiple-choice question (4 options) to a category – the correct answer can be typed out or picked by number (1–4)
  - Set a difficulty level: Easy / Medium / Hard
  - Update a question's text, options, correct answer or difficulty
  - Remove a question
- **Input handling** – invalid menu choices and non-numeric IDs are caught with a friendly message instead of crashing

### Student *(planned)*
- Take quizzes by category and view scores

## Sample data
The app starts with sample data so it can be tried straight away:

| Type | Data |
|---|---|
| Admin account | username `admin`, password `admin123` |
| Categories | *OOP Fundamentals*, *Data Structures* |
| Questions | "What does OOP stand for?" (OOP Fundamentals, Easy) |

## Class design

```
User  (UserID, Username, Password, Email, Role, Login(), Logout(), UpdateProfile())
 └── Admin  (LoginDate, CreateSampleAdmins())

Category  (CategoryID, CategoryName, CategoryDescription)
 └── has many ── Question  (QuestionID, QuestionText, QuestionOptions,
                            QuestionCorrectAnswer, QuestionDifficultyLevel)

QuizSystem  – holds the admins and categories, loads sample data, authenticates admins
MainMenu    – start menu (Admin / Student / Exit)
AdminMenu   – category and question management screens
```

- **Inheritance:** `Admin` extends `User`
- **Association:** each `Category` contains a list of `Question` objects
- **Encapsulation:** IDs are read-only, and each menu class handles only its own screen

## Project structure

```
QuizSystemApp LMS/
├── QuizSystemApp LMS.slnx
└── QuizSystemApp LMS/
    ├── Program.cs                  # Entry point – loads sample data, opens main menu
    └── NewFolder/
        ├── User.cs                 # Base user class
        ├── Admin.cs                # Admin user (inherits User)
        ├── Category.cs             # Quiz category (contains Questions)
        ├── question.cs             # Multiple-choice question
        └── Core/
            ├── QuizSystem.cs       # System data + admin authentication
            └── Menus/
                ├── MainMenu.cs     # Main menu
                └── AdminMenu.cs    # Admin category/question management
```

## Getting started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2026 (or VS Code with the C# extension)

### Run
**Visual Studio:** open `QuizSystemApp LMS.slnx` and press **F5**.

**Command line:**
```bash
cd "QuizSystemApp LMS/QuizSystemApp LMS"
dotnet run
```

### Quick demo
1. Choose `1` (Admin) and log in with `admin` / `admin123`
2. Choose `1` (Manage Categories) → `1` to view the sample categories
3. Go back with `0`, then choose `2` (Manage Questions) → `2` to add a question

## Known limitations / future work
- Data is stored **in memory only** – changes are lost when the app closes (a file or database could be added)
- Passwords are stored and compared as **plain text** – a real system would hash them (e.g. with BCrypt or PBKDF2)
- Student mode (taking quizzes and scoring) is not yet implemented
- The `NewFolder` directory could be renamed to something clearer, such as `Models`

## Team
**Group 3** – Ulster University

- Luan McShane (B00983827)
- *(add other team members here)*
[README.md](https://github.com/user-attachments/files/32772778/README.md)
