using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Transporte
{
    public sealed class NFeLacresTransp : GenericClone<NFeLacresTransp>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     X34 - Número dos Lacres
        /// </summary>
        [DFeElement(TipoCampo.Str, "nLacre", Id = "X34", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NLacre { get; set; }

        #endregion
    }
}