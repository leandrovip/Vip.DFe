using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;

namespace Vip.DFe.NFe.NotaFiscal.Cobranca
{
    public sealed class NFeCobranca : GenericClone<NFeCobranca>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeCobranca()
        {
            Fatura = new NFeCobrFatura();
            Duplicata = new DFeCollection<NFeCobrDuplicata>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Y02 - Grupo Fatura
        /// </summary>
        [DFeElement("fat", Id = "Y02", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeCobrFatura Fatura { get; set; }

        /// <summary>
        ///     Y07 - Grupo Duplicata
        ///     <para>Ocorrência: 0-120</para>
        /// </summary>
        [DFeCollection("dup", Id = "Y07", MinSize = 0, MaxSize = 120, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeCobrDuplicata> Duplicata { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeFatura()
        {
            return Fatura.NFat.IsNotNullOrEmpty() || Fatura.VOrig > 0 || Fatura.VDesc > 0 || Fatura.VLiq > 0;
        }

        #endregion
    }
}