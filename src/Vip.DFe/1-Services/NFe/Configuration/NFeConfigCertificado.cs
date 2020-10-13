using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Vip.DFe.Extensions;

namespace Vip.DFe.NFe.Configuration
{
    public sealed class NFeConfigCertificado
    {
        #region Fields

        private DateTime dataVenc;
        private string subjectName;
        private string cnpj;

        #endregion Fields

        #region Constructor

        internal NFeConfigCertificado()
        {
            dataVenc = DateTime.MinValue;
            Certificado = string.Empty;
            Senha = string.Empty;
            ManterEmCache = false;
            subjectName = string.Empty;
            cnpj = string.Empty;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        ///     Certificado ou Numero de Serie.
        /// </summary>
        public string Certificado { get; set; }

        /// <summary>
        ///     Define/retorna o certificado em bytes.
        /// </summary>
        public byte[] CertificadoBytes { get; set; }

        /// <summary>
        ///     Define/retorna a senha do certificado.
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        ///     Define se irá manter o cache do certificado digital na memória
        /// </summary>
        public bool ManterEmCache { get; set; }

        /// <summary>
        ///     Retorna a data de vencimento do certificado.
        /// </summary>
        public DateTime DataVenc
        {
            get
            {
                if (dataVenc == DateTime.MinValue && !Certificado.IsNullOrEmpty() && !CertificadoBytes.IsNullOrEmpty()) GetCertificado();
                return dataVenc;
            }
        }

        /// <summary>
        ///     Retorna o nome do responsável pelo certificado.
        /// </summary>
        public string Nome
        {
            get
            {
                if (subjectName.IsNullOrEmpty() && !Certificado.IsNullOrEmpty() && !CertificadoBytes.IsNullOrEmpty()) GetCertificado();
                return subjectName;
            }
        }

        /// <summary>
        ///     Retornar o CNPJ
        /// </summary>
        public string CNPJ
        {
            get
            {
                if (cnpj.IsNullOrEmpty() && !Certificado.IsNullOrEmpty() && !CertificadoBytes.IsNullOrEmpty()) GetCertificado();
                return cnpj;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        ///     Seleciona um certificado digital instalado na maquina retornando o numero de serie do mesmo.
        /// </summary>
        public string SelecionarCertificado()
        {
            var cert = CertificadoDigital.SelecionarCertificadoPin(string.Empty);
            return cert?.GetSerialNumberString() ?? string.Empty;
        }

        /// <summary>
        ///     Retorna o certificado digital de acordo com os dados informados.
        /// </summary>
        public X509Certificate2 ObterCertificado()
        {
            if (CertificadoBytes?.Length > 0) return CertificadoDigital.SelecionarCertificado(CertificadoBytes, Senha);
            if (File.Exists(Certificado)) return CertificadoDigital.SelecionarCertificado(Certificado, Senha);
            var ret = CertificadoDigital.SelecionarCertificadoPin(Certificado, Senha);

            return ret;
        }

        /// <summary>
        ///     Retornar o Certificado
        /// </summary>
        private void GetCertificado()
        {
            var cert = ObterCertificado();

            try
            {
                dataVenc = cert.GetExpirationDateString().ToData();
                subjectName = cert.SubjectName.Name;
                cnpj = cert.GetCNPJ();
            }
            finally
            {
                if (!ManterEmCache)
                {
                    cert?.Reset();
                    cert?.Dispose();
                }
            }
        }

        #endregion Methods
    }
}