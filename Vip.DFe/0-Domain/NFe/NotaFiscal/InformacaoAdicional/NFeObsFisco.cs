using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.InformacaoAdicional
{
    /// <summary>
    ///     Grupo Campo de uso livre do Fisco
    /// </summary>
    public sealed class NFeObsFisco : GenericClone<NFeObsFisco>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Z08 - Identificação do campo
        /// </summary>
        [DFeElement(TipoCampo.Str, "xCampo", Id = "Z08", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XCampo { get; set; }

        /// <summary>
        ///     Z09 - Conteúdo do campo
        /// </summary>
        [DFeElement(TipoCampo.Str, "xTexto", Id = "Z09", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XTexto { get; set; }

        #endregion
    }
}