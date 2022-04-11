using WordleSolver.common;
using WordleSolver.Data;

namespace WordleSolver.Search
{
    public readonly struct Guess
    {
        private readonly Word _guess;
        private readonly (Word word, Match match)[] _givens;
        private readonly Dictionary<Match, List<Word>> _outcomes;

        public Guess(Word guess) : this(guess, new List<(Word word, Match match)>()) { }

        public Guess(Word guess, IEnumerable<(Word word, Match match)> given)
        {
            _guess = guess;
            _outcomes = new Dictionary<Match, List<Word>>();
            _givens = given.ToArray();
            PopulateOutcomes(given);
        }

        public Word GuessedWord => _guess;

        public IEnumerable<(Word word, Match match)> Givens => _givens;

        // 1 / WordCount = probability this guess is right
        public int WordCount => _outcomes.Values.Sum(l => l.Count);

        public int OutcomeCount => _outcomes.Count();

        public int CertainOutcomes => _outcomes.Values.Where(l => l.Count == 1).Count();

        public int UnCertainOutcomes => _outcomes.Values.Where(l => l.Count > 1).Count();

        public IReadOnlyDictionary<Match, List<Word>> Outcomes => _outcomes;

        private void PopulateOutcomes(IEnumerable<(Word, Match)> given)
        {
            foreach (var word in WordList.Answers.Where(a => given.All(g => a.Compatable(g))))
            {
                var match = word.Compare(_guess);

                if (_outcomes.TryGetValue(match, out var list))
                {
                    list.Add(word);
                } else
                {
                    _outcomes[match] = new List<Word>() { word };
                }
            }
        }
    }
}
