using Testex.Models;
using System.IO;
using System.Text.Json;

namespace Testex;

public partial class TestPage : ContentPage
{
    List<Question> questions = new List<Question>();
    List<Question> choosen_questions = new List<Question>();
    private Question _currentQuestion;
    public Question CurrentQuestion { get { return _currentQuestion; } set { _currentQuestion = value; OnPropertyChanged(); } }
    private string _selectedAnswer;
    public string SelectedAnswer { get { return _selectedAnswer; } set { _selectedAnswer = value; OnPropertyChanged(); } }

    Random random = new Random();

    int current_question_index = 0;
    int points = 0;
    int max_points = 0;

    public TestPage()
	{
		InitializeComponent();
        BindingContext = this;
        GetQuestions();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Reset();
    }

    private void Reset()
    {
        current_question_index = 0;
        points = 0;
        max_points = 0;
        SelectQuestions();
        GetCurrentQuestion();
    }

    private void GetQuestions()
    {
        if (questions.Count > 0) return;    

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
                if (cq.Id == question.Id)
                {
                    skip = true;
                    break;
                }
            }

            if (!skip) choosen_questions.Add(question);
        }

        foreach (var question in choosen_questions)
        {
            if(question.QuestionOpen)
            {
                max_points += 2;
            }
            else
            {
                max_points++;
            }
        }
    }

    private void GetCurrentQuestion()
    {
        if (current_question_index == choosen_questions.Count)
        {
            DisplayAlert("Test zakończony", $"Zdobyłeś {points}/{max_points} punktów!", "OK");
            Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        else
        {
            CurrentQuestion = choosen_questions[current_question_index];
            current_question_index++;
        }
    }

    public void NextQuestion(object sender, EventArgs e)
    {
        if(_currentQuestion.QuestionOpen)
        {
            string answer = openAnswerEntry.Text;
        }
        else
        {
            string answer = SelectedAnswer;
        }

        points = _currentQuestion.CheckAnswer(new List<string> { SelectedAnswer }, points);

        SelectedAnswer = null;
        openAnswerEntry.Text = null;

        GetCurrentQuestion();
    }
}