using Vip.Fiscal.Enums;
using Vip.Fiscal.Interfaces;

namespace Vip.DFe.Demo.Models
{
    public sealed class ItemTributavelDemo : IEntidadeProduto
    {
        public TipoDocumento TipoDocumento { get; set; } = TipoDocumento.NFe;
        public decimal ValorItem { get; set; }
        public decimal QuantidadeItem { get; set; }
        public bool Servico { get; set; }
        public bool AtivoImobilizadoOuUsoeConsumo { get; set; }
        public decimal Frete { get; set; }
        public decimal Seguro { get; set; }
        public decimal OutrasDespesas { get; set; }
        public decimal Desconto { get; set; }

        public Cst Cst { get; set; }
        public Csosn Csosn { get; set; }
        public decimal PercentualIcms { get; set; }
        public decimal PercentualReducao { get; set; }
        public decimal PercentualCredito { get; set; }
        public decimal PercentualIcmsSt { get; set; }
        public decimal PercentualMva { get; set; }
        public decimal PercentualReducaoSt { get; set; }
        public decimal PercentualDiferimento { get; set; }
        public decimal PercentualDifalInterna { get; set; }
        public decimal PercentualDifalInterestadual { get; set; }
        public decimal PercentualFcp { get; set; }
        public decimal PercentualFcpSt { get; set; }
        public decimal PercentualFcpStRetido { get; set; }
        public decimal ValorUltimaBaseCalculoIcmsStRetido { get; set; }
        public decimal PercentualOriginarioUf { get; set; }
        public decimal QuantidadeBaseCalculoIcmsMonofasico { get; set; }
        public decimal QuantidadeBaseCalculoIcmsMonofasicoRetencao { get; set; }
        public decimal QuantidadeBaseCalculoIcmsMonofasicoRetidoAnteriormente { get; set; }
        public decimal AliquotaAdRemIcms { get; set; }
        public decimal AliquotaAdRemIcmsretencao { get; set; }
        public decimal AliquotaAdRemIcmsRetidoAnteriormente { get; set; }
        public decimal PercentualReducaoAliquotaAdRemIcms { get; set; }
        public decimal PercentualIcmsMonofasicoDiferido { get; set; }
        public bool CalcularIcmsEfetivoeRetencaoParaSt { get; set; }
        public decimal PercentualIcmsEfetivo { get; set; }
        public decimal PercentualReducaoIcmsEfetivo { get; set; }

        public CstPisCofins CstPisCofins { get; set; }
        public decimal PercentualPis { get; set; }
        public decimal PercentualCofins { get; set; }
        public bool DeduzIcmsDaBaseDePisCofins { get; set; }
        public decimal PercentualReducaoPis { get; set; }
        public decimal PercentualReducaoCofins { get; set; }
        public decimal PercentualBiodisel { get; set; }

        public CstIpi CstIpi { get; set; }
        public decimal ValorIpi { get; set; }
        public decimal PercentualIpi { get; set; }

        public decimal PercentualIssqn { get; set; }
        public decimal PercentualRetPis { get; set; }
        public decimal PercentualRetCofins { get; set; }
        public decimal PercentualRetCsll { get; set; }
        public decimal PercentualRetIrrf { get; set; }
        public decimal PercentualRetInss { get; set; }

        public CstIbsCbs CstIbsCbs { get; set; }
        public decimal PercentualIbsUF { get; set; }
        public decimal PercentualIbsMunicipal { get; set; }
        public decimal PercentualCbs { get; set; }

        public static ItemTributavelDemo From(Item item)
        {
            return new ItemTributavelDemo
            {
                TipoDocumento = TipoDocumento.NFe,
                ValorItem = item.ValorItem,
                QuantidadeItem = item.Quantidade,
                Frete = item.Frete,
                Seguro = item.Seguro,
                OutrasDespesas = item.Outros,
                Desconto = item.Desconto,
                Servico = false,
                AtivoImobilizadoOuUsoeConsumo = false,

                Cst = Cst.Cst00,
                Csosn = Csosn.Csosn102,
                PercentualIcms = 18.00m,

                CstPisCofins = CstPisCofins.Cst01,
                PercentualPis = 1.65m,
                PercentualCofins = 7.60m,
                DeduzIcmsDaBaseDePisCofins = false,

                CstIbsCbs = CstIbsCbs.Cst000,
                PercentualIbsUF = 0.10m,
                PercentualCbs = 0.90m
            };
        }
    }
}
