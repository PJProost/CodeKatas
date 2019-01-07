using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas
{
    public class BloomFilter
    {
        private bool[] _bitmap = null;

        public BloomFilter(IEnumerable<string> dataSet, int size = 0)
        {
            if (size == 0)
            {
                size = OptimumBitmapSize(dataSet.Count());
            }
            DataSetToBitmap(dataSet, size);
        }

        private void DataSetToBitmap(IEnumerable<string> dataSet, int size)
        {
            _bitmap = new bool[size]; //default values false
            foreach (var data in dataSet)
            {
                Add(data);
            }
        }

        private void Add(string data)
        { //to make this public there should be a way to increase the bitmap size if required
            //to avoid collision multiple hashes should be calculated and stored:
            _bitmap[BitmapHashIndex(data)] = true;
            _bitmap[BitmapHashIndexMD5(data)] = true;
            _bitmap[BitmapHashIndexFNV1A(data)] = true;
            //7 hashes seems to be common, see also the OptimumNumberOfHashFunctions method
        }

        public bool Contains(string lookupValue)
        {
            //to avoid collision multiple hashes should be checked:
            return _bitmap[BitmapHashIndex(lookupValue)]
                && _bitmap[BitmapHashIndexMD5(lookupValue)]
                && _bitmap[BitmapHashIndexFNV1A(lookupValue)];
        }

        private int BitmapHashIndex(string data)
        {
            var hash = data.GetHashCode();
            var absHash = Math.Abs(hash); //fix negative hashes
            return FitHashToIndex(absHash);
        }
        private int BitmapHashIndexMD5(string data)
        {
            var md5 = MD5.Create();

            //var dataChars = data.ToCharArray();
            //var dataBytes = new byte[dataChars.Length];
            //for (int i = 0; i < dataChars.Length; i++)
            //{
            //    dataBytes[i] = Convert.ToByte(dataChars[i]); //this doesn't work for some special characters which take more than one byte to store
            //}

            var dataBytes = Encoding.ASCII.GetBytes(data);

            var hashBytes = md5.ComputeHash(dataBytes);
            var hash = -1;
            foreach (var item in hashBytes)
            {
                hash += Convert.ToInt32(item);
            }
            //the hash functions used in a bloom filter should be independent and uniformly distributed
            //-> not sure if this fits the bill. either way this might not be a fast enough hash code for Prod code. in general cryptographic hashes should be avoided for bloom filters
            return FitHashToIndex(hash);
        }
        private int BitmapHashIndexFNV1A(string data)
        {
            var hash = checked((long)FNVHash.FNV1aIn32bit(data));
            return FitHashToIndex(hash);
        }
        private int FitHashToIndex(long hash)
        {
            var index = hash % _bitmap.Length;
            return (int)index;
        }

        public string VisualizeBitmap()
        {
            var result = "";
            foreach (var bit in _bitmap)
            {
                var vis = "0";
                if (bit)
                {
                    vis = "1";
                }
                result += vis;
            }
            return result;
        }
        
        public static int OptimumBitmapSize(int numberOfElements, double maxFalsePositiveRate = 0.01)
        {
            var result = ((numberOfElements * -1 * Math.Log(maxFalsePositiveRate)) / Math.Pow(Math.Log(2), 2));
            return (int)Math.Ceiling(result);
        }
        public static int OptimumNumberOfHashFunctions(int bitmapSize, int numberOfElements)
        {
            var result = (bitmapSize / numberOfElements) * Math.Log(2);
            return (int)Math.Ceiling(result);
        }
    }
}
