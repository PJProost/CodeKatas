using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas
{
    //make these implement HashAlgorithm as base class to align these with the System.Security hash classes (even though tese are not cryptographic hash functions)

    public class FNVHash
    {
        public static UInt32 FNV1aIn32bit(object obj)
        {
            return FNV1aIn32bit(obj.ToByteArray());
        }
        public static UInt32 FNV1aIn32bit(string stringUTF8)
        {
            return FNV1aIn32bit(Encoding.UTF8.GetBytes(stringUTF8));
        }
        public static UInt32 FNV1aIn32bit(byte[] array)
        {
            var hash = offset32bit;
            foreach (var octet in array)
            {
                hash = hash ^ octet;
                hash = hash * prime32bit;
            }
            return hash;
        }

        public static UInt64 FNV1aIn64bit(object obj)
        {
            return FNV1aIn64bit(obj.ToByteArray());
        }
        public static UInt64 FNV1aIn64bit(string stringUTF8)
        {
            return FNV1aIn64bit(Encoding.UTF8.GetBytes(stringUTF8));
        }
        public static UInt64 FNV1aIn64bit(byte[] array)
        {
            var hash = offset64bit;
            foreach (var octet in array)
            {
                hash = hash ^ octet;
                hash = hash * prime64bit;
            }
            return hash;
        }

        public static UInt32 FNV1In32bit(object obj)
        {
            return FNV1In32bit(obj.ToByteArray());
        }
        public static UInt32 FNV1In32bit(string stringUTF8)
        {
            return FNV1In32bit(Encoding.UTF8.GetBytes(stringUTF8));
        }
        public static UInt32 FNV1In32bit(byte[] array)
        {
            var hash = offset32bit;
            foreach (var octet in array)
            {
                hash = hash * prime32bit;
                hash = hash ^ octet;
            }
            return hash;
        }

        public static UInt64 FNV1In64bit(object obj)
        {
            return FNV1In64bit(obj.ToByteArray());
        }
        public static UInt64 FNV1In64bit(string stringUTF8)
        {
            return FNV1In64bit(Encoding.UTF8.GetBytes(stringUTF8));
        }
        public static UInt64 FNV1In64bit(byte[] array)
        {
            var hash = offset64bit;
            foreach (var octet in array)
            {
                hash = hash * prime64bit;
                hash = hash ^ octet;
            }
            return hash;
        }

        public const UInt32 prime32bit = 16777619;
        public const UInt32 offset32bit = 2166136261;
        public const UInt64 prime64bit = 1099511628211;
        public const UInt64 offset64bit = 14695981039346656037;
    }
}
