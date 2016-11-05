using System.Text;

namespace SetTwo.SharedCore
{
    public static class StringExtensions
    {

        public static byte[] ToByteArray(this string inputString)
        {
            return Encoding.UTF8.GetBytes(inputString);
        }

    }
}
