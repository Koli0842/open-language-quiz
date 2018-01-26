using OpenQuiz.Model;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace OpenQuiz.View
{
    public sealed partial class StatisticsPage : Page, INotifyPropertyChanged
    {
        private Score dictionary;
        private Score Dictionary
        {
            get { return dictionary; }
            set { dictionary = value; OnPropertyChanged("Dictionary"); }
        }

        public StatisticsPage()
        {
            InitializeComponent();
            Transitions = new TransitionCollection
            {
                new EntranceThemeTransition()
            };
            dictionary = new Score("Dictionary");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
