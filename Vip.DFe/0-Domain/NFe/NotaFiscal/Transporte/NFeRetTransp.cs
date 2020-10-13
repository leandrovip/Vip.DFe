using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Transporte
{
    /// <summary>
    ///     Grupo Retenção ICMS transporte
    /// </summary>
    public sealed class NFeRetTransp : GenericClone<NFeRetTransp>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     X12 - Valor do Serviço
        /// </summary>
        [DFeElement(TipoCampo.De2, "vServ", Id = "X12", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VServ { get; set; }

        /// <summary>
        ///     X13 - BC da Retenção do ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCRet", Id = "X13", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBCRet { get; set; }

        /// <summary>
        ///     X14 - Alíquota da Retenção
        /// </summary>
        [DFeElement(TipoCampo.De4, "pICMSRet", Id = "X14", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIcmsRet { get; set; }

        /// <summary>
        ///     X15 - Valor do ICMS Retido
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSRet", Id = "X15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcmsRet { get; set; }

        /// <summary>
        ///     X16 - CFOP
        /// </summary>
        [DFeElement(TipoCampo.Int, "CFOP", Id = "X16", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int Cfop { get; set; }

        /// <summary>
        ///     X17 - Código do município de ocorrência do fato gerador do ICMS do transporte
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "cMunFG", Id = "X17", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CMunFG { get; set; }

        #endregion
    }
}