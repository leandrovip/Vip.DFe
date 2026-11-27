using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Vip.DFe.Attributes;
using Vip.DFe.Cryptography.Models;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal
{
    [DFeSignInfoElement("infNFe")]
    [DFeRoot("NFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public sealed class NFe : DFeSignDocument<NFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields

        private infNFe _infNFe;

        #endregion;

        #region Constructor

        public NFe()
        {
            _infNFe = new infNFe(this);
            InfNFeSupl = new infNFeSupl();
            Signature = new DFeSignature();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     A01 - Informações da Nota Fiscal Eletrônica
        /// </summary>
        [DFeElement("infNFe", Id = "A01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public infNFe InfNFe
        {
            get => _infNFe;
            set
            {
                if (_infNFe == value) return;

                _infNFe = value;
                if (_infNFe != null && _infNFe.Parent != this)
                    _infNFe.Parent = this;
            }
        }

        /// <summary>
        ///     ZX01 - Informações suplementares da Nota Fiscal
        /// </summary>
        [DFeElement("infNFeSupl", Id = "ZX01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public infNFeSupl InfNFeSupl { get; set; }

        [DFeIgnore] public bool Assinado => ShouldSerializeSignature();

        #endregion

        #region Methods

        /// <summary>
        ///     Assina a NFe
        /// </summary>
        /// <param name="certificado"> Certificado Digital</param>
        /// <param name="saveOptions"> Opções </param>
        public void Assinar(X509Certificate2 certificado, SaveOptions saveOptions)
        {
            Guard.Against<ArgumentNullException>(certificado == null, "Certificado não pode ser nulo.");

            if (InfNFe.Id.IsNullOrEmpty() || InfNFe.Id.Length < 44)
            {
                var chave = ChaveDFe.Gerar(
                    InfNFe.Ide.CodigoUF,
                    InfNFe.Ide.DhEmi.DateTime,
                    InfNFe.Emitente.CNPJ,
                    (int) InfNFe.Ide.Modelo,
                    InfNFe.Ide.Serie,
                    InfNFe.Ide.NumeroNFe,
                    InfNFe.Ide.TipoEmissao,
                    InfNFe.Ide.CNf.ToInt32()
                );

                InfNFe.Id = $"NFe{chave.Chave}";
                InfNFe.Ide.CDV = chave.Digito;
            }

            AssinarDocumento(certificado, saveOptions, false, SignDigest.SHA1);
        }

        /// <summary>
        ///     Valida a assinatura da NFe
        /// </summary>
        /// <param name="gerarXml"></param>
        /// <returns></returns>
        public bool ValidarAssinatura(bool gerarXml = true)
        {
            Guard.Against<VipException>(!Assinado, "Documento não esta assinado.");
            return ValidarAssinaturaDocumento(gerarXml);
        }

        /// <summary>
        ///     Retorna Nome do XML da NFe
        /// </summary>
        /// <returns></returns>
        public string ObterNomeXml()
        {
            // Remove só o prefixo "NFe" do Id (a chave pode conter letras do CNPJ alfanumérico).
            var id = InfNFe.Id ?? string.Empty;
            return $"{(id.StartsWith("NFe") ? id.Substring(3) : id)}-nfe.xml";
        }

        /// <summary>
        ///     Gera a URL para o QR-Code (versão 2.0 ou 3.0, conforme <see cref="NFeConfig.NFCeVersaoQrCode" />) com o
        ///     tratamento de parâmetro query no final da url.
        ///     <para>A versão 3.0 em contingência off-line exige certificado: use a sobrecarga com certificado.</para>
        /// </summary>
        /// <returns></returns>
        public void GerarQrCode(NFeConfig configuracao)
        {
            GerarQrCode(configuracao, null);
        }

        /// <summary>
        ///     Gera a URL para o QR-Code (versão 2.0 ou 3.0, conforme <see cref="NFeConfig.NFCeVersaoQrCode" />) com o
        ///     tratamento de parâmetro query no final da url.
        ///     <para>
        ///         Para NF-e modelo 55 com tpImp = 6 (DANFE Simplificado Tipo 2) gera sempre a versão 3.0, sem CSC; demais NF-e
        ///         não possuem QR Code.
        ///     </para>
        /// </summary>
        /// <param name="configuracao">Configuração.</param>
        /// <param name="certificado">
        ///     Certificado que assina a NFC-e; obrigatório apenas para a versão 3.0 em contingência off-line
        ///     (tpEmis = 9).
        /// </param>
        public void GerarQrCode(NFeConfig configuracao, X509Certificate2 certificado)
        {
            var nfce = InfNFe.Ide.Modelo == NFeModelo.NFCe;

            // NT 2026.003/2026.002 (ZX02): NF-e modelo 55 com tpImp = 6 (DANFE Simplificado Tipo 2) exige infNFeSupl
            // com QR Code versão 3 e URL de consulta da NFC-e da UF.
            var nfeSimplificadoTipo2 = InfNFe.Ide.Modelo == NFeModelo.NFe && InfNFe.Ide.TipoImpressao == TipoImpressao.SimplificadoTipo2;
            if (!nfce && !nfeSimplificadoTipo2) return;

            Guard.Against<VipException>(nfeSimplificadoTipo2 && InfNFe.Ide.TipoEmissao != TipoEmissao.Normal && InfNFe.Ide.TipoEmissao != TipoEmissao.OffLine,
                "NF-e com DANFE Simplificado Tipo 2 (tpImp=6) só admite emissão normal (tpEmis=1) ou contingência off-line (tpEmis=9)");

            var versao3 = nfeSimplificadoTipo2 || configuracao.NFCeVersaoQrCode == NFeVersaoQrCode.V3;

            if (!versao3)
            {
                Guard.Against<VipException>(configuracao.NFCeIdToken.IsNullOrEmpty(), "ID do token para montagem do QRCode não encontrado");
                Guard.Against<VipException>(configuracao.NFCeCSC.IsNullOrEmpty(), "Token para montagem do QRCode não encontrado");
            }

            // NFC-e: mantém a busca pelo modelo da configuração; NF-e com tpImp = 6 usa as URLs da NFC-e (modelo 65) da UF.
            var enderecoQrCode = nfce ? configuracao.Webservices.ObterUrlQrCode() : configuracao.Webservices.ObterUrlQrCode(NFeModelo.NFCe);
            Guard.Against<VipException>(enderecoQrCode.IsNullOrEmpty(), "URL do QrCode não encontrada");

            var urlConsultaChave = nfce ? configuracao.Webservices.ObterUrlChave() : configuracao.Webservices.ObterUrlChave(NFeModelo.NFCe);
            Guard.Against<VipException>(urlConsultaChave.IsNullOrEmpty(), "URL de consulta da chave não encontrada");

            #region Gerar URL QRCODE

            const string interrogacao = "?";
            const string parametro = "p=";

            if (!enderecoQrCode.EndsWith(interrogacao))
                enderecoQrCode += interrogacao;
            if (!enderecoQrCode.EndsWith(parametro))
                enderecoQrCode += parametro;

            const string pipe = "|";

            //Chave de Acesso da NFC-e 
            var chave = InfNFe.Id.Substring(3);

            //Identificação do Ambiente (1 – Produção, 2 – Homologação) 
            var ambiente = (int) InfNFe.Ide.TpAmb;

            if (versao3)
            {
                var urlQrCodeV3 = string.Concat(enderecoQrCode, ObterDadosQrCodeV3(chave, ambiente, certificado));
                InfNFeSupl.QrCode = urlQrCodeV3;
                InfNFeSupl.UrlChave = urlConsultaChave;
                return;
            }

            //Identificador do CSC (Código de Segurança do Contribuinte no Banco de Dados da SEFAZ). Informar sem os zeros não significativos
            var idCsc = configuracao.NFCeIdToken.ToInt32(1);

            const int versaoQrCode = 2;

            string dadosBase;

            if (InfNFe.Ide.TipoEmissao == TipoEmissao.OffLine)
            {
                var diaEmi = InfNFe.Ide.DhEmi.Day.ToString("D2");
                var valorNfce = InfNFe.Total.IcmsTot.VNf.ToString("0.00").Replace(',', '.');
                var digVal = Signature.SignedInfo.Reference.DigestValue.ObterHex();
                dadosBase = string.Concat(chave, pipe, versaoQrCode, pipe, ambiente, pipe, diaEmi, pipe, valorNfce, pipe, digVal, pipe, idCsc);
            }
            else
            {
                dadosBase = string.Concat(chave, pipe, versaoQrCode, pipe, ambiente, pipe, idCsc);
            }

            var dadosSha1 = string.Concat(dadosBase, configuracao.NFCeCSC).ObterHexSha1();
            var urlQrCode = string.Concat(enderecoQrCode, dadosBase, pipe, dadosSha1);

            #endregion

            InfNFeSupl.QrCode = urlQrCode;
            InfNFeSupl.UrlChave = urlConsultaChave;
        }

        /// <summary>
        ///     Monta os parâmetros do QR Code versão 3 (Manual DANFE NFC-e e QR Code, itens 4.4.1 e 4.4.2).
        /// </summary>
        private string ObterDadosQrCodeV3(string chave, int ambiente, X509Certificate2 certificado)
        {
            var online = string.Join("|", chave, "3", ambiente.ToString(CultureInfo.InvariantCulture));
            if (InfNFe.Ide.TipoEmissao != TipoEmissao.OffLine) return online;

            Guard.Against<VipException>(certificado == null, "Certificado digital não informado: obrigatório para gerar o QRCode versão 3 em contingência off-line (tpEmis = 9)");

            var diaEmi = InfNFe.Ide.DhEmi.Day.ToString("D2", CultureInfo.InvariantCulture);
            var valorNfce = InfNFe.Total.IcmsTot.VNf.ToString("0.00", CultureInfo.InvariantCulture);

            // NT 2025.001 (ZX02): a identificação do destinatário no QR Code deve coincidir com o grupo dest da NFC-e
            // (mesmo critério de serialização de dest e mesma normalização do XML: sem máscara, zeros à esquerda).
            var tipoDest = string.Empty;
            var idDest = string.Empty;
            var dest = InfNFe.Destinatario;
            if (dest != null)
            {
                if (dest.CNPJ.IsNotNullOrEmpty())
                {
                    tipoDest = "1";
                    idDest = dest.CNPJ.SomenteAlfanumerico().PadLeft(14, '0');
                }
                else if (dest.CPF.IsNotNullOrEmpty())
                {
                    tipoDest = "2";
                    idDest = dest.CPF.OnlyNumbers().PadLeft(11, '0');
                }
                else if (dest.IdEstrangeiro.IsNotNullOrEmpty() && dest.Nome.IsNotNullOrEmpty())
                {
                    tipoDest = "3";
                }
            }

            var parametros = string.Join("|", online, diaEmi, valorNfce, tipoDest, idDest);
            var assinatura = AssinarRsaSha1(certificado, Encoding.UTF8.GetBytes(parametros));
            return string.Concat(parametros, "|", Convert.ToBase64String(assinatura));
        }

        /// <summary>
        ///     Assina os dados com RSA SHA-1 (PKCS#1 v1.5). Usa GetRSAPrivateKey (CAPI e CNG) e, se indisponível, a chave legada.
        /// </summary>
        private static byte[] AssinarRsaSha1(X509Certificate2 certificado, byte[] dados)
        {
            RSA rsaCng = null;
            try
            {
                rsaCng = certificado.GetRSAPrivateKey();
            }
            catch (NotSupportedException)
            {
            }

            if (rsaCng != null)
                using (rsaCng)
                    return rsaCng.SignData(dados, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);

#pragma warning disable CS0618
            var rsa = certificado.PrivateKey as RSA;
#pragma warning restore CS0618
            Guard.Against<VipException>(rsa == null, "Chave privada RSA do certificado não encontrada para assinar o QRCode versão 3");
            return rsa.SignData(dados, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        }

        private bool ShouldSerializeInfNFeSupl()
        {
            return InfNFeSupl.QrCode.IsNotNullOrEmpty() || InfNFeSupl.UrlChave.IsNotNullOrEmpty();
        }

        #endregion
    }
}