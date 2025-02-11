using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vip.DFe.Demo.Helpers;

namespace Vip.DFe.Demo.Extensions
{
    public static class ComboboxExtensions
    {
        public static T SelectedItem<T>(this ComboBox comboBox) where T : class
        {
            return (T) comboBox.SelectedItem;
        }

        public static IEnumerable<T> Objects<T>(this ComboBox comboBox) where T : class
        {
            return comboBox.Items.OfType<T>();
        }

        public static T GetEnumValue<T>(this ComboBox comboBox) where T : struct, IConvertible
        {
            var type = typeof(T);
            var value = System.Enum.GetValues(type).Cast<T>().FirstOrDefault(x => x.GetDescription() == comboBox.Text);
            return value;
        }

        public static T? GetEnumValueOrNull<T>(this ComboBox comboBox) where T : struct, IConvertible
        {
            if (comboBox.SelectedItem.IsNull()) return null;
            return comboBox.GetEnumValue<T>();
        }

        public static void DataSource<T>(this ComboBox comboBox) where T : struct, IConvertible
        {
            comboBox.DataSource = EnumHelper.GetDescriptions<T>();
            comboBox.Refresh();
        }

        public static void DataSource<T>(this ComboBox comboBox, T value) where T : struct, IConvertible
        {
            comboBox.DataSource<T>();
            comboBox.Text = value.GetDescription();
        }

        public static void DataSource<T>(this ComboBox comboBox, T[] excludes) where T : struct, IConvertible
        {
            var listExcludes = new List<string>();
            excludes.ForEach(x => listExcludes.Add(x.GetDescription()));
            comboBox.DataSource = EnumHelper.GetDescriptions<T>().Where(x => !listExcludes.Contains(x)).ToList();
            comboBox.Refresh();
        }

        public static void Clear(this ComboBox comboBox)
        {
            comboBox.SelectedItem = null;
            comboBox.ResetText();
        }
    }
}