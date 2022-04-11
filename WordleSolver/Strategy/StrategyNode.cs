using WordleSolver.common;

namespace WordleSolver.Strategy
{
    public readonly struct StrategyNode : IStrategy
    {
        public readonly Word Guess { get; }

        public readonly IReadOnlyDictionary<Match, StrategyNode> Next { get; }

        public IEnumerable<Match> Options => Next.Keys;

        public StrategyNode(Word guess, IEnumerable<KeyValuePair<Match, StrategyNode>> next)
        {
            Guess = guess;
            Next = next.ToDictionary(_ => _.Key, _ => _.Value);
        }

        public StrategyNode(Word guess, IReadOnlyDictionary<Match, StrategyNode> next)
        {
            Guess = guess;
            Next = next;
        }

        IStrategy IStrategy.Next(Match result)
        {
            if (Next.TryGetValue(result, out StrategyNode node))
            {
                return node;
            }

            throw new ArgumentException("No strategy found for the given result.", nameof(result));
        }
    }
}
