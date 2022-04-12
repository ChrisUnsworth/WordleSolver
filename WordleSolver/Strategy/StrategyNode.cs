using WordleSolver.common;

namespace WordleSolver.Strategy
{
    public readonly struct StrategyNode : IStrategy
    {
        private const byte EndMarker = byte.MaxValue;

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

        public void Write(Stream stream)
        {
            stream.Write(Guess.ToByteArray());
            foreach (var pair in Next)
            {
                stream.Write(pair.Key.ToByteArray());
                pair.Value.Write(stream);
            }

            stream.WriteByte(EndMarker);
        }

        public static StrategyNode Read(Stream stream)
        {
            var wordBytes = new byte[4];
            stream.Read(wordBytes, 0, wordBytes.Length);
            var word = new Word(wordBytes);
            var nextNodes = new Dictionary<Match, StrategyNode>();
            while (TryReadNode(stream, out (StrategyNode node, Match match) nextNode))
            {
                nextNodes.Add(nextNode.match, nextNode.node);
            }

            return new StrategyNode(word, nextNodes);
        }

        public static bool TryReadNode(Stream stream, out (StrategyNode node, Match match) node)
        {
            var bytes = new byte[2];
            int next = stream.ReadByte();
            if (next == -1 || next == EndMarker)
            {
                node = default;
                return false;
            }

            bytes[0] = (byte)next;
            bytes[1] = (byte)stream.ReadByte();
            var match = new Match(bytes);

            node = (Read(stream), match);
            return true;
        }

        public int Size() => 5 + Next.Sum(p => 2 + p.Value.Size());
    }
}
