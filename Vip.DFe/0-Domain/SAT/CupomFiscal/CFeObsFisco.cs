using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo do campo de uso livre do Fisco
    ///     <br /><b>Campo preenchido pelo SAT</b>
    /// </summary>
    public sealed class CFeObsFisco : GenericClone<CFeObsFisco>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        ///     Identificação do campo
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeAttribute("xCampo", Id = "Z04", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XCampo { get; set; }

        /// <summary>
        ///     Conteúdo do campo
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.Str, "xTexto", Id = "Z05", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XTexto { get; set; }

        #endregion Properties
    }
}