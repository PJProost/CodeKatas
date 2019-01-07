using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas
{
    public class StringCompressor
    {
        public static string CompressToHex(string stringToCompress)
        {
            var huffmanTree = new HuffmanTree(HexCharactersLowerCase);
            return huffmanTree.Compress(stringToCompress).ToHex();
        }
        public static string CompressToHex(string stringToCompress, HuffmanTree huffmanTree)
        {
            return huffmanTree.Compress(stringToCompress).ToHex();
        }
        public static byte[] CompressToBytes(string stringToCompress, string charsForCompressionTable)
        {
            var huffmanTree = new HuffmanTree(charsForCompressionTable);
            return huffmanTree.Compress(stringToCompress);
        }
        public static byte[] CompressToBytes(string stringToCompress, out HuffmanTree huffmanTree)
        {
            huffmanTree = new HuffmanTree(stringToCompress);
            return huffmanTree.Compress(stringToCompress);
        }
        public static string DecompressFromHex(string hexStringToDecompress)
        {
            var huffmanTree = new HuffmanTree(HexCharactersLowerCase);
            return huffmanTree.Decompress(hexStringToDecompress.HexToBytes());
        }
        public static string DecompressfromHex(string hexStringToDecompress, HuffmanTree huffmanTree)
        {
            return huffmanTree.Decompress(hexStringToDecompress.HexToBytes());
        }
        public static string DecompressFromBytes(byte[] bytes, string charsForCompressionTable)
        {
            var huffmanTree = new HuffmanTree(charsForCompressionTable);
            return huffmanTree.Decompress(bytes);
        }
        public static string DecompressFromBytes(byte[] bytes, HuffmanTree huffmanTree)
        {
            return huffmanTree.Decompress(bytes);
        }

        public static string HexCharactersLowerCase
        {
            get { return "0123456789abcdef"; }
        }

        public class HuffmanTree
        {
            public HuffmanTree(string stringToCompress)
            {
                var leaves = GetCharsWithFrequency(stringToCompress);
                leaves.Add(new HuffmanTree(EOF, 1));
                switch (leaves.Count)
                {
                    case 1:
                        Leaf(leaves[0].Character, leaves[0].Weight);
                        break;
                    case 2:
                        Branch(leaves[0], leaves[1]);
                        break;
                    default:
                        do
                        {
                            var orderedLeaves = leaves.OrderBy(l => l.Weight).ThenBy(l => l.Depth).ThenBy(l => l.Character).ToList();
                            //ordering to make the results consistent between runs so the test doesn't break
                            var branch = new HuffmanTree(orderedLeaves[0], orderedLeaves[1]);
                            leaves.Remove(branch.TreeOne);
                            leaves.Remove(branch.TreeTwo);
                            leaves.Add(branch);
                        }
                        while (leaves.Count > 2);
                        Branch(leaves[0], leaves[1]);
                        break;
                }
            }
            private HuffmanTree(char character, int weight)
            { //leaf node
                Leaf(character, weight);
            }
            private HuffmanTree(HuffmanTree tree1, HuffmanTree tree2)
            { //branch/root node
                Branch(tree1, tree2);
            }

            private List<HuffmanTree> GetCharsWithFrequency(string stringToCompress)
            {
                List<HuffmanTree> charsWithFrequency = new List<HuffmanTree>();
                foreach (var character in stringToCompress.ToCharArray())
                {
                    var weight = 1;
                    var leaf = charsWithFrequency.FindAll(l => l.Character == character);
                    if (leaf.Count == 1)
                    {
                        weight = leaf[0].Weight + 1;
                        charsWithFrequency.Remove(leaf[0]);
                    }
                    charsWithFrequency.Add(new HuffmanTree(character, weight));
                }
                return charsWithFrequency;
            }

            private void Leaf(char character, int weight)
            { //leaf node
                Character = character;
                Weight = weight;
                Depth = 0;
            }
            private void Branch(HuffmanTree tree1, HuffmanTree tree2)
            { //branch/root node
                TreeOne = tree1;
                TreeTwo = tree2;
                Weight = tree1.Weight + tree2.Weight;
                Depth = tree1.Depth + 1;
                if (tree2.Depth >= Depth) Depth = tree2.Depth + 1;
            }

            public Dictionary<char, BitArray> CompressionTable()
            { //TODO improve, lots of repeated code
                var result = new Dictionary<char, BitArray>();
                var root = this;
                var current = this;

                if (Character != 0)
                {
                    throw new InvalidOperationException("You can't build a compression table on leaves of the tree");
                }
                else
                {
                    if (TreeOne != null)
                    {
                        var pathOne = new BitArray(1);
                        pathOne[0] = false; //default value is false so not really required
                        TreeOne.CompressionTable(pathOne).ToList().ForEach(item => result.Add(item.Key, item.Value));
                    }
                    if (TreeTwo != null)
                    {
                        var pathTwo = new BitArray(1);
                        pathTwo[0] = true;
                        TreeTwo.CompressionTable(pathTwo).ToList().ForEach(item => result.Add(item.Key, item.Value));
                    }
                }
                return result;
            }
            private Dictionary<char, BitArray> CompressionTable(BitArray charPath)
            { //TODO improve, lots of repeated code
                var result = new Dictionary<char, BitArray>();

                if (Character != 0)
                {
                    result.Add(Character, charPath);
                }
                else
                {
                    charPath.Length += 1;
                    if (TreeOne != null)
                    {
                        var pathOne = (BitArray)charPath.Clone();
                        pathOne[pathOne.Length - 1] = false; //default value is false so not really required
                        var temp = TreeOne.CompressionTable(pathOne);
                        temp.ToList().ForEach(item => result.Add(item.Key, item.Value));
                    }
                    if (TreeTwo != null)
                    {
                        var pathTwo = (BitArray)charPath.Clone();
                        pathTwo[pathTwo.Length - 1] = true;
                        var temp = TreeTwo.CompressionTable(pathTwo);
                        temp.ToList().ForEach(item => result.Add(item.Key, item.Value));
                    }
                }
                return result;
            }

            public byte[] Compress(string input)
            {
                var compressedBits = new BitArray(0);
                var table = CompressionTable();
                var characters = input.ToCharArray().ToList();
                characters.Add(EOF); //add EOF indicator at the end of the input
                characters.ForEach(c => compressedBits = compressedBits.Append(table[c])); //for each character append the bits to the bitarray
                compressedBits = compressedBits.RoundToWholeByte();
                var temp = compressedBits.BitString();
                byte[] compressedBytes = new byte[compressedBits.Length / 8];
                compressedBits.CopyTo(compressedBytes, 0); //this reverses order
                return compressedBytes;
            }

            public string Decompress(byte[] input)
            {
                var compressedBits = new BitArray(input); //reverses the order of the bits
                var builder = new StringBuilder();
                var root = this;
                var current = this;
                
                foreach (bool bit in compressedBits)
                {
                    if (bit == false) current = current.TreeOne;
                    else current = current.TreeTwo;
                    if (current.Character != 0)
                    {
                        if (current.Character == EOF) break;
                        else
                        {
                            builder.Append(current.Character);
                            current = root;
                        }
                    }
                }
                return builder.ToString();
            }

            public List<char> CharactersInTree()
            {
                var result = new List<char>();
                if (Character != 0) result.Add(Character);
                if (TreeOne != null) result.AddRange(TreeOne.CharactersInTree());
                if (TreeTwo != null) result.AddRange(TreeTwo.CharactersInTree());
                return result;
            }

            public char Character { get; private set; }
            public int Weight { get; private set; }
            public int Depth { get; private set; }
            public HuffmanTree TreeOne { get; private set; }
            public HuffmanTree TreeTwo { get; private set; }

            public override bool Equals(object obj)
            {
                if (obj == null || this.GetType() != obj.GetType()) return false;
                return this.GetHashCode() == obj.GetHashCode();
            }
            public override int GetHashCode()
            {
                //TODO improve, this might have a lot of collission...
                var weighthash = 0;
                var treeonehash = 0;
                var treetwohash = 0;
                if (Weight != 0) weighthash = Weight.GetHashCode();
                if (TreeOne != null) weighthash = TreeOne.GetHashCode();
                if (TreeTwo != null) weighthash = TreeTwo.GetHashCode();
                return Character.GetHashCode() + weighthash + treeonehash + treetwohash;
            }
            public override string ToString()
            {
                if (TreeOne == null)
                {
                    var charString = Character.ToString();
                    if (Character == Char.MaxValue) charString = "EOF";
                    return $"({Depth},{Weight} Char: '{charString}')";
                }
                else
                {
                    return $"({Depth},{Weight} TreeOne: {TreeOne.Depth},{TreeOne.Weight},{TreeOne.Character.ToString()} TreeTwo: {TreeTwo.Depth},{TreeTwo.Weight},{TreeTwo.Character.ToString()})";
                }
            }

            public const char EOF = Char.MaxValue;
        }
    }
}
