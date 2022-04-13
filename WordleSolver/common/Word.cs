using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleSolver.common
{
    public readonly struct Word
    {
        private readonly uint _value;

        public Word(string word)
        {
            uint intWord = 0;
            word = word.Trim().ToUpperInvariant();

            foreach (var idx in Enumerable.Range(0, 5))
            {
                intWord |= LetterToUint(word[idx]) << (5 * idx);
            }

            _value = intWord;
        }

        public Word(IEnumerable<char> word)
        {
            uint intWord = 0;

            foreach (var c in word.Reverse())
            {
                intWord <<= 5;
                intWord |= (uint)Char.ToUpperInvariant(c) - 'A';
            }

            _value = intWord;
        }

        public Word(IEnumerable<byte> word)
        {
            uint intWord = 0;

            foreach (var b in word)
            {
                intWord <<= 8;
                intWord |= b;
            }

            _value = intWord;
        }

        public char this[int idx] => UintToLetter((_value >> (5 * idx)) & 31);

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var idx in Enumerable.Range(0, 5))
            {
                result.Append(this[idx]);
            }

            return result.ToString();
        }

        public Match Compare(Word guess) => new(CompareEnumerable(guess));

        public bool Compatable((Word word, Match match) given)
        {
            foreach (var idx in Enumerable.Range(0, 5))
            {
                switch (given.match[idx])
                {
                    case 0:
                        if (ContainsLetter(given.word, idx)) return false;
                        break;
                    case 1:
                        if (!ContainsLetter(given.word, idx) || SameLetter(given.word, idx)) return false;
                        break;
                    case 2:
                        if (!SameLetter(given.word, idx)) return false;
                        break;
                }
            }

            return true;
        }

        public byte[] ToByteArray()
        {
            byte[] result;
            unchecked
            {
                result = new byte[4] 
                { 
                    (byte)(_value >> 24),
                    (byte)(_value >> 16),
                    (byte)(_value >> 8),
                    (byte)_value
                };
            }
            
            return result;
        }

        private IEnumerable<int> CompareEnumerable(Word guess)
        {
            foreach (var idx in Enumerable.Range(0, 5))
            {
                if (SameLetter(guess, idx))
                {
                    yield return 2;
                } else if (ContainsLetter(guess, idx))
                {
                    yield return 1;
                } else
                {
                    yield return 0;
                }
            }
        }

        private bool SameLetter(Word guess, int i) =>
           (_value & 31 << (i * 5)) == (guess._value & 31 << (i * 5));

        private bool ContainsLetter(Word guess, int i)
        {
            uint mask = (guess._value >> (i * 5)) & 31;

            foreach (var idx in Enumerable.Range(0, 5))
            {
                if ( ((_value >> (idx * 5)) & 31) == mask)
                {
                    return true;
                }
            }

            return false;
        }

        private static uint LetterToUint(char letter) => (uint)(letter - 'A');

        private static char UintToLetter(uint letter) => (char)(letter + 'A');

    }
}
