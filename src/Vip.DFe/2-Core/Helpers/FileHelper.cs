using System;
using System.IO;

namespace Vip.DFe.Helpers
{
    [Obsolete("Sem uso na biblioteca; será removido em versão futura.")]
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