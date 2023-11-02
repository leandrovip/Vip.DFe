using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    public sealed class NFeIcmsTot : GenericClone<NFeIcmsTot>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     W03 - Base de Cálculo do ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "W03", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     W04 - Valor Total do ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMS", Id = "W04", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcms { get; set; }

        /// <summary>
        ///     W04a - Valor Total do ICMS desonerado
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSDeson", Id = "W04a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcmsDeson { get; set; }

        /// <summary>
        ///     W04c - Valor total do ICMS relativo Fundo de Combate à Pobreza(FCP) da UF de destino
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFCPUFDest", Id = "W04c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VFcpUfDest { get; set; }

        /// <summary>
        ///     W04e - Valor total do ICMS Interestadual para a UF de destino
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSUFDest", Id = "W04e", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcmsUfDest { get; set; }

        /// <summary>
        ///     W04g - Valor total do ICMS Interestadual para a UF do remetente
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSUFRemet", Id = "W04g", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcmsUfRemet { get; set; }

        /// <summary>
        ///     W04h - Valor Total do FCP (Fundo de Combate à Pobreza)
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFCP", Id = "W04h", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VFcp { get; set; }

        /// <summary>
        ///     W05 - Base de Cálculo do ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCST", Id = "W05", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcSt { get; set; }

        /// <summary>
        ///     W06 - Valor Total do ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.De2, "vST", Id = "W06", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VSt { get; set; }

        /// <summary>
        ///     W06a - Valor Total do FCP (Vundo de Combate à Pobreza) retido por substituição tributária
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFCPST", Id = "W06a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VFcpSt { get; set; }

        /// <summary>
        ///     W06b - Valor Total do FCP retido anteriormente por Substituição Tributária
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFCPSTRet", Id = "W06b", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VFcpStRet { get; set; }

        /// <summary>
        ///     W06b.1 - Quantidade tributada do ICMS monofásico próprio
        /// </summary>
        [DFeElement(TipoCampo.De2, "qBCMono", Id = "W06b.1", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QtdeBCMono { get; set; }

        /// <summary>
        ///     W06c - Valor do ICMS monofásico próprio
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSMono", Id = "W06c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VICMSMono { get; set; }

        /// <summary>
        ///     W06c.1 - Quantidade tributada do ICMS monofásico sujeito a retenção
        /// </summary>
        [DFeElement(TipoCampo.De2, "qBCMonoReten", Id = "W06b.1", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QtdeBCMonoReten { get; set; }

        /// <summary>
        ///     W06d - Valor do ICMS monofásico próprio sujeito a retenção
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSMonoReten", Id = "W06d", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VICMSMonoReten { get; set; }

        /// <summary>
        ///     W06d.1 - Quantidade tributada do ICMS monofásico retido anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De2, "qBCMonoRet", Id = "W06d.1", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QtdeBCMonoRet { get; set; }

        /// <summary>
        ///     W06e - Valor do ICMS monofásico próprio retido anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSMonoRet", Id = "W06e", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VICMSMonoRet { get; set; }

        /// <summary>
        ///     W07 - Valor Total dos produtos e serviços
        /// </summary>
        [DFeElement(TipoCampo.De2, "vProd", Id = "W07", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VProd { get; set; }

        /// <summary>
        ///     W08 - Valor Total do Frete
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFrete", Id = "W08", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VFrete { get; set; }

        /// <summary>
        ///     W09 - Valor Total do Seguro
        /// </summary>
        [DFeElement(TipoCampo.De2, "vSeg", Id = "W09", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VSeg { get; set; }

        /// <summary>
        ///     W10 - Valor Total do Desconto
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDesc", Id = "W10", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDesc { get; set; }

        /// <summary>
        ///     W11 - Valor Total do II
        /// </summary>
        [DFeElement(TipoCampo.De2, "vII", Id = "W11", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VII { get; set; }

        /// <summary>
        ///     W12 - Valor Total do IPI
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIPI", Id = "W12", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIpi { get; set; }

        /// <summary>
        ///     W12a - Valor Total do IPI devolvido
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIPIDevol", Id = "W12a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIpiDevol { get; set; }

        /// <summary>
        ///     W13 - Valor do PIS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPIS", Id = "W13", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPis { get; set; }

        /// <summary>
        ///     W14 - Valor da COFINS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINS", Id = "W14", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCofins { get; set; }

        /// <summary>
        ///     W15 - Outras Despesas acessórias
        /// </summary>
        [DFeElement(TipoCampo.De2, "vOutro", Id = "W15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VOutro { get; set; }

        /// <summary>
        ///     w16 - Valor Total da NF-e
        /// </summary>
        [DFeElement(TipoCampo.De2, "vNF", Id = "w16", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VNf { get; set; }

        /// <summary>
        ///     W16a - Valor aproximado total de tributos federais, estaduais e municipais.
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTotTrib", Id = "W16a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VTotTrib { get; set; }

        #endregion
    }
}