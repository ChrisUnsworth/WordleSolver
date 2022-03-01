using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleSolver.common;

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
    }
}
