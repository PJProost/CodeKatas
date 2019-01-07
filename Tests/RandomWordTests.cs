using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CodeKatas;

namespace Tests
{
    [TestFixture]
    class RandomWordTests
    {
        [Test]
        public void RandomWordShouldBeStringOfCorrectLength()
        {
            var obj = "string";
            var expected = obj.GetType();
            var randomWord = new RandomWord();
            var word = randomWord.Next(6);
            var result = word.GetType();
            Assert.IsTrue(expected == result && obj.Length == word.Length);
        }
    }
}
