using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;

namespace Vip.DFe
{
    public static class CertificadoDigital
    {
        #region Methods

        /// <summary>
        ///     Define o PIN para chave privada de um objeto <see cref="X509Certificate2" /> passado no parâmetro
        /// </summary>
        private static void DefinirPinParaChavePrivada(this X509Certificate2 certificado, string pin)
        {
            Guard.Against<ArgumentNullException>(certificado.IsNull(), "Certificado não informado");

            var key = (RSACryptoServiceProvider)certificado.PrivateKey;

            var providerHandle = IntPtr.Zero;
            var pinBuffer = Encoding.ASCII.GetBytes(pin);

            MetodosNativos.Executar(() => MetodosNativos.CryptAcquireContext(ref providerHandle,
                key.CspKeyContainerInfo.KeyContainerName,
                key.CspKeyContainerInfo.ProviderName,
                key.CspKeyContainerInfo.ProviderType,
                MetodosNativos.CryptContextFlags.Silent));
            MetodosNativos.Executar(() => MetodosNativos.CryptSetProvParam(providerHandle,
                MetodosNativos.CryptParameter.KeyExchangePin,
                pinBuffer, 0));
            MetodosNativos.Executar(() => MetodosNativos.CertSetCertificateContextProperty(
                certificado.Handle,
                MetodosNativos.CertificateProperty.CryptoProviderHandle,
                0, providerHandle));
        }

        /// <summary>
        ///     Busca o certificado digital, caso não informado número de série é exibido a caixa de diálogo
        /// </summary>
        /// <param name="numeroSerie">Serie do certificado.</param>
        /// <param name="senha">Senha do certificado</param>
        /// <returns>X509Certificate2</returns>
        /// <exception cref="VipException">
        /// </exception>
        public static X509Certificate2 SelecionarCertificadoPin(string numeroSerie, string senha = "")
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            try
            {
                store.Open(OpenFlags.MaxAllowed | OpenFlags.ReadOnly);

                var certificates = store.Certificates.Find(X509FindType.FindByTimeValid, DateTime.Now, true)
                    .Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);

                var certificadosSelecionados = certificates.Find(X509FindType.FindBySerialNumber, numeroSerie, false);
                Guard.Against<VipException>(certificadosSelecionados.Count == 0, "Certificado digital não encontrado");

                var certificado = certificadosSelecionados.Count < 1 ? null : certificadosSelecionados[0];
                if (senha.IsNotNullOrEmpty()) certificado.DefinirPinParaChavePrivada(senha);
                return certificado;
            }
            finally
            {
                store.Close();
            }
        }

        /// <summary>
        ///     Seleciona um certificado informando o caminho e a senha
        /// </summary>
        /// <param name="caminho">Caminho do certificado digital</param>
        /// <param name="senha">Senha do certificado</param>
        /// <returns>X509Certificate2</returns>
        /// <exception cref="VipException">Arquivo do Certificado digital não encontrado</exception>
        public static X509Certificate2 SelecionarCertificado(string caminho, string senha)
        {
            Guard.Against<ArgumentNullException>(caminho.IsNullOrEmpty(), "Caminho do arquivo não poder ser nulo ou vazio");
            Guard.Against<ArgumentException>(!File.Exists(caminho), "Arquivo do Certificado digital não encontrado");

            var secureString = new SecureString();
            foreach (var s in senha) secureString.AppendChar(s);

            var cert = new X509Certificate2(caminho, secureString);
            return cert;
        }

        /// <summary>
        ///     Seleciona um certificado passando um array de bytes.
        /// </summary>
        /// <param name="certificado">O certificado.</param>
        /// <param name="senha">A senha.</param>
        /// <returns>X509Certificate2.</returns>
        /// <exception cref="System.Exception">Arquivo do Certificado digital não encontrado</exception>
        public static X509Certificate2 SelecionarCertificado(byte[] certificado, string senha = "")
        {
            Guard.Against<ArgumentNullException>(certificado == null, "O certificado não poder ser nulo");
            Guard.Against<ArgumentException>(certificado?.Length == 0, "Certificado com tamanho inválido");

            var secureString = new SecureString();
            foreach (var s in senha) secureString.AppendChar(s);

            var cert = new X509Certificate2(certificado, secureString);
            return cert;
        }

        #endregion Methods

        #region Internal

        internal static class MetodosNativos
        {
            internal enum CryptContextFlags
            {
                None = 0,
                Silent = 0x40
            }

            internal enum CertificateProperty
            {
                None = 0,
                CryptoProviderHandle = 0x1
            }

            internal enum CryptParameter
            {
                None = 0,
                KeyExchangePin = 0x20
            }

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool CryptAcquireContext(
                ref IntPtr hProv,
                string containerName,
                string providerName,
                int providerType,
                CryptContextFlags flags
            );

            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern bool CryptSetProvParam(
                IntPtr hProv,
                CryptParameter dwParam,
                [In] byte[] pbData,
                uint dwFlags);

            [DllImport("CRYPT32.DLL", SetLastError = true)]
            internal static extern bool CertSetCertificateContextProperty(
                IntPtr pCertContext,
                CertificateProperty propertyId,
                uint dwFlags,
                IntPtr pvData
            );

            public static void Executar(Func<bool> action)
            {
                if (!action()) throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        #endregion
    }
}