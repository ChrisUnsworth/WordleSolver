using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WordleSolver.common;

namespace WordleSolverTests
{
    [TestClass]
    public class WordTests
    {
        [TestMethod]
        public void Constructor()
        {
            var @string = "Tests";
            var word = new Word(@string);

            Assert.AreEqual(@string.ToUpperInvariant(), word.ToString());
        }

        [TestMethod]
        public void FullMatch()
        {
            var @string = "APPLE";
            var word = new Word(@string);
            var guess = new Word(@string);

            var match = word.Compare(guess);


            foreach (var idx in Enumerable.Range(0, 5))
            {
                Assert.AreEqual(2, match[idx]);
            }

            Assert.IsTrue(match.AreEqual);
        }

        [TestMethod]
        public void OneMatch()
        {
            var word = new Word("APPLE");
            var guess = new Word("AMMMM");

            var match = word.Compare(guess);

            Assert.AreEqual(2, match[0]);

            foreach (var idx in Enumerable.Range(1, 4))
            {
                Assert.AreEqual(0, match[idx]);
            }

            Assert.IsFalse(match.AreEqual);
        }

        [TestMethod]
        public void OneMatchOneContains()
        {
            var word = new Word("APPLE");
            var guess = new Word("AMMMP");

            var match = word.Compare(guess);

            Assert.AreEqual(2, match[0]);
            Assert.AreEqual(1, match[4]);

            foreach (var idx in Enumerable.Range(1, 3))
            {
                Assert.AreEqual(0, match[idx]);
            }

            Assert.IsFalse(match.AreEqual);
        }

        [TestMethod]
        public void CompatableTest1()
        {
            var word = new Word("APPLE");

            var match = new Match(new int[] { 1, 0, 0, 0, 0 });

            var guess = new Word("XXXXX");

            Assert.IsFalse(guess.Compatable((word, match)));
        }

        [TestMethod]
        public void CompatableTest2()
        {
            var word = new Word("APPLE");

            var match = new Match(new int[] { 0, 2, 1, 0, 0 });

            var guess = new Word("XPXXX");

            Assert.IsTrue(guess.Compatable((word, match)));
        }

        [TestMethod]
        public void CompatableTest3()
        {
            var word = new Word("APPLE");

            var match = new Match(new int[] { 2, 2, 1, 2, 2 });

            var guess = new Word("APPLE");

            Assert.IsFalse(guess.Compatable((word, match)));
        }

        [TestMethod]
        public void CompatableTest4()
        {
            var word = new Word("APPLE");

            var match = new Match(new int[] { 2, 2, 0, 2, 2 });

            var guess = new Word("APPLE");

            Assert.IsFalse(guess.Compatable((word, match)));
        }

        [TestMethod]
        public void CompatableTest5()
        {
            var word = new Word("APPLE");

            var match = new Match(new int[] { 2, 2, 2, 2, 2 });

            var guess = new Word("APPLE");

            Assert.IsTrue(guess.Compatable((word, match)));
        }

        [TestMethod]
        public void ByteRoundTrip()
        {
            var stringWord = "APPLE";
            var word = new Word(stringWord);
            var bytes = word.ToByteArray();
            var newWord = new Word(bytes);
            Assert.AreEqual(stringWord, newWord.ToString());
        }
    }
}