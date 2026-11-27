namespace Vip.DFe.Danfe.Modelo
{
    public class ProdutoViewModel
    {
        #region Constructors

        public ProdutoViewModel()
        {
            AliquotaIpi = null;
            ValorIpi = null;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>Código do produto ou serviço</para>
        ///     <para>Tag cProd</para>
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        ///     <para>Informações Adicionais do Produto</para>
        ///     <para>Tag infAdProd</para>
        /// </summary>
        public string InformacoesAdicionais { get; set; }

        /// <summary>
        ///     <para>Descrição do produto ou serviço</para>
        ///     <para>Tag xProd</para>
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        ///     <para>Código NCM com 8 dígitos ou 2 dígitos (gênero)</para>
        ///     <para>Tag NCM</para>
        /// </summary>
        public string Ncm { get; set; }

        /// <summary>
        ///     <para>Origem da mercadoria + Tributação do ICMS</para>
        ///     <para>Tag orig e CST</para>
        /// </summary>
        public string OCst { get; set; }

        /// <summary>
        ///     <para>Código Fiscal de Operações e Prestações</para>
        ///     <para>Tag CFOP</para>
        /// </summary>
        public int Cfop { get; set; }

        /// <summary>
        ///     <para>Unidade Comercial</para>
        ///     <para>Tag uCom</para>
        /// </summary>
        public string Unidade { get; set; }

        /// <summary>
        ///     <para>Quantidade Comercial</para>
        ///     <para>Tag qCom</para>
        /// </summary>
        public double Quantidade { get; set; }

        /// <summary>
        ///     <para>Valor Unitário de Comercialização</para>
        ///     <para>Tag vUnCom</para>
        /// </summary>
        public double ValorUnitario { get; set; }

        /// <summary>
        ///     <para>Valor Total Bruto dos Produtos ou Serviços</para>
        ///     <para>Tag vProd</para>
        /// </summary>
        public double ValorTotal { get; set; }

        /// <summary>
        ///     <para>Valor da BC do ICMS</para>
        ///     <para>Tag vBC</para>
        /// </summary>
        public double BaseIcms { get; set; }

        /// <summary>
        ///     <para>Valor do ICMS</para>
        ///     <para>Tag vICMS</para>
        /// </summary>
        public double ValorIcms { get; set; }

        /// <summary>
        ///     <para>Alíquota do imposto</para>
        ///     <para>Tag pICMS</para>
        /// </summary>
        public double AliquotaIcms { get; set; }

        /// <summary>
        ///     <para>Valor do IPI</para>
        ///     <para>Tag vIPI</para>
        /// </summary>
        public double? ValorIpi { get; set; }

        /// <summary>
        ///     <para>Alíquota do IPI</para>
        ///     <para>Tag pIPI</para>
        /// </summary>
        public double? AliquotaIpi { get; set; }

        /// <summary>
        ///     <para>Valor aproximado total de tributos federais, estaduais e municipais. [NT2013.003]</para>
        ///     <para>Tag vTotTrib</para>
        /// </summary>
        public double? ValorAproximadoTributos { get; set; }

        #region Reforma Tributária (NT 2026.010)

        /// <summary>
        ///     <para>Valor da BC do IPI</para>
        ///     <para>Tag vBC (O10)</para>
        /// </summary>
        public double? BaseIpi { get; set; }

        /// <summary>
        ///     <para>Código de Classificação Tributária do IBS e da CBS</para>
        ///     <para>Tag cClassTrib (UB14)</para>
        /// </summary>
        public string ClassificacaoTributaria { get; set; }

        /// <summary>
        ///     <para>Base de cálculo do IBS e da CBS</para>
        ///     <para>Tag vBC (UB16)</para>
        /// </summary>
        public double? BaseIbsCbs { get; set; }

        /// <summary>
        ///     <para>Alíquota do IBS UF impressa no DANFE: pAliqEfet (UB28) quando gIBSUF/gRed informado; senão pIBSUF (UB18)</para>
        /// </summary>
        public double? AliquotaIbsUf { get; set; }

        /// <summary>
        ///     <para>Valor do IBS UF</para>
        ///     <para>Tag vIBSUF (UB35)</para>
        /// </summary>
        public double? ValorIbsUf { get; set; }

        /// <summary>
        ///     <para>Alíquota do IBS Município impressa no DANFE: pAliqEfet (UB47) quando gIBSMun/gRed informado; senão pIBSMun (UB37)</para>
        /// </summary>
        public double? AliquotaIbsMunicipio { get; set; }

        /// <summary>
        ///     <para>Valor do IBS Município</para>
        ///     <para>Tag vIBSMun (UB54)</para>
        /// </summary>
        public double? ValorIbsMunicipio { get; set; }

        /// <summary>
        ///     <para>Alíquota da CBS impressa no DANFE: pAliqEfet (UB66) quando gCBS/gRed informado; senão pCBS (UB56)</para>
        /// </summary>
        public double? AliquotaCbs { get; set; }

        /// <summary>
        ///     <para>Valor da CBS</para>
        ///     <para>Tag vCBS (UB67)</para>
        /// </summary>
        public double? ValorCbs { get; set; }

        /// <summary>
        ///     <para>Base de cálculo do Imposto Seletivo</para>
        ///     <para>Tag vBCIS (UB05)</para>
        /// </summary>
        public double? BaseIs { get; set; }

        /// <summary>
        ///     <para>Alíquota do Imposto Seletivo</para>
        ///     <para>Tag pIS (UB06)</para>
        /// </summary>
        public double? AliquotaIs { get; set; }

        /// <summary>
        ///     <para>Valor do Imposto Seletivo</para>
        ///     <para>Tag vIS (UB11)</para>
        /// </summary>
        public double? ValorIs { get; set; }

        #endregion

        public string DescricaoCompleta
        {
            get
            {
                var descriCaoCompleta = Descricao;

                if (!string.IsNullOrWhiteSpace(InformacoesAdicionais)) descriCaoCompleta += "\r\n" + BreakLines(InformacoesAdicionais);

                return descriCaoCompleta;
            }
        }

        #endregion

        #region Methods Privates

        private string BreakLines(string str) => str == null ? string.Empty : str.Replace(';', '\n');

        #endregion
    }
}