using Vip.DFe.Demo.Models;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;
using Vip.Fiscal;
using Vip.Fiscal.Enums;

namespace Vip.DFe.Demo.Services
{
    public sealed class TributacaoDemoService
    {
        public ResultadoTributacaoDemo Calcular(Item item, RegimeTributario regimeTributario, NFeDestinoOperacao destinoOperacao)
        {
            var entidade = ItemTributavelDemo.From(item);
            var crt = regimeTributario == RegimeTributario.SimplesNacional ? Crt.SimplesNacional : Crt.RegimeNormal;
            var tipoOperacao = destinoOperacao == NFeDestinoOperacao.Interestadual ? TipoOperacao.OperacaoInterestadual : TipoOperacao.OperacaoInterna;

            var produto = new TributacaoProduto(entidade, crt, tipoOperacao, TipoPessoa.Juridica).Calcular();
            var service = new TributacaoService(entidade);

            return new ResultadoTributacaoDemo
            {
                Item = item,
                Entidade = entidade,
                Produto = produto,
                IbsUf = service.ObterIbs(),
                IbsMunicipal = service.ObterIbsMunicipal(),
                Cbs = service.ObterCbs()
            };
        }
    }
}