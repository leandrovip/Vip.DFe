using System;
using System.Collections.Generic;
using System.Linq;
using org.pdfclown.documents.contents.colorSpaces;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    /// <summary>
    ///     Célula com blocos de texto empilhados (com quebra automática de linha).
    /// </summary>
    internal class CelulaPilhaTexto : CelulaTabela
    {
        #region Fields

        private readonly List<Item> _itens = new List<Item>();

        #endregion

        #region Properties

        /// <summary>
        ///     Quando verdadeiro, o último item é alinhado à base da linha da tabela, se houver espaço sobrando.
        /// </summary>
        public bool UltimoItemNaBase { get; set; }

        #endregion

        #region Methods

        public CelulaPilhaTexto Adicionar(string texto, Fonte fonte, AlinhamentoHorizontal alinhamento = AlinhamentoHorizontal.Esquerda, DeviceRGBColor cor = null)
        {
            if (!string.IsNullOrWhiteSpace(texto))
                _itens.Add(new Item(texto, fonte, alinhamento, cor));

            return this;
        }

        public override float MedirAltura(float largura) => _itens.Sum(i => CriarTextBlock(i, 0, 0, largura).Height);

        public override void Desenhar(Gfx gfx, float x, float y, float largura, float altura)
        {
            var dy = y;

            for (var i = 0; i < _itens.Count; i++)
            {
                var item = _itens[i];
                var tb = CriarTextBlock(item, x, dy, largura);

                if (UltimoItemNaBase && i == _itens.Count - 1 && i > 0)
                    tb.Y = Math.Max(dy, y + altura - tb.Height);

                DesenharComCor(gfx, item.Cor, () => tb.Draw(gfx));
                dy += tb.Height;
            }
        }

        private static TextBlock CriarTextBlock(Item item, float x, float y, float largura) => new TextBlock(item.Texto, item.Fonte)
        {
            Width = largura,
            X = x,
            Y = y,
            AlinhamentoHorizontal = item.Alinhamento
        };

        #endregion

        #region Item

        /// <summary>
        ///     Bloco de texto da pilha (detalhe interno da célula).
        /// </summary>
        private class Item
        {
            #region Constructors

            public Item(string texto, Fonte fonte, AlinhamentoHorizontal alinhamento, DeviceRGBColor cor)
            {
                Texto = texto;
                Fonte = fonte;
                Alinhamento = alinhamento;
                Cor = cor;
            }

            #endregion

            #region Properties

            public string Texto { get; }
            public Fonte Fonte { get; }
            public AlinhamentoHorizontal Alinhamento { get; }
            public DeviceRGBColor Cor { get; }

            #endregion
        }

        #endregion
    }
}