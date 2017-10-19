using Windows.Storage;

namespace OpenQuiz.Control
{
    class ScoreService
    {
        private ApplicationDataContainer container;

        public ScoreService(string category)
        {
            ApplicationDataContainer score = App.roamingSettings.CreateContainer("Score", ApplicationDataCreateDisposition.Always);
            container = score.CreateContainer(category, ApplicationDataCreateDisposition.Always);
        }

        public void SetScore(string key, int score)
        {
            container.Values[key] = score;
        }

        public int GetScore(string key)
        {
            int score = 0;
            if (container.Values.ContainsKey(key))
            {
                score = (int)container.Values[key];
            }
            return score;
        }
    }
}
