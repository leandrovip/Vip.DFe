using System.Windows.Forms;

namespace Vip.DFe.Demo.Extensions
{
    public static class TextBoxExtensions
    {
        public static string NameProperty(this TextBox textBox)
        {
            return textBox.Name.Substring(3, textBox.Name.Length - 3).Replace("_", ".");
        }
    }
}