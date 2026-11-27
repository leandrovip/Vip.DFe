namespace Vip.DFe.Danfe.Modelo
{
    public class CalculoImpostoViewModel
    {
        #region Propriedades

        /// <summary>
        ///     <para>Base de Cálculo do ICMS</para>
        ///     <para>Tag vBC</para>
        /// </summary>
        public double BaseCalculoIcms { get; set; }

        /// <summary>
        ///     <para>Valor Total do ICMS</para>
        ///     <para>Tag vICMS</para>
        /// </summary>
        public double ValorIcms { get; set; }

        /// <summary>
        ///     <para>Tag vICMSUFDest</para>
        /// </summary>
        public double? vICMSUFDest { get; set; }

        /// <summary>
        ///     <para>Tag vICMSUFRemet</para>
        /// </summary>
        public double? vICMSUFRemet { get; set; }

        /// <summary>
        ///     <para>Tag vFCPUFDest</para>
        /// </summary>
        public double? vFCPUFDest { get; set; }

        /// <summary>
        ///     <para>Base de Cálculo do ICMS ST</para>
        ///     <para>Tag vBCST</para>
        /// </summary>
        public double BaseCalculoIcmsSt { get; set; }

        /// <summary>
        ///     <para>Valor Total do ICMS ST</para>
        ///     <para>Tag vST</para>
        /// </summary>
        public double ValorIcmsSt { get; set; }

        /// <summary>
        ///     <para>Valor Total dos produtos e serviços</para>
        ///     <para>Tag vProd</para>
        /// </summary>
        public double? ValorTotalProdutos { get; set; }

        /// <summary>
        ///     <para>Valor Total do Frete</para>
        ///     <para>Tag vFrete</para>
        /// </summary>
        public double ValorFrete { get; set; }

        /// <summary>
        ///     <para>Valor Total do Seguro</para>
        ///     <para>Tag vSeg</para>
        /// </summary>
        public double ValorSeguro { get; set; }

        /// <summary>
        ///     <para>Valor Total do Desconto </para>
        ///     <para>Tag vDesc</para>
        /// </summary>
        public double Desconto { get; set; }

        /// <summary>
        ///     <para>Outras Despesas acessórias</para>
        ///     <para>Tag vOutro</para>
        /// </summary>
        public double OutrasDespesas { get; set; }

        /// <summary>
        ///     Valor do imposto de importação.
        /// </summary>
        public double ValorII { get; set; }

        /// <summary>
        ///     <para>Valor Total do IPI</para>
        ///     <para>Tag vIPI</para>
        /// </summary>
        public double ValorIpi { get; set; }

        /// <summary>
        ///     Valor do PIS
        /// </summary>
        public double ValorPis { get; set; }

        /// <summary>
        ///     Valor do COFINS
        /// </summary>
        public double ValorCofins { get; set; }

        /// <summary>
        ///     <para>Valor Total da NF-e </para>
        ///     <para>Tag vNF</para>
        /// </summary>
        public double ValorTotalNota { get; set; }

        /// <summary>
        ///     <para>Valor aproximado total de tributos federais, estaduais e municipais (NT 2013.003)</para>
        ///     <para>Tag vTotTrib</para>
        /// </summary>
        public double? ValorAproximadoTributos { get; set; }

        #endregion

        #region ICMS facultativos (NT 2026.010)

        /// <summary>
        ///     <para>Valor Total do FCP</para>
        ///     <para>Tag vFCP (W04h)</para>
        /// </summary>
        public double? ValorFcp { get; set; }

        /// <summary>
        ///     <para>Valor Total do FCP retido por substituição tributária</para>
        ///     <para>Tag vFCPST (W06a)</para>
        /// </summary>
        public double? ValorFcpSt { get; set; }

        /// <summary>
        ///     <para>Quantidade tributada do ICMS monofásico próprio</para>
        ///     <para>Tag qBCMono (W06b.1)</para>
        /// </summary>
        public double? BaseCalculoIcmsMono { get; set; }

        /// <summary>
        ///     <para>Valor total do ICMS monofásico próprio</para>
        ///     <para>Tag vICMSMono (W06c)</para>
        /// </summary>
        public double? ValorIcmsMono { get; set; }

        /// <summary>
        ///     <para>Quantidade tributada do ICMS monofásico sujeito a retenção</para>
        ///     <para>Tag qBCMonoReten (W06c.1)</para>
        /// </summary>
        public double? BaseCalculoIcmsMonoRetencao { get; set; }

        /// <summary>
        ///     <para>Valor total do ICMS monofásico sujeito a retenção</para>
        ///     <para>Tag vICMSMonoReten (W06d)</para>
        /// </summary>
        public double? ValorIcmsMonoRetencao { get; set; }

        #endregion

        #region IBS / CBS / IS (NT 2026.010)

        /// <summary>
        ///     <para>Valor total da CBS</para>
        ///     <para>Tag vCBS (W56)</para>
        /// </summary>
        public double? ValorCbs { get; set; }

        /// <summary>
        ///     <para>Valor total do IBS da UF</para>
        ///     <para>Tag vIBSUF (W41)</para>
        /// </summary>
        public double? ValorIbsUf { get; set; }

        /// <summary>
        ///     <para>Valor total do IBS do Município</para>
        ///     <para>Tag vIBSMun (W46)</para>
        /// </summary>
        public double? ValorIbsMunicipio { get; set; }

        /// <summary>
        ///     <para>Valor total do Imposto Seletivo</para>
        ///     <para>Tag vIS (W33)</para>
        /// </summary>
        public double? ValorIs { get; set; }

        /// <summary>
        ///     <para>Valor total do IBS monofásico</para>
        ///     <para>Tag vIBSMono (W58)</para>
        /// </summary>
        public double? ValorIbsMono { get; set; }

        /// <summary>
        ///     <para>Valor total da CBS monofásica</para>
        ///     <para>Tag vCBSMono (W59)</para>
        /// </summary>
        public double? ValorCbsMono { get; set; }

        /// <summary>
        ///     <para>Valor total do IBS monofásico sujeito a retenção</para>
        ///     <para>Tag vIBSMonoReten (W59a)</para>
        /// </summary>
        public double? ValorIbsMonoRetencao { get; set; }

        /// <summary>
        ///     <para>Valor total da CBS monofásica sujeita a retenção</para>
        ///     <para>Tag vCBSMonoReten (W59b)</para>
        /// </summary>
        public double? ValorCbsMonoRetencao { get; set; }

        #endregion
    }
}