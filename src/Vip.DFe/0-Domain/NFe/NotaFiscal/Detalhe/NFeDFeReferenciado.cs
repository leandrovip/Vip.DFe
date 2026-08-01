using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    /// <summary>
    ///     VC01 - Grupo de Documento Fiscal Referenciado no item
    /// </summary>
    public class NFeDFeReferenciado : GenericClone<NFeDFeReferenciado>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     VC02 - Chave de acesso do documento fiscal referenciado
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "chaveAcesso", Id = "VC02", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string ChaveAcesso { get; set; }

        /// <summary>
        ///     VC03 - Número do item no documento fiscal referenciado
        /// </summary>
        [DFeElement(TipoCampo.Int, "nItem", Id = "VC03", Min = 1, Max = 3, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int NItem { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeNItem()
        {
            return NItem > 0;
        }

        #endregion
    }
}