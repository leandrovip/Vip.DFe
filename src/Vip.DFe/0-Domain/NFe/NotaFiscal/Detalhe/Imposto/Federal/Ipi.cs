using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     Grupo IPI - Informar apenas quando o item for sujeito ao IPI
    /// </summary>
    public sealed class Ipi : GenericClone<Ipi>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     O02 - Classe de enquadramento do IPI para Cigarros e Bebidas
        ///     Versão 3.10
        /// </summary>
        [DFeElement(TipoCampo.Str, "clEnq", Id = "O02", Min = 1, Max = 5, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string ClEnq { get; set; }

        /// <summary>
        ///     O03 - CNPJ do produtor da mercadoria, quando diferente do emitente. Somente para os casos de exportação direta ou
        ///     indireta.
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJProd", Id = "O03", Min = 14, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CnpjProd { get; set; }

        /// <summary>
        ///     O04 - Código do selo de controle IPI
        /// </summary>
        [DFeElement(TipoCampo.Str, "cSelo", Id = "O04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CSelo { get; set; }

        /// <summary>
        ///     O05 - Quantidade de selo de controle
        /// </summary>
        [DFeElement(TipoCampo.Int, "qSelo", Id = "O05", Min = 1, Max = 12, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int QSelo { get; set; }

        /// <summary>
        ///     O06 - Código de Enquadramento Legal do IPI
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "cEnq", Id = "O06", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CEnq { get; set; }

        /// <summary>
        ///     O07 (IPITrib) - Grupo do CST 00, 49, 50 e 99
        ///     O08 (IPINT) - Grupo CST 01, 02, 03, 04, 51, 52, 53, 54 e 55
        /// </summary>
        [DFeItem(typeof(IpiTrib), "IPITrib")]
        [DFeItem(typeof(IpiNt), "IPINT")]
        public INFeIpi Imposto { get; set; }

        #endregion
    }
}