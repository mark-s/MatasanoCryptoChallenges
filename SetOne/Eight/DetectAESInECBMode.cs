using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MatasantoCrypto.Set1.Eight
{
    public class DetectAESInECBMode
    {

        public string GetEncryptedStrings(string fileName)
        {
            var cypherTextFromFile = LoadFileToString(fileName);

            var result = "";

            for (int keyLength = 2; keyLength < 1024; keyLength *= 2)
            {
                var keyLengthChunks = GetChunks(cypherTextFromFile, keyLength);

                // get the duplicates
                var duplicates = keyLengthChunks.GroupBy(s => s).SelectMany(grp => grp.Skip(1));

                var count = duplicates.Distinct().Count();
                if (count == 1)
                {
                    result = duplicates.FirstOrDefault();
                    break;
                }
            }


            return result;
        }


        private string LoadFileToString(string filename)
        {
            var lines = File.ReadAllLines(filename);
            return String.Join(String.Empty, lines);
        }




        private List<string> GetChunks(string text, int chunkSize)
        {
            var sections = new List<string>();

            var counter = 0;

            while (counter < text.Length)
            {
                var chunk = text.Substring(counter, chunkSize);
                sections.Add(chunk);
                counter += chunk.Length;
            }

            return sections;
        }


    }
}
