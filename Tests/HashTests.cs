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
    public partial class HashTests
    {
        [TestCase("one", "3123124719")]
        [TestCase("two", "3190065193")]
        [TestCase("test string", "2533107786")]
        [TestCase("longer test string", "1499978845")]
        [TestCase("The quick brown fox jumps over the lazy dog", "76545936")]
        [TestCase("The quick brown fox jumps over the lazy dog.", "3970930714")]
        public void FNV1aHash32ShouldReturnCorrectHash(string input, string result)
        {
            Assert.AreEqual(result, FNVHash.FNV1aIn32bit(input).ToString());
        }

        [TestCase("one", "1064933119")]
        [TestCase("two", "678209053")]
        [TestCase("test string", "4243667524")]
        [TestCase("longer test string", "4211426769")]
        [TestCase("The quick brown fox jumps over the lazy dog", "3922226286")]
        [TestCase("The quick brown fox jumps over the lazy dog.", "1954722052")]
        public void FNV1Hash32ShouldReturnCorrectHash(string input, string result)
        {
            Assert.AreEqual(result, FNVHash.FNV1In32bit(input).ToString());
        }

        [TestCase("one", "1A08AA1921CA5CAF")]
        [TestCase("two", "5714D319447C9709")]
        [TestCase("test string", "986CFBDD8A262FEA")]
        [TestCase("longer test string", "80558BF115E421DD")]
        [TestCase("The quick brown fox jumps over the lazy dog", "F3F9B7F5E7E47110")]
        [TestCase("The quick brown fox jumps over the lazy dog.", "75C4D4D9092C6C5A")]
        public void FNV1aHash64ShouldReturnCorrectHash(string input, string result)
        {
            Assert.AreEqual(ulong.Parse(result, System.Globalization.NumberStyles.HexNumber), FNVHash.FNV1aIn64bit(input));
        }

        [TestCase("one", "D8ADC6186B88367F")]
        [TestCase("two", "D89CAF186B799BBD")]
        [TestCase("test string", "7A48D9CFC5F29084")]
        [TestCase("longer test string", "B2AB10D6C57A4691")]
        [TestCase("The quick brown fox jumps over the lazy dog", "A8B2F3117DE37ACE")]
        [TestCase("The quick brown fox jumps over the lazy dog.", "8B8DD4B8E989AC24")]
        public void FNV1Hash64ShouldReturnCorrectHash(string input, string result)
        {
            Assert.AreEqual(ulong.Parse(result, System.Globalization.NumberStyles.HexNumber), FNVHash.FNV1In64bit(input));
        }
    }
}
