using OpenQuiz.Model;
using System;
using Windows.Storage;

namespace OpenQuiz.Control
{
    class SettingsProvider
    {
        private ApplicationDataContainer container;

        public SettingsProvider(string category)
        {
            ApplicationDataContainer settings = App.roamingSettings.CreateContainer("Settings", ApplicationDataCreateDisposition.Always);
            container = settings.CreateContainer(category, ApplicationDataCreateDisposition.Always);
        }

        public void SetSetting(string key, int value)
        {
            container.Values[key] = value;
        }

        public int GetSetting(string key)
        {
            int value = GetDefault(key);

            if (container.Values.ContainsKey(key))
            {
                value = (int)container.Values[key];
            }
            return value;
        }

        private int GetDefault(string key)
        {
            switch (key)
            {
                case PageSettings.WAIT_TIME: return 1500;
                case PageSettings.CHOICE_COUNT: return 4;
            }
            throw new Exception();
        }
    }
}
