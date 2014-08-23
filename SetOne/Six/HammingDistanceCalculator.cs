using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MatasantoCrypto.Set1.Six
{
    public interface IGetHammingDistance
    {
        int GetHammingDistance(string string1, string string2);

        int GetHammingDistance(byte[] bytes1, byte[] bytes2);

        int GetHammingDistance(BitArray bits1, BitArray bits2);
    }

    public class HammingDistanceCalculator : IGetHammingDistance
    {

        public int GetHammingDistance(string string1, string string2)
        {
            var string1AsBits = new BitArray(Encoding.UTF8.GetBytes(string1));
            var string2AsBits = new BitArray(Encoding.UTF8.GetBytes(string2));

            return GetHammingDistance(string2AsBits, string1AsBits);
        }

        public int GetHammingDistance(byte[] bytes1, byte[] bytes2)
        {
            var asBits1 = new BitArray(bytes1);
            var asBits2 = new BitArray(bytes2);

            return GetHammingDistance(asBits1, asBits2);
        }

        public int GetHammingDistance(BitArray bits1, BitArray bits2)
        {
            if (bits1.Length != bits2.Length)
                throw new ArgumentException("arguments must be same length!");

            int diffs = 0;
            for (int i = 0; i < bits1.Length; i++)
            {
                if (bits1[i] != bits2[i])
                    diffs++;
            }
            return diffs;
        }

    }
}
