namespace WordleSolver.common
{
    public interface IStrategy
    {
        public Word Guess { get; }

        public IStrategy Next(Match result);

        public IEnumerable<Match> Options { get; }
    }
}
