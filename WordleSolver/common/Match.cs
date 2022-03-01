namespace WordleSolver.common
{
    public readonly struct Match
    {
        private readonly ushort _match;

        public Match(IEnumerable<int> comparison)
        {
            ushort match = 0;
            foreach (ushort i in comparison.Reverse())
            {
                match <<= 2;
                match |= i;
            }
            
            _match = match;
        }

        public int this[int i]
        {
            get => (_match >> (i * 2)) & 3;
        }
    }
}
