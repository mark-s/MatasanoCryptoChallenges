using System.Diagnostics;

namespace MatasantoCrypto.Set1.SharedCore
{
    [DebuggerDisplay("Text: {Text} KeyChar: [{KeyChar}] KeyString: [{KeyString}] Score: {Score}")]
    public struct ResultItem
    {
        public string Text { get; set; }
        public char KeyChar { get; set; }
        public string KeyString { get; set; }
        public int Score { get; set; }
    }
}
