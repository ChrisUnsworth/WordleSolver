namespace WordleSolver.common
{
    public interface IStrategy
    {
        public Word Guess { get; }

        public IStrategy Next(Match result);

        public IEnumerable<Match> Options { get; }

        public void Write(Stream stream);

        public int Size();
    }
}
