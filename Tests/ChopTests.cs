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
    public class ChopTests
    {
        static object[] testcases =
        {
            new object[] { -1, 3, new int[] { 1 } },
            new object[] {-1, 3, new int[] { 1 } },
            new object[] { 0, 1, new int[] { 1 } },
            new object[] { -1, 50, new int[] { 1, 100 } },
            new object[] { 1, 2, new int[] { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20 } },
            new object[] { 0, 1, new int[] { 1, 3, 5 } },
            new object[] { 1, 3, new int[] { 1, 3, 5 } },
            new object[] { 2, 5, new int[] { 1, 3, 5 } },
            new object[] { -1, 0, new int[] { 1, 3, 5 } },
            new object[] { -1, 2, new int[] { 1, 3, 5 } },
            new object[] { -1, 4, new int[] { 1, 3, 5 } },
            new object[] { -1, 6, new int[] { 1, 3, 5 } },
            new object[] { 0, 1, new int[] { 1, 3, 5, 7 } },
            new object[] { 1, 3, new int[] { 1, 3, 5, 7 } },
            new object[] { 2, 5, new int[] { 1, 3, 5, 7 } },
            new object[] { 3, 7, new int[] { 1, 3, 5, 7 } },
            new object[] { -1, 0, new int[] { 1, 3, 5, 7 } },
            new object[] { -1, 2, new int[] { 1, 3, 5, 7 } },
            new object[] { -1, 4, new int[] { 1, 3, 5, 7 } },
            new object[] { -1, 6, new int[] { 1, 3, 5, 7 } },
            new object[] { -1, 8, new int[] { 1, 3, 5, 7 } }
        };

        [TestCaseSource("testcases")]
        public void ChopFrameworkShouldReturnCorrectValue(int expected, int find, int[] inSortedArray)
        {
            Assert.AreEqual(expected, Chop.ChopFramework(find, inSortedArray));
        }

        //[TestCaseSource("testcases")]
        public void ChopLoopShouldReturnCorrectValue(int expected, int find, int[] inSortedArray)
        {
            Assert.AreEqual(expected, Chop.ChopLoop(find, inSortedArray));
        }

        [TestCaseSource("testcases")]
        public void ChopIndexesShouldReturnCorrectValue(int expected, int find, int[] inSortedArray)
        {
            Assert.AreEqual(expected, Chop.ChopIndexes(find, inSortedArray));
        }

        [TestCase(1, 2, 1)]
        [TestCase(1, 4, 2)]
        [TestCase(1, 5, 3)]
        [TestCase(2, 2, 2)]
        public void AverageShouldReturnExpectedValue(int min, int max, int expected)
        {
            Assert.AreEqual(expected, Chop.Average(min, max));
        }
    }
}
