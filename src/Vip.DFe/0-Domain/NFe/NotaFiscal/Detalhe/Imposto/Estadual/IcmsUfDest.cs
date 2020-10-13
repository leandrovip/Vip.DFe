using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    /// <summary>
    ///     Informação do ICMS Interestadual
    /// </summary>
    public class IcmsUfDest : GenericClone<IcmsUfDest>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     NA03 - Valor da BC do ICMS na UF de destino
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCUFDest", Id = "NA03", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcUfDest { get; set; }

        /// <summary>
        ///     NA04 - Valor da BC FCP na UF de destino
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCFCPUFDest", Id = "NA04", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBcFcpUfDest { get; set; }

        /// <summary>
        ///     NA05 - Percentual do ICMS relativo ao Fundo de Combate à Pobreza (FCP) na UF de destino
        /// </summary>
        [DFeElement(TipoCampo.De4, "pFCPUFDest", Id = "NA05", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PFcpUfDest { get; set; }

        /// <summary>
        ///     NA07 - Alíquota interna da UF de destino
        /// </summary>
        [DFeElement(TipoCampo.De4, "pICMSUFDest", Id = "NA07", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIcmsUfDest { get; set; }

        /// <summary>
        ///     NA09 - Alíquota interestadual das UF envolvidas
        /// </summary>
        [DFeElement(TipoCampo.De2, "pICMSInter", Id = "NA09", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIcmsInter { get; set; }

        /// <summary>
        ///     NA11 - Percentual provisório de partilha do ICMS Interestadual
        /// </summary>
        [DFeElement(TipoCampo.De4, "pICMSInterPart", Id = "NA11", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIcmsInterPart { get; set; }

        /// <summary>
        ///     NA13 - Valor do ICMS relativo ao Fundo de Combate à Pobreza(FCP) da UF de destino
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFCPUFDest", Id = "NA13", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VFcpUfDest { get; set; }

        /// <summary>
        ///     NA15 - Valor do ICMS Interestadual para a UF de destino
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSUFDest", Id = "NA15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcmsUfDest { get; set; }

        /// <summary>
        ///     NA17 - Valor do ICMS Interestadual para a UF do remetente
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSUFRemet", Id = "NA17", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcmsUfRemet { get; set; }

        #endregion
    }
}