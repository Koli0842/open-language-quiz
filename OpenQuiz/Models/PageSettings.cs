using OpenQuiz.Control;

namespace OpenQuiz.Models
{
    class PageSettings
    {
        public const string WAIT_TIME = "WaitTime";
        public const string CHOICE_COUNT = "ChoiceCount"; 

        private string category;

        private int waitTime;
        private int choiceCount;

        SettingsProvider settingsProvider;
        
        internal PageSettings(string category)
        {
            this.category = category;
            settingsProvider = new SettingsProvider(category);

            WaitTime = settingsProvider.GetSetting(WAIT_TIME);
            ChoiceCount = settingsProvider.GetSetting(CHOICE_COUNT);
        }

        internal int WaitTime
        {
            get { return waitTime; }
            set { waitTime = value; settingsProvider.SetSetting(WAIT_TIME, value); }
        }
        internal int ChoiceCount
        {
            get { return choiceCount; }
            set { choiceCount = value; settingsProvider.SetSetting(CHOICE_COUNT, value); }
        }
    }
}
