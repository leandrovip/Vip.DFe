using Vip.DFe.Demo.Models;
using Vip.Fiscal.Interfaces.Resultados;

namespace Vip.DFe.Demo.Services
{
    public sealed class ResultadoTributacaoDemo
    {
        public Item Item { get; set; }
        public ItemTributavelDemo Entidade { get; set; }
        public Vip.Fiscal.TributacaoProduto Produto { get; set; }
        public IResultadoIbs IbsUf { get; set; }
        public IResultadoIbsMunicipal IbsMunicipal { get; set; }
        public IResultadoCbs Cbs { get; set; }

        public decimal BaseIbsCbs => IbsUf?.BaseCalculo ?? IbsMunicipal?.BaseCalculo ?? Cbs?.BaseCalculo ?? 0;
        public decimal ValorIbsUf => IbsUf?.Valor ?? 0;
        public decimal ValorIbsMunicipal => IbsMunicipal?.Valor ?? 0;
        public decimal ValorIbs => ValorIbsUf + ValorIbsMunicipal;
        public decimal ValorCbs => Cbs?.Valor ?? 0;
    }
}
