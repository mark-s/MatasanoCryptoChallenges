using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetOne.One
{
    public interface IConvertHexToBase64
    {
        string ConvertToBase64(byte[] hexBytes); 
        byte[] GetBytesFromhexString(string hexString);
    }


}
