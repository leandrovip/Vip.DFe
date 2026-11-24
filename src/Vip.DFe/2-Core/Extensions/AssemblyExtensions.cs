using System;
using System.IO;
using System.Reflection;

namespace Vip.DFe.Extensions
{
    /// <summary>
    ///     Classe AssemblyExtensions.
    /// </summary>
    internal static class AssemblyExtensions
    {
        /// <summary>
        ///     Gets the path.
        /// </summary>
        /// <param name="ass">The ass.</param>
        /// <returns>System.String.</returns>
        public static string GetPath(this Assembly ass)
        {
            var uri = new UriBuilder(ass.CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}