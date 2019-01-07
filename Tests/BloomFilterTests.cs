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
    public partial class BloomFilterTests
    {
        private static IEnumerable<string> dataSet = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven" };
        private static object[] testcases =
        {
            new object[] { true, "one", dataSet },
            new object[] { true, "two", dataSet },
            new object[] { true, "three", dataSet },
            new object[] { true, "four", dataSet },
            new object[] { true, "five", dataSet },
            new object[] { true, "six", dataSet },
            new object[] { true, "seven", dataSet },
            new object[] { true, "eight", dataSet },
            new object[] { true, "nine", dataSet },
            new object[] { true, "ten", dataSet },
            new object[] { true, "eleven", dataSet },
            new object[] { false, "seventeen", dataSet },
            new object[] { false, "", dataSet }
        };
        private BloomFilter filter = null;

        [TestCaseSource("testcases")]
        public void BloomFilter_ContainsReturnsExpectedResult(bool expectedResult, string lookupValue, IEnumerable<string> dataSet)
        {
            filter = new BloomFilter(dataSet);
            Assert.AreEqual(expectedResult, filter.Contains(lookupValue));
        }

        [Test]
        public void BloomFilter_VisualizeBitmapShouldReturnCorrectString()
        {
            var filter = new BloomFilter(dataSet, 100);
            var result = filter.VisualizeBitmap();
            //var expected = "0000010000101000000000001001001000000000000001000100000000000000000000000000001000000000000000000001"; //one hash ((string.gethashcode)
            //var expected = "1000010000101001000110101001001000000000100001001100000000000010000000010000001000000000000000000011"; //two hash (string.gethashcode and md5)
            var expected = "1000010010101001010111111001001000000000100001001110000000001011010000010000001000000000000001001011"; //three hash (string.gethashcode, md5 and fnv1a)
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void BloomFilter_OptimumNumberOfHashFunctionsShouldReturnCorrectValue()
        {
            var bitmapSize = BloomFilter.OptimumBitmapSize(dataSet.Count());
            var result = BloomFilter.OptimumNumberOfHashFunctions(bitmapSize, dataSet.Count());
            var expected = 7; //wouldn't expect this many
            Assert.AreEqual(expected, result);

            var bitmapSizeBig = BloomFilter.OptimumBitmapSize(216553);
            result = BloomFilter.OptimumNumberOfHashFunctions(bitmapSizeBig, 216553);
            expected = 7;
            Assert.AreEqual(expected, result);
        }

    }
}
