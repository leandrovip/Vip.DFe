using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Identificacao
{
    public sealed class NFeNfRef : GenericClone<NFeNfRef>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     BA02 - Chave de acesso da NF-e referenciada
        /// </summary>
        [DFeElement(TipoCampo.Str, "refNFe", Id = "BA02", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string RefNFe { get; set; }

        /// <summary>
        ///     BA03 - Informação da NF modelo 1/1A referenciada
        /// </summary>
        [DFeElement("refNF", Id = "BA03", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeRefNf RefNF { get; set; }

        /// <summary>
        ///     BA10 - Informações da NF de produtor rural referenciada
        /// </summary>
        [DFeElement("refNFP", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeRefNfp RefNFP { get; set; }

        /// <summary>
        ///     BA19 - Chave de acesso do CT-e referenciado
        /// </summary>
        [DFeElement(TipoCampo.Str, "refCTe", Id = "BA19", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string RefCTe { get; set; }

        /// <summary>
        ///     BA20 - Informações do Cupom Fiscal referenciado
        /// </summary>
        [DFeElement("refECF", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeRefEcf RefECF { get; set; }

        #endregion
    }
}