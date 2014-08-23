namespace MatasantoCrypto.Set1.SharedCore
{
    public static class StringExtensions
    {
        public static char[] Repeat(this string input, int maxCharacters)
        {
            if (input == null)
                return null;

            var arr = input.ToCharArray();

            int iSource = 0;
            char[] destArray = new char[maxCharacters];
            for (int i = 0; i < destArray.Length; i++)
            {
                if (iSource >= arr.Length)
                {
                    iSource = 0; // reset if at end of source
                }
                destArray[i] = arr[iSource++];
            }


            return destArray;
        }
    }
}