using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.InformacaoAdicional
{
    /// <summary>
    ///     Grupo Processo referenciado
    /// </summary>
    public sealed class NFeProcRef : GenericClone<NFeProcRef>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Z11 - Identificador do processo ou ato concessório
        /// </summary>
        [DFeElement(TipoCampo.Str, "nProc", Id = "Z11", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NProc { get; set; }

        /// <summary>
        ///     Z12 - Indicador da origem do processo
        /// </summary>
        [DFeElement(TipoCampo.Enum, "indProc", Id = "Z12", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeIndProcesso IndProc { get; set; }

        #endregion
    }
}