using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetOne.Two
{


    public interface IFixedXOR
    {
        string GetFixedXOR(byte[] charBytesl, byte[] charBytes2);
        string GetFixedXOR(byte charBytel, byte charByte2);
    }


    public class FixedXOR : IFixedXOR
    {
        public string GetFixedXOR(byte[] charBytes1, byte[] charBytes2)
        {
            if (charBytes1.Length != charBytes2.Length)
                throw new ArgumentException("Strings must be equal length");

            var toReturn = "";

            for (int i = 0; i < charBytes1.Length; i++)
                toReturn += GetFixedXOR(charBytes1[i], charBytes2[i]);
            return toReturn;

        }

        public string GetFixedXOR(byte charBytel, byte charByte2)
        {
            return String.Format("{0:x}", charBytel ^ charByte2).PadLeft(2, "0"[0]);
        }


    }

}
