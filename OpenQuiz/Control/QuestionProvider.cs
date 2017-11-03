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
            List<Word> choices = CreateDistinctChoices();
            Word word = GetRandomWord(choices);

            return new Question(word, choices);
        }

        private List<Word> CreateDistinctChoices()
        {
            List<Word> choices = new List<Word>();
            while (choices.Count < GetPossibleChoiceCount())
            {
                Word word = GetRandomWord();

                if(!choices.Contains(word))
                    choices.Add(word);
            }
            return choices;
        }

        private int GetPossibleChoiceCount()
        {
            int size = settings.ChoiceCount;
            if(dictionary.Count < size)
            {
                size = dictionary.Count;
            }
            return size;
        }

        private Word GetRandomWord()
        {
            return GetRandomWord(dictionary);
        }

        private Word GetRandomWord(List<Word> choices)
        {
            return choices[random.Next(choices.Count)];
        }
    }
}
