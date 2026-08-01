using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    /// <summary>
    ///     Grupo de observações de uso livre do contribuinte/Fisco para o item da NF-e
    ///     <br /> xCampo/xTexto - obsCont (VA02/VA03), obsFisco (VA04/VA05)
    /// </summary>
    public sealed class NFeDetObs : GenericClone<NFeDetObs>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Identificação do campo
        ///     <br /> VA02 (obsCont), VA04 (obsFisco)
        /// </summary>
        [DFeAttribute(TipoCampo.Str, "xCampo", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XCampo { get; set; }

        /// <summary>
        ///     Conteúdo do campo
        ///     <br /> VA03 (obsCont), VA05 (obsFisco)
        /// </summary>
        [DFeElement(TipoCampo.Str, "xTexto", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XTexto { get; set; }

        #endregion
    }
}