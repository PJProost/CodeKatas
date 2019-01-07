using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas
{
    class Program
    {
        static void Main(string[] args)
        {
            //BloomFilter();

            //Shift();

            //ShowBytes();

            Console.WriteLine("Enter key to exit");
            Console.ReadLine();
        }

        private static void ShowBytes()
        {
            for (int i = 0; i < 20; i++)
            {
                var octet = i;
                Console.WriteLine($"Decimal: {octet}");
                Console.WriteLine($"Hexadec: {Convert.ToString(octet, 16).PadLeft(2, '0')}");
                //Console.WriteLine($"Hex: {string.Format("{0:x2}", octet)}");
                Console.WriteLine($"Binary:  {Convert.ToString(octet, 2).PadLeft(8, '0')}");
                Console.WriteLine();
            }
        }

        private static void Shift()
        {
            var value = "this is a test string";
            var block_no = 1;
            var byte1 = value[(block_no * 4) + 0];
            var byte2 = value[(block_no * 4) + 1];// << 8;
            var byte3 = value[(block_no * 4) + 2];// << 16;
            var byte4 = value[(block_no * 4) + 3];// << 24;


            Console.WriteLine($"byte 1: '{byte1}'");
            Console.WriteLine($"\tbinary: {Convert.ToString(byte1, 2).PadLeft(8, '0')}");
            Console.WriteLine($"byte 2: '{byte2}'");
            Console.WriteLine($"\tbinary: {Convert.ToString(byte2, 2).PadLeft(8, '0')}");
            Console.WriteLine($"byte 3: '{byte3}'");
            Console.WriteLine($"\tbinary: {Convert.ToString(byte3, 2).PadLeft(8, '0')}");
            Console.WriteLine($"byte 4: '{byte4}'");
            Console.WriteLine($"\tbinary: {Convert.ToString(byte4, 2).PadLeft(8, '0')}");
        }

        private static void BloomFilter()
        {
            const int numberOfWords = 1000;
            var lines = System.IO.File.ReadAllLines("wordlist.txt");
            var falseHits = 0;
            var percentage = (decimal)0;
            //var 
            var filter = new BloomFilter(lines);
            var randWord = new RandomWord();
            var rand = new Random();
            Console.WriteLine($"Words in collection: {lines.Length}\tRandom words checked: {"0",-6} False hits: {falseHits,-6} ({percentage}:P)");
            for (decimal i = 0; i < numberOfWords; i++)
            {
                percentage = i / numberOfWords;
                Console.WriteLine($"Progress: {percentage:P}");
                var word = randWord.Next(rand.Next(5, 8));
                Console.WriteLine($"Checking word: {word}");
                var inCollectionBloom = filter.Contains(word);
                var inCollectionFwork = lines.Contains(word);
                if (inCollectionBloom != inCollectionFwork)
                {
                    falseHits += 1;
                }
                percentage = falseHits / (i + 1);
                Console.WriteLine($"Words in collection: {lines.Length}\tRandom words checked: {i + 1,-6} False hits: {falseHits,-6} ({percentage:P})");
            }
        }
    }
}
