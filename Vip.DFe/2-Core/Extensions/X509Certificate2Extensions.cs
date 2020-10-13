using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Vip.DFe.Exception;

namespace Vip.DFe.Extensions
{
    public static class X509Certificate2Extensions
    {
        /// <summary>
        ///     Retorna o CNPJ do certificado se o mesmo possuir
        /// </summary>
        /// <param name="certificado">Certificado</param>
        /// <returns></returns>
        public static string GetCNPJ(this X509Certificate2 certificado)
        {
            Guard.Against<ArgumentNullException>(certificado == null, nameof(certificado));

            var cnpj = string.Empty;
            var extensions = from X509Extension extension in certificado.Extensions
                select extension.Format(true)
                into s1
                select s1.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var lines in extensions)
            {
                foreach (var t in lines)
                {
                    if (!t.Trim().StartsWith("2.16.76.1.3.3")) continue;

                    var value = t.Substring(t.IndexOf('=') + 1);
                    var elements = value.Split(' ');
                    var cnpjBytes = new byte[14];

                    for (var j = 0; j < cnpjBytes.Length; j++) cnpjBytes[j] = Convert.ToByte(elements[j + 2], 16);

                    cnpj = Encoding.UTF8.GetString(cnpjBytes);
                    break;
                }

                if (!cnpj.IsNullOrEmpty()) break;
            }

            return cnpj;
        }

        /// <summary>
        ///     Verifica se o certificado digital esta dentro da validade.
        ///     <para>Verificar validade do certificado digital, se vencido dispara ArgumentException</para>
        /// </summary>
        /// <param name="certificado"></param>
        public static bool IsValid(this X509Certificate2 certificado)
        {
            var dataExpiracao = Convert.ToDateTime(certificado.GetExpirationDateString());
            return dataExpiracao <= DateTime.Now;
        }
    }
}