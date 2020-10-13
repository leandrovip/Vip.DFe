using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Identificacao
{
    /// <summary>
    ///     Informações da NF de produtor rural referenciada
    /// </summary>
    public sealed class NFeRefNfp : GenericClone<NFeRefNfp>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     BA11 - Código da UF do emitente
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cUF", Id = "BA11", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CodigoUF CodigoUF { get; set; }

        /// <summary>
        ///     BA12 - Ano e Mês de emissão da NF-e
        /// </summary>
        [DFeElement(TipoCampo.Int, "AAMM", Id = "BA12", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int AAMM { get; set; }

        /// <summary>
        ///     BA13 - CNPJ do emitente
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "BA13", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     BA14 - CPF do emitente
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "BA14", Min = 11, Max = 11, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CPF { get; set; }

        /// <summary>
        ///     BA15 - IE do emitente
        /// </summary>
        [DFeElement(TipoCampo.Custom, "IE", Id = "BA15", Min = 2, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string IE { get; set; }

        /// <summary>
        ///     BA16 - Modelo do Documento Fiscal
        /// </summary>
        [DFeElement(TipoCampo.Enum, "mod", Id = "BA16", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeRefNfpMod Mod { get; set; }

        /// <summary>
        ///     BA17 - Série do Documento Fiscal, informar 0 se inexistente
        /// </summary>
        [DFeElement(TipoCampo.Int, "serie", Id = "BA17", Min = 1, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int Serie { get; set; }

        /// <summary>
        ///     BA18 - Número do Documento Fiscal
        /// </summary>
        [DFeElement(TipoCampo.Int, "nNF", Id = "BA18", Min = 1, Max = 9, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NumeroFiscal { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeCNPJ()
        {
            return CNPJ.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeCPF()
        {
            return CPF.IsNotNullOrEmpty();
        }

        private string SerializeIE()
        {
            var ie = IE.TrimVip().ToUpper();
            return ie.Equals(DFeConstantes.Isento) ? DFeConstantes.Isento : ie.OnlyNumbers();
        }

        public object DeserializeIE(string value)
        {
            return value;
        }

        #endregion
    }
}