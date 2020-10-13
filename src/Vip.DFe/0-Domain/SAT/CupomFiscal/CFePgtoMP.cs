using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.SAT.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo de informa��es dos Meios de Pagamento empregados na quita��o do CF-e
    /// </summary>
    public sealed class CFePgtoMp : GenericClone<CFePgtoMp>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        ///     C�digo do Meio de Pagamento empregado para quita��o do CF-e
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cMP", Id = "WA04", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public MeioPagamento CMp { get; set; }

        /// <summary>
        ///     Valor do Meio de Pagamento empregado para quita��o do CF-e
        /// </summary>
        [DFeElement(TipoCampo.De2, "vMP", Id = "WA05", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VMp { get; set; }

        /// <summary>
        ///     Credenciadora de cart�o de d�bito ou cr�dito
        /// </summary>
        [DFeElement(TipoCampo.Int, "cAdmC", Id = "WA06", Min = 3, Max = 3, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? CAdmC { get; set; }

        #endregion Properties

        #region Methods

        private bool ShouldSerializeCAdmC()
        {
            return CAdmC != null;
        }

        #endregion Methods
    }
}