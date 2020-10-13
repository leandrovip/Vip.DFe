using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.InformacaoAdicional
{
    public sealed class NFeObsCont : GenericClone<NFeObsCont>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Z05 - Identificação do campo
        /// </summary>
        [DFeAttribute(TipoCampo.Str, "xCampo", Id = "Z05", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XCampo { get; set; }

        /// <summary>
        ///     Z06 - Conteúdo do campo
        /// </summary>
        [DFeElement(TipoCampo.Str, "xTexto", Id = "Z06", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XTexto { get; set; }

        #endregion
    }
}