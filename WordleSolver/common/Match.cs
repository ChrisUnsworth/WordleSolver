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

        public bool AreEqual => _match == 0B_1010101010;

        public Match(byte[] bytes)
        {
            ushort match = bytes[0];
            match <<= 8;
            match |= bytes[1];
            _match = match;
        }

        public int this[int i]
        {
            get => (_match >> (i * 2)) & 3;
        }

        public byte[] ToByteArray()
        {
            byte[] result;
            unchecked
            {
                result = new byte[2]
                {
                    (byte)(_match >> 8),
                    (byte)_match
                };
            }

            return result;
        }
    }
}
