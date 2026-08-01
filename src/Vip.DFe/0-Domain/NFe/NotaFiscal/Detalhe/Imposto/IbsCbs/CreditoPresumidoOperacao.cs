using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo do Crédito Presumido da Operação
    /// </summary>
    public class CreditoPresumidoOperacao : GenericClone<CreditoPresumidoOperacao>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB121 - Valor da Base de Cálculo do Crédito Presumido da Operação
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCCredPres", Id = "UB121", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcCredPres { get; set; }

        /// <summary>
        ///     UB122 - Código de Classificação do Crédito Presumido do IBS e da CBS
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "cCredPres", Id = "UB122", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CCredPres { get; set; }

        /// <summary>
        ///     UB123 - Grupo de Informações do Crédito Presumido referente ao IBS, quando aproveitado pelo emitente do documento
        /// </summary>
        [DFeElement("gIBSCredPres", Id = "UB123", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CreditoPresumidoTributo IbsCredPres { get; set; }

        /// <summary>
        ///     UB127 - Grupo de Informações do Crédito Presumido referente a CBS, quando aproveitado pelo emitente do documento
        /// </summary>
        [DFeElement("gCBSCredPres", Id = "UB127", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CreditoPresumidoTributo CbsCredPres { get; set; }

        #endregion
    }
}