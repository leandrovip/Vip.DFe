using System.Windows.Forms;

namespace Vip.DFe.Demo.Extensions
{
    public static class CheckBoxExtensions
    {
        public static string NameProperty(this CheckBox checkBox)
        {
            return checkBox.Name.Substring(3, checkBox.Name.Length - 3).Replace("_", ".");
        }

        public static void Check(this CheckBox checkBox)
        {
            checkBox.Checked = true;
        }

        public static void Uncheck(this CheckBox checkBox)
        {
            checkBox.Checked = false;
        }
    }
}