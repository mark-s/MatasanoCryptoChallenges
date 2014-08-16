using System.Collections.Generic;
using System.IO;
using System.Linq;
using SetOne.One;
using SetOne.Three;

namespace SetOne.Four
{
    
    public interface IDetectSingleCharacterXOR
    {
        string DetectXOR(string filename);
    }


    public class DetectSingleCharacterXOR : IDetectSingleCharacterXOR
    {

        private readonly ISingleByteXORCipher _xorCipher;
        private readonly ConvertHexToBase64 _convertHexToBase64;

        public DetectSingleCharacterXOR(ISingleByteXORCipher xorCipher, ConvertHexToBase64 convertHexToBase64)
        {
            _xorCipher = xorCipher;
            _convertHexToBase64 = convertHexToBase64;
        }

        public string DetectXOR(string filename)
        {
            var tests = new Dictionary<string, int>();

            foreach (string line in File.ReadAllLines(filename))
            {
                var thisItern = _xorCipher.GetUnencryptedText(_convertHexToBase64.GetBytesFromhexString(line));
                tests.Add(thisItern, _xorCipher.ScoreCharacters(thisItern));
            }

            var maxVal = tests.Max(i => i.Value);
            return tests.FirstOrDefault(v => v.Value == maxVal).Key;

        }

        
    }
}
