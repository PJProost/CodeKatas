using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CodeKatas;

namespace Tests
{
    public class PalindromeTests
    {
        [TestCase("A man, a plan, a canal - Panama!", "amanaplanacanalpanama")]
        [TestCase("A Toyota's a Toyota", "atoyotasatoyota")]
        [TestCase("Rise to vote, sir", "risetovotesir")]
        public void StringCleanedShouldReturnCleanedString(string input, string expected)
        {
            Assert.AreEqual(Palindrome.CleanString(input), expected);
        }

        [TestCase("abcdef", "fedcba")]
        [TestCase("risetovotesir", "risetovotesir")]
        [TestCase("abcdefghijklmnopqrstuvwxyz", "zyxwvutsrqponmlkjihgfedcba")]
        public void ReverseStringShouldReturnReversedString(string input, string expected)
        {
            Assert.AreEqual(Palindrome.ReverseString(input), expected);
        }
    }
}