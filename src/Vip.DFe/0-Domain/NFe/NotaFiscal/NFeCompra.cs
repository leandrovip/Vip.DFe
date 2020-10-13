using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal
{
    /// <summary>
    ///     Grupo Compra
    /// </summary>
    public sealed class NFeCompra : GenericClone<NFeCompra>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     ZB02 - Nota de Empenho
        /// </summary>
        [DFeElement(TipoCampo.Str, "xNEmp", Id = "ZB02", Min = 1, Max = 22, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XNEmp { get; set; }

        /// <summary>
        ///     ZB03 - Pedido
        /// </summary>
        [DFeElement(TipoCampo.Str, "xPed", Id = "ZB03", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XPed { get; set; }

        /// <summary>
        ///     ZB04 - Contrato
        /// </summary>
        [DFeElement(TipoCampo.Str, "xCont", Id = "ZB04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XCont { get; set; }

        #endregion
    }
}