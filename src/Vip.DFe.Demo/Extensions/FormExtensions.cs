using System.Windows.Forms;
using Vip.DFe.Demo.Helpers;

namespace Vip.DFe.Demo.Extensions
{
    public static class FormExtensions
    {
        public static void PreencherTextBox(this Form form, object model)
        {
            form.Controls.PreencherTextBox(model);
        }

        public static void PreencherCheckBox(this Form form, object model)
        {
            form.Controls.PreencherCheckBox(model);
        }
    }
}