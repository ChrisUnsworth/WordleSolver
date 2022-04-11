using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleSolver.common;
using WordleSolver.Data;
using WordleSolver.Strategy;

namespace WordleSolver.Search
{
    public static class Widest
    {
        public static IStrategy GetStrategy() =>
            GetStrategy(Evaluator.MaxDepth);
        public static IStrategy GetStrategy(Func<IStrategy, double> evaluation)
        {
            var bestVal = double.MaxValue;
            var count = 1;
            IStrategy best = null;

            foreach (var strategy in WordList.Answers.Select(GetStrategy))
            {
                Console.WriteLine($"Evaluating {count++} / {WordList.Answers.Length} Best so far: {bestVal}");
                var eval = evaluation(strategy);
                if (eval < bestVal)
                {
                    bestVal = eval;
                    best = strategy;
                }
            }
            
            return best;
        }

        public static IStrategy GetStrategy(Word guessWord)
        {
            var root = new Node(new (guessWord));
            Expand(ref root);
            return root.ToStrategyNode();
        }

        public static void Expand(ref Node node)
        {
            foreach (var outcome in node.Guess.Outcomes.Where(p => p.Value.Count > 1))
            {
                var child = Expand(outcome, node.Guess);
                node.Next.Add(outcome.Key, child);
                Expand(ref child);
            }
        }

        public static Node Expand(KeyValuePair<Match, List<Word>> outcome, Guess guess) =>
            new(outcome.Value.Select(w => new Guess(w, guess.Givens.Concat(new[] { (guess.GuessedWord, outcome.Key) })))
                         .MaxBy(g => g.OutcomeCount));
    }
}
