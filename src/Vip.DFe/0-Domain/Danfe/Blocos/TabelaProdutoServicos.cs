using System;
using System.Collections.Generic;
using System.Drawing;
using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Extensions;
using Vip.DFe.Graphics;
using Vip.DFe.Helpers;

namespace Vip.DFe.Danfe.Blocos
{
    internal class TabelaProdutosServicos : ElementoBase
    {
        #region Constructors

        public TabelaProdutosServicos(DanfeViewModel viewModel, Estilo estilo) : base(estilo)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            CabecalhoBloco = new CabecalhoBloco(estilo, "DADOS DOS PRODUTOS / SERVIÇOS", !ViewModel.UsarLeiauteAnterior);

            const AlinhamentoHorizontal ad = AlinhamentoHorizontal.Direita;
            const AlinhamentoHorizontal ac = AlinhamentoHorizontal.Centro;
            const AlinhamentoHorizontal ae = AlinhamentoHorizontal.Esquerda;

            Tabela = new Tabela(Estilo);

            if (!ViewModel.UsarLeiauteAnterior)
            {
                MontarTabelaRtc();
                return;
            }

            var cabecalho4 = ViewModel.Emitente.CRT == "3" ? "O/CST" : "O/CSOSN";

            if (ViewModel.Orientacao == Orientacao.Retrato)
                Tabela
                    .ComColuna(8.5f, ac, "CÓDIGO", "PRODUTO")
                    .ComColuna(0, ae, "DESCRIÇÃO DO PRODUTO / SERVIÇO")
                    .ComColuna(5.6F, ac, "NCM/SH")
                    .ComColuna(3.9F, ac, cabecalho4)
                    .ComColuna(3.5F, ac, "CFOP")
                    .ComColuna(3.25F, ac, "UN")
                    .ComColuna(6F, ad, "QUANTI.")
                    .ComColuna(6F, ad, "VALOR", "UNIT.")
                    .ComColuna(6F, ad, "VALOR", "TOTAL")
                    .ComColuna(6F, ad, "B CÁLC", "ICMS")
                    .ComColuna(5, ad, "VALOR", "ICMS")
                    .ComColuna(5, ad, "VALOR", "IPI")
                    .ComColuna(3.5F, ad, "ALIQ.", "ICMS")
                    .ComColuna(3.5F, ad, "ALIQ.", "IPI");
            if (ViewModel.Orientacao == Orientacao.Paisagem)
                Tabela
                    .ComColuna(8.1f, ac, "CÓDIGO PRODUTO")
                    .ComColuna(0, ae, "DESCRIÇÃO DO PRODUTO / SERVIÇO")
                    .ComColuna(5.5F, ac, "NCM/SH")
                    .ComColuna(3.1F, ac, cabecalho4)
                    .ComColuna(3.1F, ac, "CFOP")
                    .ComColuna(3F, ac, "UN")
                    .ComColuna(5.25F, ad, "QUANTI.")
                    .ComColuna(5.6F, ad, "VALOR UNIT.")
                    .ComColuna(5.6F, ad, "VALOR TOTAL")
                    .ComColuna(5.6F, ad, "B CÁLC ICMS")
                    .ComColuna(5.6F, ad, "VALOR ICMS")
                    .ComColuna(5.6F, ad, "VALOR IPI")
                    .ComColuna(3F, ad, "ALIQ.", "ICMS")
                    .ComColuna(3F, ad, "ALIQ.", "IPI");

            Tabela.AjustarLarguraColunas();

            foreach (var p in ViewModel.Produtos)
            {
                var linha = new List<string>
                {
                    p.Codigo,
                    p.DescricaoCompleta,
                    p.Ncm,
                    p.OCst,
                    p.Cfop.Formatar("N0"),
                    p.Unidade,
                    p.Quantidade.Formatar(),
                    p.ValorUnitario.Formatar(),
                    p.ValorTotal.Formatar(),
                    p.BaseIcms.Formatar(),
                    p.ValorIcms.Formatar(),
                    p.ValorIpi.Formatar(),
                    p.AliquotaIcms.Formatar(),
                    p.AliquotaIpi.Formatar()
                };

                Tabela.AdicionarLinha(linha);
            }
        }

        #endregion

        #region Properties

        public CabecalhoBloco CabecalhoBloco { get; private set; }
        public Tabela Tabela { get; private set; }
        public DanfeViewModel ViewModel { get; private set; }
        public RectangleF RetanguloTabela => BoundingBox.CutTop(EspacoAntesCabecalho + CabecalhoBloco.Height);

        /// <summary>
        ///     Respiro antes do título "Dados dos produtos / serviços" no leiaute da NT 2026.010.
        /// </summary>
        private float EspacoAntesCabecalho => ViewModel.UsarLeiauteAnterior ? 0 : BlocoBase.EspacoAntesCabecalho;

        public bool CompletamenteDesenhada => Tabela.LinhaAtual == ViewModel.Produtos.Count;
        public override bool PossuiContono => false;

        #endregion

        #region Leiaute NT 2026.010

        /// <summary>
        ///     Tabela de 8 colunas da NT 2026.010 (item 4.3): bases, alíquotas e valores de ICMS, IPI, IBS, CBS e IS por item.
        /// </summary>
        private void MontarTabelaRtc()
        {
            const AlinhamentoHorizontal ad = AlinhamentoHorizontal.Direita;
            const AlinhamentoHorizontal ac = AlinhamentoHorizontal.Centro;
            const AlinhamentoHorizontal ae = AlinhamentoHorizontal.Esquerda;

            var regimeNormal = ViewModel.Emitente.CRT == "3";
            var rotuloCst = regimeNormal ? "CST" : "CSOSN";

            // Larguras em % da largura útil; a descrição fica com o restante.
            if (ViewModel.Orientacao == Orientacao.Retrato)
                Tabela
                    .ComColuna(0, ae, "DESCRIÇÃO DO PRODUTO / SERVIÇO")
                    .ComColuna(6.6F, ac, rotuloCst + " /", "CFOP")
                    .ComColuna(6.5F, ad, "QTD / UN")
                    .ComColuna(6.9F, ad, "VLR UNIT")
                    .ComColuna(6.9F, ad, "VLR TOTAL")
                    .ComColuna(12.6F, ac, "BASES DE CÁLCULO")
                    .ComColuna(15.6F, ac, "ALÍQUOTAS")
                    .ComColuna(17.9F, ac, "VALOR DOS TRIBUTOS");
            else
                Tabela
                    .ComColuna(0, ae, "DESCRIÇÃO DO PRODUTO / SERVIÇO")
                    .ComColuna(5F, ac, rotuloCst + " /", "CFOP")
                    .ComColuna(5.2F, ad, "QTD / UN")
                    .ComColuna(5.6F, ad, "VLR UNIT")
                    .ComColuna(5.6F, ad, "VLR TOTAL")
                    .ComColuna(10.4F, ac, "BASES DE CÁLCULO")
                    .ComColuna(12.6F, ac, "ALÍQUOTAS")
                    .ComColuna(14.6F, ac, "VALOR DOS TRIBUTOS");

            Tabela.AjustarLarguraColunas();

            var fonte = Tabela.FonteCorpo;
            var fonteNegrito = Estilo.CriarFonteNegrito(fonte.Tamanho);

            foreach (var p in ViewModel.Produtos)
            {
                var descricao = new CelulaPilhaTexto {UltimoItemNaBase = true}
                    .Adicionar(string.IsNullOrWhiteSpace(p.Codigo) ? p.Descricao : $"{p.Codigo} - {p.Descricao}", fonteNegrito)
                    .Adicionar(QuebrarLinhas(p.InformacoesAdicionais), fonte, ae, CelulaTabela.CorSecundaria)
                    .Adicionar(LinhaClassificacao(p), fonte, ae, CelulaTabela.CorSecundaria);

                var cstCfop = new CelulaRotuloValor(fonte)
                    .AdicionarLinha(rotuloCst, p.OCst)
                    .AdicionarLinha("CFOP", p.Cfop > 0 ? p.Cfop.ToString() : string.Empty);

                var quantidade = new CelulaPilhaTexto()
                    .Adicionar(p.Quantidade.Formatar(), fonte, ad)
                    .Adicionar(p.Unidade, fonte, ad, CelulaTabela.CorSecundaria);

                var valorUnitario = new CelulaPilhaTexto().Adicionar(p.ValorUnitario.Formatar(), fonte, ad);
                var valorTotal = new CelulaPilhaTexto().Adicionar(p.ValorTotal.Formatar(), fonte, ad);

                var bases = new CelulaRotuloValor(fonte)
                    .AdicionarLinha("ICMS", Valor(p.BaseIcms))
                    .AdicionarLinha("IBS / CBS", Valor(p.BaseIbsCbs))
                    .AdicionarLinha("IS", Valor(p.BaseIs))
                    .AdicionarLinha("IPI", Valor(p.BaseIpi));

                var aliquotas = new CelulaRotuloValor(fonte)
                    .AdicionarLinha("ICMS", Percentual(p.AliquotaIcms), "CBS", Percentual(p.AliquotaCbs))
                    .AdicionarLinha("IBS UF", Percentual(p.AliquotaIbsUf), "IPI", Percentual(p.AliquotaIpi))
                    .AdicionarLinha("IBS MUN", Percentual(p.AliquotaIbsMunicipio), "IS", Percentual(p.AliquotaIs));

                var tributos = new CelulaRotuloValor(fonte)
                    .AdicionarLinha("ICMS", Valor(p.ValorIcms), "CBS", Valor(p.ValorCbs))
                    .AdicionarLinha("IBS UF", Valor(p.ValorIbsUf), "IPI", Valor(p.ValorIpi))
                    .AdicionarLinha("IBS MUN", Valor(p.ValorIbsMunicipio), "IS", Valor(p.ValorIs));

                Tabela.AdicionarLinha(descricao, cstCfop, quantidade, valorUnitario, valorTotal, bases, aliquotas, tributos);
            }
        }

        private static string LinhaClassificacao(ProdutoViewModel p)
        {
            var partes = new List<string>();
            if (!string.IsNullOrWhiteSpace(p.Ncm)) partes.Add($"[NCM {p.Ncm.Trim()}]");
            if (!string.IsNullOrWhiteSpace(p.ClassificacaoTributaria)) partes.Add($"[cClassTrib {p.ClassificacaoTributaria}]");
            return string.Join(" ", partes);
        }

        private static string QuebrarLinhas(string str) => string.IsNullOrWhiteSpace(str) ? null : str.Replace(';', '\n');

        private static string Valor(double? valor) => valor.GetValueOrDefault().ToString("#,0.00", DanfeHelper.Cultura);

        private static string Percentual(double? valor) => valor.GetValueOrDefault().ToString("#,0.00##", DanfeHelper.Cultura) + "%";

        #endregion

        #region Methods

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            Tabela.SetPosition(RetanguloTabela.Location);
            Tabela.SetSize(RetanguloTabela.Size);
            Tabela.Draw(gfx);

            CabecalhoBloco.SetPosition(X, Y + EspacoAntesCabecalho);
            CabecalhoBloco.Width = Width;
            CabecalhoBloco.Draw(gfx);
        }

        #endregion
    }
}