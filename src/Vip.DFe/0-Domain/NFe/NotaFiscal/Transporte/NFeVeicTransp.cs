using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Transporte
{
    /// <summary>
    ///     Grupo Veículo Transporte
    /// </summary>
    public sealed class NFeVeicTransp : GenericClone<NFeVeicTransp>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     X19 - Placa do Veículo
        /// </summary>
        [DFeElement(TipoCampo.Str, "placa", Id = "X19", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Placa { get; set; }

        /// <summary>
        ///     X20 - Sigla da UF
        /// </summary>
        [DFeElement(TipoCampo.Str, "UF", Id = "X20", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UF { get; set; }

        /// <summary>
        ///     X21 - Registro Nacional de Transportador de Carga (ANTT)
        /// </summary>
        [DFeElement(TipoCampo.Str, "RNTC", Id = "X21", Min = 1, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string RNTC { get; set; }

        #endregion
    }
}