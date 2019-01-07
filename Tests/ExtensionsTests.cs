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
    class ExtensionsTests
    {
        [Test]
        public void ObjectToByteArrayAndReverseShouldBeEqual()
        {
            var obj = "test string";

            var bytes = obj.ToByteArray();
            var obj2 = bytes.FromByteArrayTo<string>();

            Assert.AreEqual(obj, obj2);
        }
    }
}
