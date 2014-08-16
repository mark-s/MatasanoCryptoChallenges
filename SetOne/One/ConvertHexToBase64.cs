using System;

namespace SetOne.One
{


    public class ConvertHexToBase64 : IConvertHexToBase64
    {
        public string ConvertToBase64(byte[] hexBytes)
        {
            return Convert.ToBase64String(hexBytes);
        }

        public byte[] GetBytesFromhexString(string hexString)
        {
            var length = hexString.Length;
            var bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            return bytes;
        }
    }
}
