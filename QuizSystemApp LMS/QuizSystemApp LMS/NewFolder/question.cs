using System;
using System.Collections.Generic;
using System.Text;

namespace QuizSystemApp_LMS.NewFolder;
public class Question //Luan McShane B00983827
{
    public int QuestionID { get; }
    public string QuestionText { get; set; }
    public List<string> QuestionOptions { get; set; }
    public string QuestionCorrectAnswer { get; set; }
    public string QuestionDifficultyLevel { get; set; }

    public Question(int questionID, string questionText, List<string> questionOptions,
        string questionCorrectAnswer, string questionDifficultyLevel)
    {
        QuestionID = questionID;
        QuestionText = questionText;
        QuestionOptions = questionOptions;
        QuestionCorrectAnswer = questionCorrectAnswer;
        QuestionDifficultyLevel = questionDifficultyLevel;
    }
}