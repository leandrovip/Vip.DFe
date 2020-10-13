using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.SAT.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    /// <summary>
    ///     Campo cRegTrib=1 – Simples Nacional e CSOSN = 900
    /// </summary>
    public sealed class ImpostoIcmsSn900 : GenericClone<ImpostoIcmsSn900>, ICFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        /// <summary>
        ///     Origem da mercadoria
        /// </summary>
        [DFeElement(TipoCampo.Enum, "Orig", Id = "N06", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public OrigemMercadoria Orig { get; set; }

        /// <summary>
        ///     Código de Situação da Operação – SIMPLES NACIONAL
        /// </summary>
        [DFeElement(TipoCampo.Str, "CSOSN", Id = "N10", Min = 3, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Csosn { get; set; }

        /// <summary>
        ///     Alíquota efetiva do imposto
        /// </summary>
        [DFeElement(TipoCampo.De2, "pICMS", Id = "N08", Min = 3, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIcms { get; set; }

        /// <summary>
        ///     Valor do ICMS
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMS", Id = "N09", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcms { get; set; }

        #endregion Propriedades
    }
}