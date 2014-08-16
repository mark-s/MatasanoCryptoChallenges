using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SetOne.Two;

namespace SetOne.Three
{


    public interface ISingleByteXORCipher
    {
        string GetUnencryptedText(byte[] secretHexBytes);

        int ScoreCharacters(string teststring);
    }


    public class SingleByteXORCipher : ISingleByteXORCipher
    {


        private const string CHARS_TO_USE = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 1234567890!@#$%^&*(){}[]:; |\"\\/";

        private readonly IFixedXOR _fixedXOR;

        private readonly List<char> _goodCharacters = new List<char> { 'e', 't', 'a', 'o', 'i', 'n' };
        private readonly List<char> _badCharacters = new List<char> { 'p', 'b', 'v', 'k' };
        private readonly List<char> _veryBadCharacters = new List<char> { 'j', 'x', 'q', 'z' };


        public SingleByteXORCipher(IFixedXOR fixedXOR)
        {
            _fixedXOR = fixedXOR;
        }




        public string GetUnencryptedText(byte[] secretHexBytes)
        {
            var tests = new Dictionary<string, int>();

            foreach (var charByte in CHARS_TO_USE.ToCharArray().Select(c => BitConverter.GetBytes(c)[0]))
            {
                var sb = new StringBuilder();
                foreach (var hexByte in secretHexBytes)
                {
                    var num = int.Parse(_fixedXOR.GetFixedXOR(charByte, hexByte), NumberStyles.AllowHexSpecifier);
                    var asChar = (char)num;
                    sb.Append(Regex.Replace((asChar).ToString(), @"[^\u0000-\u007F]", "^"));
                }
                var asString = sb.ToString();
                if (tests.ContainsKey(asString) == false)
                    tests.Add(asString, ScoreCharacters(asString.ToLowerInvariant()));
            }
            var maxVal = tests.Max(i => i.Value);
            return tests.FirstOrDefault(v => v.Value == maxVal).Key;

        }


        public int ScoreCharacters(string teststring)
        {
            int finalScore = 0;

            if (teststring.IndexOf('^') != -1)
                return Int32.MinValue;

            foreach (char badCharacter in _badCharacters)
            {
                foreach (char c in teststring)
                    if (c == badCharacter)
                        finalScore -= 1;
            }

            foreach (char veryBadCharacter in _veryBadCharacters)
            {
                foreach (char c in teststring)
                    if (c == veryBadCharacter)
                        finalScore -= 2;
            }
            foreach (char goodCharacter in _goodCharacters)
            {
                foreach (char c in teststring)
                    if (c == goodCharacter)
                        finalScore += 5;
            }

            return finalScore;
        }





    }
}
