using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    /// <summary>
    ///     NB01 - Grupo do ICMS Previsto no Pagamento Antecipado (NT 2026.008).
    ///     <br /> Grupo utilizado exclusivamente em cenários específicos de pagamento antecipado.
    /// </summary>
    public sealed class IcmsPrevistoPagtoAntecip : GenericClone<IcmsPrevistoPagtoAntecip>, INFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     NB02 - Valor do ICMS que incidirá no momento do fornecimento futuro
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSPrevisto", Id = "NB02", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcmsPrevisto { get; set; }

        #endregion
    }
}