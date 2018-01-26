using System;

namespace OpenQuiz.Model
{
    class Word : IEquatable<Word>
    {
        private string query;
        private string result;

        public string Query
        {
            get;
            set;
        }

        public string Result
        {
            get;
            set;
        }

        public bool Equals(Word other)
        {
            return query == other.query || result == other.result;
        }
    }
}
