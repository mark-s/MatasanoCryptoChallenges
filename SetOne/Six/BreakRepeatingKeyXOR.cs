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
        private readonly ConvertHex _convertor;

        public BreakRepeatingKeyXOR(IGetHammingDistance hammingDistance, ISingleByteXORCipher xorCipher, IFixedXOR fixedXOR)
        {
            _hammingDistance = hammingDistance;
            _xorCipher = xorCipher;
            _fixedXOR = fixedXOR;
            _convertor = new ConvertHex();
        }



        public string Decrypt(string base64EncodedFileName, int minKeysize, int maxKeysize)
        {

            byte[] byteArray = LoadFileToByteArray(base64EncodedFileName);

            //var probableKeySize = 4;//GetProbableKeySize(byteArray, minKeysize, maxKeysize);

            List<string> reeee = new List<string>();
            for (int i = 2; i < 40; i++)
            {
                reeee.Add(GetResultForKeysize(byteArray, i));
            }

            var result = (from rrr in reeee let score = _xorCipher.ScoreCharacters(rrr) select new ResultItem() {Score = score, Text = rrr})
                .OrderByDescending(s => s.Score).Select(i => i.Text)
                .First();


            return result;
            //  return GetResultForKeysize(byteArray, probableKeySize);
        }

        private string GetResultForKeysize(byte[] byteArray, int probableKeySizes)
        {
            // Now that you probably know the KEYSIZE: break the ciphertext into blocks of KEYSIZE length.  
            var keySizedPieces = GetChunks(byteArray, probableKeySizes);

            // Now transpose the blocks: make a block that is the first byte of every block
            // and a block that is the second byte of every block, and so on
            List<byte[]> transposed = TransposePieces(keySizedPieces, probableKeySizes);

            // Solve each block as if it was single-character XOR. You already have code to do this. 
            List<ResultItem> perBlockResults = transposed.Select(thisBlock => _xorCipher.GetUnencryptedText(thisBlock)).ToList();

            // For each block, the single-byte XOR key that produces the best looking histogram is the
            // repeating-key XOR key byte for that block. Put them together and you have the key.
            var key = perBlockResults.Aggregate("", (current, t) => current + Convert.ToChar(t.KeyByte));

            var enumerableRepeatingKey = Encoding.UTF8.GetBytes(key.Repeat(byteArray.Length));

            var res = DecodeForOneLine(byteArray, enumerableRepeatingKey);

            return Encoding.UTF8.GetString(res.ToArray());
        }


        private int GetProbableKeySize(byte[] byteArray, int minKeysize, int maxKeysize)
        {
            //The KEYSIZE with the smallest normalized edit distance is probably the key. 
            foreach (var keysize in Enumerable.Range(minKeysize, ((maxKeysize - minKeysize) + 1)))
                _keysizeAndDistances[keysize] = GetNormalisedDistance(byteArray, keysize);

            return _keysizeAndDistances.OrderBy(i => i.Value).Select(v => v.Value).FirstOrDefault();
        }


        public int GetNormalisedDistance(byte[] data, int keysize)
        {

            // TODO:
            //You could proceed perhaps with the smallest 2-3 KEYSIZE values. 
            //Or take 4 KEYSIZE blocks instead of 2 and average the distances. 

            //For each KEYSIZE, take the first KEYSIZE worth of bytes, 
            // and the second KEYSIZE worth of bytes
            List<byte[]> chunks = GetChunks(data, keysize).Take(4).ToList();

            // and find the edit distance between them. 
            var editDistance1 = _hammingDistance.GetHammingDistance(chunks[0], chunks[1]);
            var editDistance2 = _hammingDistance.GetHammingDistance(chunks[0], chunks[2]);
            var editDistance3 = _hammingDistance.GetHammingDistance(chunks[0], chunks[3]);

            var editDistance4 = _hammingDistance.GetHammingDistance(chunks[1], chunks[0]);
            var editDistance5 = _hammingDistance.GetHammingDistance(chunks[1], chunks[2]);
            var editDistance6 = _hammingDistance.GetHammingDistance(chunks[1], chunks[3]);


            var editDistance7 = _hammingDistance.GetHammingDistance(chunks[2], chunks[0]);
            var editDistance8 = _hammingDistance.GetHammingDistance(chunks[2], chunks[1]);
            var editDistance9 = _hammingDistance.GetHammingDistance(chunks[2], chunks[3]);

            var editDistance10 = _hammingDistance.GetHammingDistance(chunks[3], chunks[0]);
            var editDistance11 = _hammingDistance.GetHammingDistance(chunks[3], chunks[1]);
            var editDistance12 = _hammingDistance.GetHammingDistance(chunks[3], chunks[2]);



            var sum = editDistance1 + editDistance2 + editDistance3 + editDistance4 + editDistance5 + editDistance6 +
                      editDistance7 + editDistance8 + editDistance9 + editDistance10 + editDistance11
                      + editDistance12;

            return (sum / 12) / keysize;

            // Normalize this result by dividing by KEYSIZE.
            //return (editDistance1 / keysize);

        }






        private byte[] LoadFileToByteArray(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var asOneLine = String.Join(String.Empty, lines);

            return _convertor.GetByteArrayFromBase64(asOneLine);
        }
        public List<byte> DecodeForOneLine(byte[] lineBytes, byte[] encryptionKeyBytes)
        {
            var toReturn = new List<byte>();
            for (int j = 0; j < lineBytes.Length; j++)
            {
                toReturn.Add(_fixedXOR.ByteXOR(lineBytes[j], encryptionKeyBytes[j]));
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

                    if (piece.GetUpperBound(0) + 1 <= i)
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

    }
}
