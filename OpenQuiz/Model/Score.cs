using OpenQuiz.Control;
using System;
using System.ComponentModel;

namespace OpenQuiz.Model
{
    class Score : INotifyPropertyChanged
    {
        public const string HIGHEST_STREAK = "HighestStreak";
        public const string ANSWERED = "Answered";
        public const string CORRECT = "Correct";

        private ScoreService scoreService;

        private int highestStreak;
        private int answered;
        private int correct;

        private int currentStreak = 0;

        public Score(string category)
        {
            scoreService = new ScoreService(category);

            highestStreak = scoreService.GetScore(HIGHEST_STREAK);
            answered = scoreService.GetScore(ANSWERED);
            correct = scoreService.GetScore(CORRECT);
        }

        public int HighestStreak
        {
            get { return highestStreak; }
            set { highestStreak = value; scoreService.SetScore(HIGHEST_STREAK, highestStreak); }
        }
        public int Answered
        {
            get { return answered; }
            set { answered = value; scoreService.SetScore(ANSWERED, answered); }
        }
        public int Correct
        {
            get { return correct; }
            set { correct = value; scoreService.SetScore(CORRECT, correct); }
        }

        public int CurrentStreak
        {
            get { return currentStreak; }
            set { currentStreak = value; OnPropertyChanged("CurrentStreak"); UpdateHighestStreak(); }
        }

        public double Accuracy
        {
            get { return Math.Round((float)correct / answered * 100, 1); }
        }

        private void UpdateHighestStreak()
        {
            if (CurrentStreak > HighestStreak)
                HighestStreak = CurrentStreak;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
