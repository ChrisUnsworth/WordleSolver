using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

using WordleSolver.common;
using WordleSolver.Search;
using WordleSolver.Strategy;

namespace WordleSolverTests
{
    [TestClass]
    public class IOTest
    {
        [TestMethod]
        public void IO()
        {
            var strategy = Widest.GetStrategy(new Word("APPLE"));
            var size = strategy.Size();

            using var stream = new MemoryStream(new byte[strategy.Size()]);
            strategy.Write(stream);
            stream.Position = 0;
            var newStrategy = StrategyNode.Read(stream);
            Assert.AreEqual(size, newStrategy.Size());
            Compare(strategy, newStrategy);
        }

        private void Compare(IStrategy s1, IStrategy s2)
        {
            Assert.AreEqual(s1.Guess.ToString(), s2.Guess.ToString());
            Assert.AreEqual(s1.Options.Count(), s2.Options.Count());
            foreach (var match in s1.Options)
            {
                Compare(s1.Next(match), s2.Next(match));
            }
        }
    }
}
