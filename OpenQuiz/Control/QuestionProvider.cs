using Newtonsoft.Json;
using OpenQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenQuiz.Control
{
    class QuestionProvider
    {
        private Random random = new Random();
        private List<Word> dictionary;
        private PageSettings settings;

        public QuestionProvider(PageSettings settings)
        {
            this.settings = settings;
        }
        
        public Task Load(string resourceName)
        {
            return Task.Run(() =>
            {
                string content = GetResourceContent(resourceName);
                dictionary = JsonConvert.DeserializeObject<List<Word>>(content);
            });
        }

        private string GetResourceContent(String resourceName)
        {
            return new UserDictionaryService().Read(resourceName);
        }

        public Question Next()
        {
            Word word = GetRandomWord();
            List<Word> choices = new List<Word>
            {
                word
            };
            FillWithDistinct(choices);

            return new Question(word, choices.OrderBy(c => random.Next()).ToList());
        }

        private void FillWithDistinct(List<Word> choices)
        {
            while (choices.Count < settings.ChoiceCount)
            {
                Word word = GetRandomWord();

                if(!choices.Contains(word))
                    choices.Add(word);
            }
        }

        private Word GetRandomWord()
        {
            return dictionary[random.Next(dictionary.Count)];
        }
    }
}
