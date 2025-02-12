using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.Demo.Models
{
    public class Configuracao
    {
        #region Configuração

        public NFeVersao Versao { get; set; } = NFeVersao.v400;
        public TipoAmbiente Ambiente { get; set; } = TipoAmbiente.Homologacao;
        public TipoEmissao TipoEmissao { get; set; } = TipoEmissao.Normal;
        public NFeModelo Modelo { get; set; } = NFeModelo.NFe;
        public CodigoUF Estado { get; set; }
        public NFeFinalidade FinalidadeEmissao { get; set; } = NFeFinalidade.Normal;
        public NFeTipo TipoMovimento { get; set; } = NFeTipo.Saida;
        public NFeDestinoOperacao DestinoOperacao { get; set; } = NFeDestinoOperacao.Interna;

        public int IntervaloTentativas { get; set; } = 1600;
        public int NumeroTentativas { get; set; } = 3;
        public int Timeout { get; set; } = 30;

        public bool RemoverAcentos { get; set; } = true;
        public bool RemoverEspacos { get; set; } = true;
        public bool ExibirErroSchema { get; set; } = true;
        public bool EnviarModoSincrono { get; set; }

        #endregion

        #region Certificado Digital

        public string CertificadoNumeroSerie { get; set; }
        public string CertificadoSenha { get; set; }
        public bool CertificadoManterCache { get; set; }

        #endregion

        #region Arquivos

        public string DiretorioArquivos { get; set; }
        public string DiretorioSchemas { get; set; }
        public bool SalvarArquivos { get; set; } = true;

        #endregion

        #region Informações Documento

        public string NaturezaOperacao { get; set; }
        public NFePresencaComprador IndicadorPresenca { get; set; } = NFePresencaComprador.Presencial;
        public int Serie { get; set; }
        public int UltimoNumeroNFe { get; set; }
        public int UltimoNumeroNFCe { get; set; }
        public string JustificativaContingencia { get; set; }
        public string IdToken { get; set; }
        public string CscToken { get; set; }
        public bool ConsumidorFinal { get; set; } = true;

        #endregion

        #region Dados Emitente

        public string EmitenteCnpj { get; set; }
        public string EmitenteInscricaoEstadual { get; set; }
        public string EmitenteRazaoSocial { get; set; }
        public RegimeTributario EmitenteRegimeTributario { get; set; }
        public string EmitenteLogradouro { get; set; }
        public string EmitenteNumero { get; set; }
        public string EmitenteBairro { get; set; }
        public string EmitenteCep { get; set; }
        public int EmitenteCodigoIbge { get; set; }
        public string EmitenteCidade { get; set; }
        public string EmitenteUf { get; set; }
        public string EmitenteTelefone { get; set; }

        #endregion

        #region Dados Destinatário

        public string DestinatarioCnpj { get; set; }
        public string DestinatarioInscricaoEstadual { get; set; }
        public string DestinatarioRazaoSocial { get; set; }
        public NFeIndIeDest DestinatarioIndicadorInscricaoEstadual { get; set; }
        public string DestinatarioLogradouro { get; set; }
        public string DestinatarioNumero { get; set; }
        public string DestinatarioBairro { get; set; }
        public string DestinatarioCep { get; set; }
        public int DestinatarioCodigoIbge { get; set; }
        public string DestinatarioCidade { get; set; }
        public string DestinatarioUf { get; set; }
        public string DestinatarioTelefone { get; set; }

        #endregion

        #region Dados Transporte

        public NFeModalidadeFrete TransporteModalidadeFrete { get; set; }
        public string TransporteCnpj { get; set; }
        public string TransporteRazaoSocial { get; set; }
        public string TransporteEndereco { get; set; }
        public string TransporteCidade { get; set; }
        public string TransporteUf { get; set; }
        public bool TransporteInformarPlacaVeiculo { get; set; }

        #endregion

        #region Dados Volume

        public string VolumeEspecie { get; set; }
        public string VolumeMarca { get; set; }
        public string VolumeNumeracao { get; set; }
        public int VolumeQuantidade { get; set; }
        public decimal VolumePesoBruto { get; set; }
        public decimal VolumePesoLiquido { get; set; }

        #endregion
    }
}