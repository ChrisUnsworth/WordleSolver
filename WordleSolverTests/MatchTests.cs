using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using WordleSolver.common;

namespace WordleSolverTests
{
    [TestClass]
    public class MatchTests
    {

        [TestMethod]
        public void ByteRoundTrip()
        {
            var input = new int[] { 1, 0, 2, 2, 1 };
            var match = new Match(input);
            var bytes = match.ToByteArray();
            var output = new Match(bytes);

            foreach (var idx in Enumerable.Range(0, bytes.Length))
            {
                Assert.AreEqual(input[idx], output[idx]);
            }
        }
    }
}