using Testex.Models;

namespace Testex
{
    public partial class MainPage : ContentPage
    {
        private List<Question> questions = new List<Question>();
        private List<Question> choosen_questions = new List<Question>();
        private Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
        }

        private void GetQuestions()
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
}
