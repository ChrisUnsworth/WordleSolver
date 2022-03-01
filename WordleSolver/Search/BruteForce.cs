using WordleSolver.common;

namespace WordleSolver.Search
{
    public static class BruteForce
    {
        public static Node FromFirstGuess(Word word)
        {
            var root = new Node(new Guess(word));

            foreach (var kvp in root.Guess.Outcomes)
            {
                root.Next[kvp.Key] = ExpandNode(root, kvp.Key);
            }

            return root;
        }

        public static Node ExpandNode(Node node, Match match) =>
            ExpandNode(node, match, new List<(Word word, Match match)>());

        public static Node ExpandNode(Node node, Match match, IEnumerable<(Word word, Match match)> given)
        {
            var nextGiven = new (Word word, Match match)[] { (node.Guess.GuessedWord, match) };
            var wordList = node.Guess.Outcomes[match];
            var nextNode = new Node(new Guess(wordList.First(), given.Concat(nextGiven)));

            if (wordList.Count == 1)
            {
                return nextNode;
            }

            foreach (var m in nextNode.Guess.Outcomes.Keys)
            {
                nextNode.Next[m] = ExpandNode(nextNode, m, given.Concat(nextGiven));
            }

            return nextNode;
        }
    }
}
