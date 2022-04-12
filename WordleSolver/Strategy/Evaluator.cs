using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleSolver.common;

namespace WordleSolver.Strategy
{
    public static class Evaluator
    {
        public static double MaxDepth(IStrategy strategy)
        {
            double maxDepth = 0;
            foreach (var match in strategy.Options) 
            {
                maxDepth = Math.Max(maxDepth, MaxDepth(strategy.Next(match)));
            }

            return maxDepth + 1;
        }

        public static List<int> GetCounts(IStrategy strategy)
        {
            var counts = new List<int>() { 1 };

            foreach (var match in strategy.Options)
            {
                var childCounts = GetCounts(strategy.Next(match));

                while (childCounts.Count <= counts.Count) { counts.Add(0); }

                foreach (var idx in Enumerable.Range(1, childCounts.Count))
                {
                    counts[idx] += childCounts[idx - 1];
                }
            }

            return counts;
        }
    }
}
