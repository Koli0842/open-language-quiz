using OpenQuiz.Control;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace OpenQuiz
{
    public sealed partial class DictionarySelectionPage : Page
    {
        private UserDictionaryService dictionaryService = new UserDictionaryService();
        List<string> dictionaryList;

        public DictionarySelectionPage()
        {
            InitializeComponent();
            Transitions = new TransitionCollection
            {
                new EntranceThemeTransition()
            };
            dictionaryList = dictionaryService.GetDictionaryList();
        }

        private void GenericButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            NavigateToQuiz(button.Tag as string);
        }
        
        private void NavigateToQuiz(String resourceName)
        {
            Frame.Navigate(typeof(DictionaryPage), resourceName);
        }
    }
}
