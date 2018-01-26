using System.Collections.Generic;
using System.ComponentModel;

namespace OpenQuiz.Model
{
    class Question : INotifyPropertyChanged
    {
        private Word word;
        private List<Word> choices;

        private bool answered = false;
        private bool correct;

        public Question(Word word, List<Word> choices)
        {
            this.word = word;
            this.choices = choices;
        }

        public Word Word {
            get { return word; }
            set { word = value; OnPropertyChanged("Word"); }
        }
        public List<Word> Choices {
            get { return choices; }
            set { choices = value; OnPropertyChanged("Choices"); }
        }

        public void Answer(Word choice)
        {
            answered = true;
            correct = choice == word;
        }

        public bool IsCorrect()
        {
            return correct;
        }

        public bool IsAnswered()
        {
            return answered;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
