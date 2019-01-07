using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeKatas;

namespace Tests
{
    [TestFixture]
    public class ControlCharsTests
    {
        [Test]
        public void ControlCharsTest()
        {
            Assert.AreEqual((int)ControlChars.Chars.STX, 0x02);
        }
    }
}
