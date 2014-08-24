using System;
using System.Collections.Generic;
using System.Linq;
using MatasantoCrypto.Set1.SharedCore;
using MatasantoCrypto.Set1.Two;

namespace MatasantoCrypto.Set1.Three
{
    public interface ISingleByteXORCipher
    {
        ResultItem GetUnencryptedText(byte[] bytes);
        int ScoreString(string testString);
        string GetBestScoringString(List<string> strings);

    }

    public class SingleByteXORCipher : ISingleByteXORCipher
    {
        private readonly IFixedXOR _fixedXOR;

        private readonly Dictionary<char, int> _scores;

        public SingleByteXORCipher(IFixedXOR fixedXOR)
        {
            _fixedXOR = fixedXOR;
            _scores = ConstructSoreingDictionary();
        }

        public ResultItem GetUnencryptedText(byte[] bytes)
        {
            var tests = new List<ResultItem>();

            foreach (byte keyByte in Enumerable.Range(0, 128).Select(c => BitConverter.GetBytes((char)c)[0]))
            {
                var text = "";

                foreach (byte hexByte in bytes)
                {
                    var num = _fixedXOR.ByteXOR(keyByte, hexByte);
                    text += Convert.ToChar(num);
                }

                var score = ScoreString(text);

                tests.Add(new ResultItem
                              {
                                  KeyChar = Convert.ToChar(keyByte),
                                  KeyByte = keyByte,
                                  Text = text,
                                  Score = score
                              });
            }

            return tests.OrderByDescending(i => i.Score).First();
        }

        public string GetBestScoringString(List<string> strings)
        {
            return strings.Select(testString => new { testString, score = ScoreString(testString) })
                .Select(t => new ResultItem { Score = t.score, Text = t.testString })
                .OrderByDescending(s => s.Score).Select(i => i.Text)
                        .First();
        }

        public int ScoreString(string testString)
        {
            return testString.Sum(c => ScoreCharacter(c));
        }

        private int ScoreCharacter(char character)
        {
            int score;
            return _scores.TryGetValue(character, out score) ? score : -0;
        }

        private Dictionary<char, int> ConstructSoreingDictionary()
        {
            // Inspired by:
            // Case-sensitive letter and bigram frequency counts from large-scale English corpora
            // http://dx.doi.org/10.3758/BF03195586

            return new Dictionary<char, int>
                           {
                               { ' ', 213 },     { '\n', 100 }, { '\r', 100 },
                               { 'e', 226 },    { 'T', 126 },
                               { 't', 225 },     { 'S', 125 },
                               { 'a', 224 },    { 'A', 124 }, 
                               { 'o', 223 },    { 'M', 123 }, 
                               { 'i', 222 },     { 'C', 122 },
                               { 'n', 221 },    { 'I', 121 }, 
                               { 's', 220 },    { 'N', 120 }, 
                               { 'r', 219 },     { 'B', 119 }, 
                               { 'h', 218 },    { 'R', 118 }, 
                               { 'l', 217 },     { 'P', 117 }, 
                               { 'd', 216 },    { 'E', 116 }, 
                               { 'c', 215 },    { 'D', 115 }, 
                               { 'u', 214 },    { 'H', 114 }, 
                               { 'm', 213 },   { 'W', 113 }, 
                               { 'f', 212 },     { 'L', 112 }, 
                               { 'p', 211 },    { 'O', 111 }, 
                               { 'g', 210 },    { 'F', 110 },
                               { 'w', 209 },     { 'Y', 109 }, 
                               { 'y', 208 },      { 'G', 108 }, 
                               { 'b', 207 },      { 'J', 107 }, 
                               { 'v', 206 },      { 'U', 106 }, 
                               { 'k', 205 },      { 'K', 105 }, 
                               { 'x', 204 },      { 'V', 104 }, 
                               { 'j', 203 },       { 'Q', 103 }, 
                               { 'q', 202 },      { 'X', 102 }, 
                               { 'z', 201 },      { 'Z', 101 },
                               {',',30},{'.',29},
                                {'0',28},{'1',27},
                                {'5',26},{'2',25},
                                {'9',24},{'-',23},
                                {'\'',22},{'4',21},
                                {'3',20},{'8',19},
                                {'6',18},{'7',17},
                                {':',16},{')',15},
                                {'(',14},{'$',13},
                                {';',12},{'*',11},
                                {'?',10},{'/',9},
                                {'&',8},{'!',7},
                                {'%',6},{'+',5},
                                {'>',4},{'<',3},
                                {'=',2},{'}',2},
                                {'{',2},{'@',1}
                           };
        }
    }
}