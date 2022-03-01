using System.Reflection;

using WordleSolver.common;

namespace WordleSolver.Data
{
    public static class WordList
    {
        private static Word[] _answers;

        public static Word[] Answers 
        { 
            get 
            { 
                if (_answers == null) _answers = GetAnswerList().ToArray();
                return _answers; 
            } 
        }

        private static IEnumerable<Word> GetAnswerList()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "WordleSolver.Data.wordle-answers-alphabetical.txt";

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            using StreamReader reader = new(stream);
            while (TryReadWord(reader, out Word word))
            {
                yield return word;
                if (!reader.EndOfStream) reader.Read();
            }
        }

        private static bool TryReadWord(StreamReader reader, out Word word)
        {
            word = default;
            var wordArray = new char[5];
            for (int i = 0; i < 5; i++)
            {
                if (reader.EndOfStream) return false;
                wordArray[i] = (char)reader.Read();
            }

            word = new(wordArray);
            return true;
        }
    }
}
