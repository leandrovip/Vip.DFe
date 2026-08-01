using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Regular.
    ///     <br /> Informar como seria a tributação caso não cumprida a condição resolutória/suspensiva.
    /// </summary>
    public class TributacaoRegular : GenericClone<TributacaoRegular>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB69 - Código da Situação Tributária do IBS e CBS
        ///     <br /> Informar qual seria o CST caso não cumprida a condição resolutória/suspensiva
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CSTReg", Id = "UB69", Min = 3, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CstReg { get; set; }

        /// <summary>
        ///     UB70 - Informar qual seria o cClassTrib caso não cumprida a condição resolutória/suspensiva
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "cClassTribReg", Id = "UB70", Min = 6, Max = 6, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CClassTribReg { get; set; }

        /// <summary>
        ///     UB71 - Alíquota do IBS da UF
        ///     <br /> Informar como seria a Alíquota caso não cumprida a condição resolutória/suspensiva
        /// </summary>
        [DFeElement(TipoCampo.De4, "pAliqEfetRegIBSUF", Id = "UB71", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PAliqEfetRegIbsUf { get; set; }

        /// <summary>
        ///     UB72 - Valor do IBS da UF
        ///     <br /> Informar como seria o valor do Tributo caso não cumprida a condição resolutória/suspensiva
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTribRegIBSUF", Id = "UB72", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VTribRegIbsUf { get; set; }

        /// <summary>
        ///     UB72a - Alíquota do IBS do Município
        ///     <br /> Informar como seria a Alíquota caso não cumprida a condição resolutória/suspensiva
        /// </summary>
        [DFeElement(TipoCampo.De4, "pAliqEfetRegIBSMun", Id = "UB72a", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PAliqEfetRegIbsMun { get; set; }

        /// <summary>
        ///     UB72b - Valor do IBS do Município
        ///     <br /> Informar como seria o valor do Tributo caso não cumprida a condição resolutória/suspensiva
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTribRegIBSMun", Id = "UB72b", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VTribRegIbsMun { get; set; }

        /// <summary>
        ///     UB72c - Alíquota da CBS
        ///     <br /> Informar como seria a Alíquota caso não cumprida a condição resolutória/suspensiva
        /// </summary>
        [DFeElement(TipoCampo.De4, "pAliqEfetRegCBS", Id = "UB72c", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PAliqEfetRegCbs { get; set; }

        /// <summary>
        ///     UB72d - Valor da CBS
        ///     <br /> Informar como seria o valor do Tributo caso não cumprida a condição resolutória/suspensiva
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTribRegCBS", Id = "UB72d", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VTribRegCbs { get; set; }

        #endregion
    }
}