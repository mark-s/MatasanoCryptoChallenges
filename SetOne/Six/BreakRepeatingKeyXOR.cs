using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MatasantoCrypto.Set1.One;
using MatasantoCrypto.Set1.SharedCore;
using MatasantoCrypto.Set1.Three;
using MatasantoCrypto.Set1.Two;

namespace MatasantoCrypto.Set1.Six
{
    public class BreakRepeatingKeyXOR
    {
        private readonly IGetHammingDistance _hammingDistance;
        private readonly ISingleByteXORCipher _xorCipher;
        private readonly IFixedXOR _fixedXOR;

        // key size - hamming distance
        private readonly Dictionary<int, int> _keysizeAndDistances = new Dictionary<int, int>();
        private ConvertHex _convertor;

        public BreakRepeatingKeyXOR(IGetHammingDistance hammingDistance,
                                                     ISingleByteXORCipher xorCipher, IFixedXOR fixedXOR)
        {
            _hammingDistance = hammingDistance;
            _xorCipher = xorCipher;
            _fixedXOR = fixedXOR;
            _convertor = new ConvertHex();
        }

        public void Decrypt(string base64EncodedFileName, int minKeysize, int maxKeysize)
        {

            var byteArray =  LoadFileToByteArray(base64EncodedFileName);

            var probableKeySize = GetProbableKeySize(byteArray, minKeysize, maxKeysize);

            // Now that you probably know the KEYSIZE: break the ciphertext into blocks of KEYSIZE length.  
            var keySizedPieces = GetChunks(byteArray, probableKeySize);

            // Now transpose the blocks: make a block that is the first byte of every block
            // and a block that is the second byte of every block, and so on
            List<byte[]> transposed = TransposePieces(keySizedPieces, probableKeySize);

            // Solve each block as if it was single-character XOR. You already have code to do this. 
            var blockResults = new List<ResultItem>();
            foreach (byte[] thisBlock in transposed)
            {
                var bestResultForBlock = _xorCipher.GetUnencryptedText(thisBlock);
                blockResults.Add(bestResultForBlock);
            }
            
            // For each block, the single-byte XOR key that produces the best looking histogram is the
            // repeating-key XOR key byte for that block. Put them together and you have the key.
            byte[] joinedKey = new byte[blockResults.Count];
            for (int i = 0; i < blockResults.Count; i++)
            {
                joinedKey[i] = (byte)blockResults[i].KeyChar;
            }
            string key = "";
            foreach (byte b in joinedKey)
            {
                key += Convert.ToChar(b);
            }


            var enumerableRepeatingKey = Encoding.UTF8.GetBytes(key.Repeat(byteArray.Length));
            var res = DecodeForOneLine(byteArray, enumerableRepeatingKey);



        }



        public string DecodeForOneLine(byte[] lineBytes, byte[] encryptionKeyBytes)
        {
            string toReturn = "";

            for (int j = 0; j < lineBytes.Length; j++)
            {
                toReturn += _fixedXOR.XOR(lineBytes[j], encryptionKeyBytes[j]);
            }

            return toReturn;
        }

        private List<byte[]> TransposePieces(List<byte[]> keySizedPieces, int keysize)
        {
            // Now transpose the blocks: make a block that is the first byte of every block
            // and a block that is the second byte of every block, and so on
            var toReturn = new List<byte[]>();

            for (int i = 0; i < keysize; i++)
            {
                var temp = new List<byte>();
                foreach (var piece in keySizedPieces)
                {

                    if(piece.GetUpperBound(0) +1 <= i)
                        continue;
                    
                    temp.Add(piece[i]);
                }

                toReturn.Add(temp.ToArray());
            }

            return toReturn;
        }

        public List<byte[]> GetChunks(byte[] array, int chunkSize)
        {
            var sections = new List<byte[]>();
            var counter = 0;

            while (counter < array.Length)
            {
                var chunk = array.Skip(counter).Take(chunkSize).ToArray();
                sections.Add(chunk);
                counter += chunk.Length;
            }
            return sections;
        }

        private int GetProbableKeySize(byte[] byteArray, int minKeysize, int maxKeysize)
        {
            //The KEYSIZE with the smallest normalized edit distance is probably the key. 

            // TODO:
            //You could proceed perhaps with the smallest 2-3 KEYSIZE values. 
            //Or take 4 KEYSIZE blocks instead of 2 and average the distances. 

            foreach (var keysize in Enumerable.Range(minKeysize, ((maxKeysize - minKeysize)+1)))
                _keysizeAndDistances[keysize] = GetNormalisedDistance(byteArray, keysize);

            var minvalue = _keysizeAndDistances.Min(v => v.Value);
            return _keysizeAndDistances.FirstOrDefault(k => k.Value == minvalue).Key;

        }

        private byte[] LoadFileToByteArray(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var asOneLine = String.Join("", lines);


            // is this bytes -> HEX -> Base64 ?
            // or
            // is this bytes -> Base64 ?
            var bytes =  _convertor.GetByteArrayFromBase64(asOneLine);
            var hex = _convertor.ByteArrayToHexString(bytes);
            var finalBytes = _convertor.HexStringToByteArray(hex);

            return finalBytes;
        }

        public int GetNormalisedDistance(byte[] data, int keysize)
        {
            //For each KEYSIZE, take the first KEYSIZE worth of bytes, 
            // and the second KEYSIZE worth of bytes
            List<byte[]> chunks = GetChunks(data, keysize).Take(2).ToList();

            // and find the edit distance between them. 
            var editDistance1 = _hammingDistance.GetHammingDistance(chunks[0], chunks[1]);

            // Normalize this result by dividing by KEYSIZE.
            return (editDistance1 / keysize);

        }

    }
}
