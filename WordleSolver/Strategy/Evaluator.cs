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
            if (!strategy.Options.Any()) { return 1; }

            double maxDepth = 0;
            foreach (var match in strategy.Options) 
            {
                maxDepth = Math.Max(maxDepth, MaxDepth(strategy.Next(match)));
            }

            return maxDepth + 1;
        }
    }
}
