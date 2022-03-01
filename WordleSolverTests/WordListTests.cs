using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using WordleSolver.Data;

namespace WordleSolverTests
{
    [TestClass]
    public class WordListTests
    {

        [TestMethod]
        public void ReadWords()
        {
            Assert.AreEqual("ABACK", WordList.Answers.First().ToString());
            Assert.AreEqual(2315, WordList.Answers.Length);
            Assert.AreEqual("ZONAL", WordList.Answers.Last().ToString());
        }
    }
}
