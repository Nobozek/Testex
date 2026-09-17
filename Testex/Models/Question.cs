using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Testex.Models
{
    internal class Question
    {
        private string _text;
        private bool _questionOpen;
        private List<string> _answers;
        private List<string> _rightAnswers;

        public Guid Id { get; }
        public string Text { get { return _text; } set { _text = value; } }
        public bool QuestionOpen { get { return _questionOpen; } set { _questionOpen = value; } }
        public List<string> Answers { get { return _answers; } set { _answers = value; } }
        public List<string> RightAnswers { get { return _rightAnswers; } set { _rightAnswers = value; } }

        [JsonConstructor]
        public Question(Guid id, string text, List<string> rightAnswers, List<string> answers = null, bool questionOpen = false)
        {
            Id = id;
            _questionOpen = questionOpen;
            _text = text;
            _answers = answers ?? new List<string>();
            _rightAnswers = rightAnswers;
        }

        public int CheckAnswer(List<string> userAnswers)
        {
            int points = 0;

            foreach(var userAnswer in userAnswers)
            {
                foreach (var answer in _rightAnswers)
                {
                    if (answer == userAnswer) points++;
                }
            }

            if (_questionOpen) points *= 2;

            return points;
        }
    }
}
