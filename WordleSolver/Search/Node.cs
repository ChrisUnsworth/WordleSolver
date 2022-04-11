using WordleSolver.common;
using WordleSolver.Strategy;

namespace WordleSolver.Search
{
    public class Node
    {
        public Guess Guess { get; }

        public Dictionary<Match, Node> Next { get; }

        public Node(Guess guess)
        {
            Guess = guess;
            Next = new Dictionary<Match, Node>();
        }

        public int MaxDepth => Next.Any()
            ? Next.Values.Max(n => n.MaxDepth) + 1
            : 1;

        public StrategyNode ToStrategyNode() =>
            new(Guess.GuessedWord, Next.Select(_ => new KeyValuePair<Match, StrategyNode>(_.Key, _.Value.ToStrategyNode())));
    }
}
