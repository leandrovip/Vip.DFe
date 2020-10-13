using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.SAT.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    /// <summary>
    ///     Classe para os imposto com CSOSN 102, 300 ou 500.
    /// </summary>
    public sealed class ImpostoIcmsSn102 : GenericClone<ImpostoIcmsSn102>, ICFeIcms
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
        ///     Código de Situação da Operação – Simples Nacional (300, 400, 500)
        /// </summary>
        [DFeElement(TipoCampo.Str, "CSOSN", Id = "N10", Min = 3, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Csosn { get; set; }

        #endregion Properties
    }
}