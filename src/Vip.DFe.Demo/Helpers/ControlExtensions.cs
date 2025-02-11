using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vip.DFe.Demo.Extensions;

namespace Vip.DFe.Demo.Helpers
{
    public static class ControlExtensions
    {
        public static IEnumerable<Control> All(this Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                foreach (var controlChild in control.Controls.All())
                    yield return controlChild;

                yield return control;
            }
        }

        public static IEnumerable<ToolStripMenuItem> All(this ToolStripItemCollection menuCollection)
        {
            foreach (var toolStripDropDownItem in menuCollection.OfType<ToolStripDropDownItem>())
            {
                var menuItem = (ToolStripMenuItem) toolStripDropDownItem;
                foreach (var menuChild in menuItem.DropDownItems.All())
                    yield return menuChild;

                yield return menuItem;
            }
        }

        public static void PreencherTextBox(this Control.ControlCollection controls, object model)
        {
            foreach (var textBox in controls.All().OfType<TextBox>())
                textBox.Text = model.GetPropertyValue<string>(textBox.NameProperty());
        }

        public static void PreencherCheckBox(this Control.ControlCollection controls, object model)
        {
            foreach (var checkBox in controls.All().OfType<CheckBox>())
                checkBox.Checked = model.GetPropertyValue<bool>(checkBox.NameProperty());
        }
    }
}