using WordleSolver.common;
using WordleSolver.Data;

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

        private static Node ExpandNode(Node node, Match match) =>
            ExpandNode(node, match, new List<(Word word, Match match)>());

        private static Node ExpandNode(Node node, Match match, IEnumerable<(Word word, Match match)> given)
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

        public static IEnumerable<(string, Node)> MinMaxDepth()
        {
            Node best = default;
            var bestDepth = int.MaxValue;   
            var count = 0;

            foreach (var word in WordList.Answers)
            {
                var node = new Node(new Guess(word));

                foreach (var kvp in node.Guess.Outcomes)
                {
                    node.Next[kvp.Key] = MinMaxDepth_ExpandNode(node, kvp.Key);
                }

                var nodeMaxDepth = node.MaxDepth;

                if (nodeMaxDepth < bestDepth)
                { 
                    bestDepth = nodeMaxDepth;
                    best = node;
                    yield return ($"New best = {bestDepth}", best);
                }

                yield return ($"Done {++count} of {WordList.Answers.Length}", best);
            }
        }

        private static Node MinMaxDepth_ExpandNode(Node node, Match match) =>
            MinMaxDepth_ExpandNode(node, match, new List<(Word word, Match match)>());

        private static Node MinMaxDepth_ExpandNode(Node node, Match match, IEnumerable<(Word word, Match match)> given)
        {
            var nextGiven = new (Word word, Match match)[] { (node.Guess.GuessedWord, match) };
            var wordList = node.Guess.Outcomes[match];

            if (wordList.Count == 1)
            {
                return new Node(new Guess(wordList.First(), given.Concat(nextGiven)));
            }


            var nextNode = wordList
                .Select(w =>
                    {
                        var n = new Node(new Guess(w, given.Concat(nextGiven)));
                        foreach (var m in n.Guess.Outcomes.Keys)
                        {
                            n.Next[m] = ExpandNode(n, m, given.Concat(nextGiven));
                        }

                        return n;
                    })
                .MaxBy(n => n.MaxDepth);

            return nextNode;
        }
    }
}
