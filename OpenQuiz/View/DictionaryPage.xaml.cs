using OpenQuiz.Control;
using OpenQuiz.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace OpenQuiz
{
    public sealed partial class DictionaryPage : Page, INotifyPropertyChanged
    {
        private QuestionProvider questionProvider;
        private Question question;

        private Task loadingTask;

        internal Score score;
        internal PageSettings settings;
        
        public DictionaryPage()
        {
            InitializeComponent();
            SetBackButtonBehaviour();

            settings = new PageSettings("Dictionary");
            questionProvider = new QuestionProvider(settings);
        }

        private Question Question
        {
            get { return question; }
            set { question = value; OnPropertyChanged("Question"); }
        }

        private void SetBackButtonBehaviour()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                Frame.Navigate(typeof(DictionarySelectionPage));
                a.Handled = true;
            };
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string resourceName = e.Parameter as string;

            score = new Score("Dictionary");

            loadingTask = questionProvider.Load(resourceName);

            NextQuestion();
        }

        internal async void NextQuestion()
        {
            await loadingTask;
            LoadingProgressBar.Visibility = Visibility.Visible;

            Question = questionProvider.Next();

            LoadingProgressBar.Visibility = Visibility.Collapsed;
        }

        internal async void CheckCorrect(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Word word = button.Tag as Word;

            if (question.IsAnswered())
                return;

            question.Answer(word);
            
            if (question.IsCorrect())
            {
                button.Background = App.correctBrush;
                score.Correct++;
                score.CurrentStreak++;
            }
            else
            {
                button.Background = App.incorrectBrush;
                score.CurrentStreak = 0;

                foreach (Button b in GetButton(AnswerGrid))
                    if (b.Tag == question.Word)
                        b.Background = App.correctBrush;
            }
            score.Answered++;

            await Task.Delay(settings.WaitTime);
            NextQuestion();
        }

        private List<Button> GetButton(DependencyObject parent)
        {
            var _List = new List<Button>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Button)
                    _List.Add(_Child as Button);
                _List.AddRange(GetButton(_Child));
            }
            return _List;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
