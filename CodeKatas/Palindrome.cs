using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas
{
    public class Palindrome
    {
        //A palindrome is a word, phrase, number, or other sequence of units that may be read the same way in either direction.
        //radar, level, rotor, kayak
        //Punctuation, capitalization, and spacing are ignored for complete phrases
        //"A man, a plan, a canal - Panama!"
        //"A Toyota's a Toyota"
        //"Rise to vote, sir"
        public static bool IsPalindrome(string input)
        {
            var stringCleaned = CleanString(input);
            return stringCleaned.Equals(ReverseString(stringCleaned));
        }
        public static string CleanString(string input)
        {
            var allowedChars = "abcdefghijklmnopqrstuvwxyz";
            input = input.ToLower();
            var sb = new StringBuilder();
            foreach (var stringChar in input.ToCharArray())
            {
                foreach (var allowedChar in allowedChars.ToCharArray())
                {
                    if (stringChar == allowedChar)
                    {
                        sb.Append(stringChar);
                        break; //no need to check more allowed chars, move on to next string char
                    }
                }
            }
            return sb.ToString();
        }
        public static string ReverseString(string input)
        {
            return new string(input.Reverse().ToArray());
        }
    }
}
