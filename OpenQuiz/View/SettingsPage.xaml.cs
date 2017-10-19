using OpenQuiz.Control;
using OpenQuiz.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

namespace OpenQuiz
{
    public sealed partial class SettingsPage : Page, INotifyPropertyChanged
    {
        private UserDictionaryService dictionaryService = new UserDictionaryService();
        private PageSettings dictionarySettings = new PageSettings("Dictionary");

        private PackageVersion version = Package.Current.Id.Version;
        private ObservableCollection<string> userDictionaries;

        public SettingsPage()
        {
            InitializeComponent();
            Transitions = new TransitionCollection
            {
                new EntranceThemeTransition()
            };
            SetCurrentRadioButton();
            ReloadUserDictionaryList();
        }

        public string VersionString
        {
            get { return string.Format("Version {0}.{1}.{2}", version.Major, version.Minor, version.Build); }
        }

        public ObservableCollection<string> UserDictionaries
        {
            get { return userDictionaries; }
            set { userDictionaries = value; OnPropertyChanged("UserDictionaries"); }
        }


        private void ReloadUserDictionaryList()
        {
            UserDictionaries = new ObservableCollection<string>(dictionaryService.GetDictionaryList());
        }

        private void SetCurrentRadioButton()
        {
            foreach (RadioButton rb in DictionaryChoiceCountPanel.Children)
            {
                if(rb.Tag as string == dictionarySettings.ChoiceCount.ToString())
                {
                    rb.IsChecked = true;
                }
            }
        }

        private async void AddUserDictionary_Click(object sender, RoutedEventArgs args)
        {
            await new AddDictionaryDialog().ShowAsync();
            ReloadUserDictionaryList();
        }

        private void DeleteUserDictionary_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            dictionaryService.Delete(button.Tag as string);
            ReloadUserDictionaryList();
        }

        private void DictionarySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (e.OldValue == 0)
                return;
            if (sender is Slider slider)
            {
                dictionarySettings.WaitTime = (int)slider.Value;
            }
        }

        private void DictionaryRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb)
            {
                dictionarySettings.ChoiceCount = Int32.Parse(rb.Tag as string);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
