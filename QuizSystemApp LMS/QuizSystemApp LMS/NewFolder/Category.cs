using System;
using System.Collections.Generic;
using System.Text;

namespace QuizSystemApp_LMS.NewFolder;
public class Category //Luan McShane B00983827
{
    // Read-only ID (as per UML)
    public int CategoryID { get; }

    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }

    // Category HAS Questions (UML association)
    public List<Question> Questions { get; } = new();

    public Category(int categoryID, string categoryName, string categoryDescription)
    {
        CategoryID = categoryID;
        CategoryName = categoryName;
        CategoryDescription = categoryDescription;
    }

    // Optional helper methods (safe to keep)
    public void AddQuestion(Question question)
    {
        Questions.Add(question);
    }

    public bool RemoveQuestion(int questionId)
    {
        var q = Questions.Find(q => q.QuestionID == questionId);
        if (q == null) return false;

        Questions.Remove(q);
        return true;
    }

    public override string ToString()
    {
        return $"{CategoryID}: {CategoryName} - {CategoryDescription} (Questions: {Questions.Count})";
    }
}
