using System;

namespace MatasantoCrypto.Set1.Two
{
    public interface IFixedXOR
    {
        string XOR(byte[] charBytes1, byte[] charBytes2);

        string XOR(byte charByte1, byte charByte2);

        byte ByteXOR(byte charByte1, byte charByte2);
    }

    public class FixedXOR : IFixedXOR
    {

        public string XOR(byte[] charBytes1, byte[] charBytes2)
        {
            if (charBytes1.Length != charBytes2.Length)
                throw new ArgumentException("Strings must be equal length");
            
            var toReturn = "";

            for (int i = 0; i < charBytes1.Length; i++)
                toReturn += XOR(charBytes1[i], charBytes2[i]);

            return toReturn;
        }

        public string XOR(byte charByte1, byte charByte2)
        {
            return String.Format("{0:x}", charByte1 ^ charByte2).PadLeft(2, "0"[0]);
        }

        public byte ByteXOR(byte charByte1, byte charByte2)
        {
            return (byte)(charByte1 ^ charByte2);
        }


    }


}
