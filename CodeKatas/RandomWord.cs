using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas
{
    public class RandomWord
    {
        public RandomWord()
        {
            CharactersToInclude = "abcdefghijklmnopqrstuvwxyz";
        }

        public string Next(int length)
        {
            var result = "";
            var random = new Random();
            for (int i = 0; i < length; i++)
            {
                var index = random.Next(0, CharactersToInclude.Length - 1);
                result += CharactersToInclude.ElementAt(index);
            }
            return result;
        }

        public string CharactersToInclude { get; set; }
    }
}