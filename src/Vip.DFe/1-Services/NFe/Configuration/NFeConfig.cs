using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.Configuration
{
    public class NFeConfig
    {
        #region Fields

        private string _cnpj;

        #endregion

        #region Constructors

        internal NFeConfig(NFeService parent)
        {
            Parent = parent;
            Ambiente = TipoAmbiente.Homologacao;
            TipoEmissao = TipoEmissao.Normal;
            Modelo = NFeModelo.NFe;
            Versao = NFeVersao.v400;
            _cnpj = "";
            NFCeIdToken = "";
            NFCeCSC = "";
            NFCeVersaoQrCode = NFeVersaoQrCode.V2;
            EnviarModoSincrono = true;
            ValidarDigest = true;
            RemoverAcentos = true;
            RemoverEspacos = true;
            ExibeErrosSchema = true;
            Webservices = new NFeConfigWebService(this);
            Arquivos = new NFeConfigArquivo(this);
            Certificado = new NFeConfigCertificado();
        }

        #endregion

        #region Properties

        internal NFeService Parent { get; }
        public TipoAmbiente Ambiente { get; set; }
        public TipoEmissao TipoEmissao { get; set; }
        public NFeModelo Modelo { get; set; }
        public NFeVersao Versao { get; set; }

        public string CNPJ
        {
            get => _cnpj;
            set => _cnpj = value.TrimVip().SomenteAlfanumerico();
        }

        public string NFCeIdToken { get; set; }
        public string NFCeCSC { get; set; }

        /// <summary>
        ///     Versão do QR Code da NFC-e (padrão: <see cref="NFeVersaoQrCode.V2" />).
        ///     <para>
        ///         A versão 3 não usa CSC/idCSC (<see cref="NFCeIdToken" /> e <see cref="NFCeCSC" /> não são exigidos); na
        ///         contingência off-line a assinatura é feita com o certificado digital do emitente. É voltada ao emitente
        ///         produtor rural pessoa física (rejeição 444 - NFC-e com qrCode na versão 2 para Pessoa Física, exceto PR);
        ///         para pessoa jurídica é opcional (NT 2025.001).
        ///     </para>
        /// </summary>
        public NFeVersaoQrCode NFCeVersaoQrCode { get; set; }

        /// <summary>
        ///     Envio do lote em modo síncrono (indSinc = 1). Padrão: true.
        ///     <para>
        ///         Obrigatório para lote com uma única NF-e/NFC-e (NT 2025.001, rejeição 452). Lote com mais de uma NF-e deve
        ///         usar o modo assíncrono (false).
        ///     </para>
        /// </summary>
        public bool EnviarModoSincrono { get; set; }
        public bool ValidarDigest { get; set; }
        public bool RemoverAcentos { get; set; }
        public bool RemoverEspacos { get; set; }
        public bool ExibeErrosSchema { get; set; }

        public NFeConfigWebService Webservices { get; private set; }
        public NFeConfigArquivo Arquivos { get; private set; }
        public NFeConfigCertificado Certificado { get; private set; }

        #endregion

        #region Methods

        public SaveOptions ObterOptions()
        {
            var saveOptions = SaveOptions.DisableFormatting | SaveOptions.OmitDeclaration;
            if (RemoverAcentos) saveOptions |= SaveOptions.RemoveAccents;
            if (RemoverEspacos) saveOptions |= SaveOptions.RemoveSpaces;
            return saveOptions;
        }

        public void Validar()
        {
            Webservices.Configurar();
        }

        #endregion
    }
}