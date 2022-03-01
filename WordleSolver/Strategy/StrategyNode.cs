using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleSolver.common;

namespace WordleSolver.Strategy
{
    public readonly struct StrategyNode
    {
        public readonly Word Guess { get; }

        public readonly IReadOnlyDictionary<Match, StrategyNode> Next { get;}
    }
}
