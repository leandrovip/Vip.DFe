using System.Text;

namespace Vip.DFe.Extensions
{
    internal static class ByteExtensions
    {
        public static string ObterHex(this byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }

            return sb.ToString();
        }
    }
}