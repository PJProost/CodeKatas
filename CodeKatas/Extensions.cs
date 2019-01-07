using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas
{
    public static class Extensions
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null) return null;
            else
            {
                var bf = new BinaryFormatter();
                using (var ms = new System.IO.MemoryStream())
                {
                    //serialize object to stream
                    bf.Serialize(ms, obj);
                    //return bytes from stream
                    return ms.ToArray();
                }
            }
        }
        public static T FromByteArrayTo<T>(this byte[] octets)
        {
            if (octets == null) return default(T);
            else
            {
                var bf = new BinaryFormatter();
                using (var ms = new System.IO.MemoryStream())
                {
                    //write bytes to stream
                    ms.Write(octets, 0, octets.Length);
                    //reset position
                    ms.Position = 0;
                    //deserialize stream to object
                    return (T)bf.Deserialize(ms);
                }
            }
        }

        public static uint RotateLeft(this uint value, int count)
        {
            return (value << count) | (value >> (32 - count));
        }
        public static uint RotateRight(this uint value, int count)
        {
            return (value >> count) | (value << (32 - count));
        }

        public static string ToHex(this byte[] inputBytes)
        {
            return string.Join(null, inputBytes.Select(octet => Convert.ToString(octet, 16).PadLeft(2, '0'))); //hex = base16
        }
        public static byte[] HexToBytes(this string inputHex)
        {
            if (inputHex.Length % 2 > 0)
                throw new FormatException("Hex string cannot have an odd number of characters");
            var result = new byte[inputHex.Length / 2];
            for (int i = 0; i < inputHex.Length; i += 2)
            {
                var hex = inputHex.Substring(i, 2);
                result[i / 2] = Convert.ToByte(hex, 16); //hex = base16
            }
            return result;
        }

        public static string BitString(this BitArray input)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (i > 0 && i % 8 == 0) builder.Append(" ");
                var bitString = "0";
                if (input[i] == true) bitString = "1";
                builder.Append(bitString);
            }
            return builder.ToString();
        }
        public static BitArray Append(this BitArray array, BitArray other)
        {
            var result = new BitArray(array.Length + other.Length);
            //looping twice required because both arrays don't necessarily have the same length
            for (int i = 0; i < array.Length; i++)
            {
                result.Set(i, array[i]);
            }
            for (int i = array.Length; i < result.Length; i++)
            {
                result.Set(i, other[i - array.Length]);
            }
            return result;
        }
        public static BitArray RoundToWholeByte(this BitArray bits)
        {
            //add zeroes at the end
            //untill the length of the array equals x bytes
            //where x is divisable by 8
            var workBits = (BitArray)bits.Clone(); //prevent modifying original array by reference, BitVector32 would have been a better design decision
            if (workBits.Length % 8 > 0)
            {
                //this was for adding in the beginning:
                //workBits.Length += 1;
                //workBits.LeftShift(1);
                //now adding in the end:
                workBits.Length++;
                return workBits.RoundToWholeByte();
            }
            else
            {
                return workBits;
            }
        }
    }
}
