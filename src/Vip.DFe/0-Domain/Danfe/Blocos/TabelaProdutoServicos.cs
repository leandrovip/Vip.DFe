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
            CabecalhoBloco = new CabecalhoBloco(estilo, "DADOS DOS PRODUTOS / SERVIÇOS");

            const AlinhamentoHorizontal ad = AlinhamentoHorizontal.Direita;
            const AlinhamentoHorizontal ac = AlinhamentoHorizontal.Centro;
            const AlinhamentoHorizontal ae = AlinhamentoHorizontal.Esquerda;

            Tabela = new Tabela(Estilo);
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
        public RectangleF RetanguloTabela => BoundingBox.CutTop(CabecalhoBloco.Height);
        public bool CompletamenteDesenhada => Tabela.LinhaAtual == ViewModel.Produtos.Count;
        public override bool PossuiContono => false;

        #endregion

        #region Methods

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            Tabela.SetPosition(RetanguloTabela.Location);
            Tabela.SetSize(RetanguloTabela.Size);
            Tabela.Draw(gfx);

            CabecalhoBloco.SetPosition(X, Y);
            CabecalhoBloco.Width = Width;
            CabecalhoBloco.Draw(gfx);
        }

        #endregion
    }
}