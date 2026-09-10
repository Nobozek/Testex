using System;
using System.Collections.Generic;
using System.Text;

namespace Testex.Models
{
    internal class Question
    {
        private string _text;
        private bool _questionOpen;
        private List<string> _answers;
        private List<string> _rightAnswers;

        public Guid Id { get; }
        public string text { get { return _text; } set { _text = value; } }
        public bool questionOpen { get { return _questionOpen;  } set { _questionOpen = value; } }
        public List<string> answers { get { return answers; } set { answers = value; } }

        public Question(string text, List<string> answers, List<string> rightAnswers)
        {
            Id = Guid.NewGuid();
            _questionOpen = false;
            _text = text;
            _answers = answers;
            _rightAnswers = rightAnswers;
        }

        public Question(string text, List<string> rightAnswers)
        {
            Id = Guid.NewGuid();
            _questionOpen = true;
            _text = text;
            _rightAnswers = rightAnswers;
        }

        public Question(Guid id, string text, List<string> answers, List<string> rightAnswers)
        {
            Id = id;
            _questionOpen = false;
            _text = text;
            _answers = answers;
            _rightAnswers = rightAnswers;
        }

        public Question(Guid id, string text, List<string> rightAnswers)
        {
            Id = id;
            _questionOpen = true;
            _text = text;
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
