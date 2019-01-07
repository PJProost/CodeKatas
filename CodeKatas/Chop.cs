using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas
{
    public class Chop
    {
        public static int ChopFramework(int find, int[] inSortedArray)
        { //use framework
            return Array.FindIndex<int>(inSortedArray, i => i == find);
        }

        public static int ChopLoop(int find, int[] inSortedArray)
        { //loop all until found
            var result = -1;
            for (int i = 0; i < inSortedArray.Length; i++)
            {
                if (find == inSortedArray[i])
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        public static int ChopIndexes(int find, int[] inSortedArray)
        { //use array indexes to chop
            var result = -1;
            var goOn = true;
            var min = 0;
            var max = inSortedArray.Length - 1;
            while (goOn == true && min <= max)
            {
                var average = Average(min, max);
                if (find == inSortedArray[average])
                {
                    result = Average(min, max);
                    goOn = false;
                    //find is in middle
                }
                else if (find > inSortedArray[average])
                {
                    min = average + 1;
                    //find is in top half
                }
                else
                {
                    max = average - 1;
                    //find is in bottom half
                }
            }
            return result;
        }

        public static int Average(int min, int max)
        {
            return (min + max) / 2;
        }
    }
}
