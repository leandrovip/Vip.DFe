using System.IO;

namespace Vip.DFe.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        ///     Cria diretório caso não exista
        /// </summary>
        /// <param name="path">Caminho do diretório</param>
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}