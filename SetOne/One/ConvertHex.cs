using System;
using System.Text;

namespace MatasantoCrypto.Set1.One
{

    public interface IConvertHex
    {
        byte[] HexStringToByteArray(string hexEncoded);

        string ByteArrayToBase64String(byte[] bytes);

        string HexStringToString(string hexEncoded);

        string ByteArrayToHexString(byte[] bytes);
    }

    public class ConvertHex : IConvertHex
    {

        public byte[] HexStringToByteArray(string hexEncoded)
        {
            var length = hexEncoded.Length;

            var bytes = new byte[length / 2];

            for (int i = 0; i < length; i += 2)
                bytes[i / 2] = Convert.ToByte(hexEncoded.Substring(i, 2), 16);

            return bytes;
        }


        public string HexStringToString(string hexEncoded)
        {
            var tmp = HexStringToByteArray(hexEncoded);
            return Encoding.UTF8.GetString(tmp);
        }



        public string ByteArrayToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        public string ByteArrayToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public byte[] GetByteArrayFromBase64(string base64Text)
        {
            return Convert.FromBase64String(base64Text);
        }
    }


}
