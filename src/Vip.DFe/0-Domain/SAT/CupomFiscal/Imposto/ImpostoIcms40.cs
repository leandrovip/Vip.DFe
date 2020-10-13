using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.SAT.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    /// <summary>
    ///     Classe ImpostoICMS40. Classe para os imposto com Cst 40, 41, 50 ou 60.
    /// </summary>
    public sealed class ImpostoIcms40 : GenericClone<ImpostoIcms40>, ICFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        ///     Origem da mercadoria
        /// </summary>
        [DFeElement(TipoCampo.Enum, "Orig", Id = "N06", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public OrigemMercadoria Orig { get; set; }

        /// <summary>
        ///     Tributação do ICMS = 40, 41, 60
        /// </summary>
        [DFeElement(TipoCampo.Str, "CST", Id = "N07", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        #endregion Properties
    }
}