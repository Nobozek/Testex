using Testex.Models;
using System.IO;
using System.Text.Json;

namespace Testex;

public partial class TestPage : ContentPage
{
    private List<Question> questions = new List<Question>();
    private List<Question> choosen_questions = new List<Question>();
    private Random random = new Random();


    public TestPage()
	{
		InitializeComponent();
        GetQuestions();
        SelectQuestions();
	}

    private void GetQuestions()
    {
        string json = File.ReadAllText("f1_questions.json");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        questions = JsonSerializer.Deserialize<List<Question>>(json, options);
    }

    private void SelectQuestions()
    {
        choosen_questions.Clear();
        choosen_questions.Add(questions[random.Next(questions.Count)]);

        while (choosen_questions.Count() < 10)
        {
            bool skip = false;
            var question = questions[random.Next(questions.Count)];

            foreach (var cq in choosen_questions)
            {
                if (cq.Id == question.Id) skip = true; break;
            }

            if (!skip) choosen_questions.Add(question);
        }
    }
}