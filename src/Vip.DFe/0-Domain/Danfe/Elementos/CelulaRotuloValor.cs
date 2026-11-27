using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    /// <summary>
    ///     Célula com linhas de pares "RÓTULO.....valor", com um ou mais pares por linha separados por " / ".
    ///     Os pares de mesma posição ficam alinhados em colunas e os valores alinhados à direita.
    /// </summary>
    internal class CelulaRotuloValor : CelulaTabela
    {
        #region Fields

        private const float EspacoMinimo = 0.9F;
        private const float RecuoPontilhado = 0.35F;
        private const float TamanhoFonteMinimo = 4.5F;
        private const string Separador = " / ";

        private readonly List<KeyValuePair<string, string>[]> _linhas = new List<KeyValuePair<string, string>[]>();
        private readonly Fonte _fonte;

        #endregion

        #region Constructors

        public CelulaRotuloValor(Fonte fonte)
        {
            _fonte = fonte ?? throw new ArgumentNullException(nameof(fonte));
        }

        #endregion

        #region Properties

        public int QuantidadeLinhas => _linhas.Count;

        #endregion

        #region Methods

        public CelulaRotuloValor AdicionarLinha(params string[] rotulosValores)
        {
            if (rotulosValores == null || rotulosValores.Length == 0 || rotulosValores.Length % 2 != 0)
                throw new ArgumentException("Informe pares de rótulo e valor.", nameof(rotulosValores));

            var pares = new KeyValuePair<string, string>[rotulosValores.Length / 2];
            for (var i = 0; i < pares.Length; i++)
                pares[i] = new KeyValuePair<string, string>(rotulosValores[i * 2] ?? string.Empty, rotulosValores[i * 2 + 1] ?? string.Empty);

            _linhas.Add(pares);
            return this;
        }

        public override float MedirAltura(float largura) => _linhas.Count * AjustarFonte(largura).AlturaLinha;

        public override void Desenhar(Gfx gfx, float x, float y, float largura, float altura)
        {
            if (_linhas.Count == 0) return;

            var fonte = AjustarFonte(largura);
            var quantidadePares = _linhas.Max(l => l.Length);
            var larguraRotulos = new float[quantidadePares];
            var larguraValores = new float[quantidadePares];
            MedirColunas(fonte, larguraRotulos, larguraValores);

            var larguraSeparador = fonte.MedirLarguraTexto(Separador);
            var larguraNecessaria = LarguraNecessaria(larguraRotulos, larguraValores, larguraSeparador);
            var sobra = Math.Max(0, largura - larguraNecessaria) / quantidadePares;
            var larguraPonto = fonte.MedirLarguraChar('.');
            var alturaLinha = fonte.AlturaLinha;

            for (var i = 0; i < _linhas.Count; i++)
            {
                var linha = _linhas[i];
                var ly = y + i * alturaLinha;
                var px = x;

                for (var j = 0; j < linha.Length; j++)
                {
                    var rotulo = linha[j].Key;
                    var valor = linha[j].Value;
                    var larguraPar = larguraRotulos[j] + EspacoMinimo + larguraValores[j] + sobra;
                    var retanguloPar = new RectangleF(px, ly, larguraPar, alturaLinha);

                    var fimRotulo = px + fonte.MedirLarguraTexto(rotulo) + RecuoPontilhado;
                    var inicioValor = px + larguraPar - fonte.MedirLarguraTexto(valor) - RecuoPontilhado;
                    var quantidadePontos = (int) Math.Floor((inicioValor - fimRotulo) / larguraPonto);

                    if (!string.IsNullOrEmpty(rotulo))
                        DesenharComCor(gfx, CorSecundaria, () => gfx.DrawString(rotulo, retanguloPar, fonte));

                    if (quantidadePontos > 0)
                    {
                        var pontos = new string('.', quantidadePontos);
                        var retanguloPontos = new RectangleF(fimRotulo, ly, inicioValor - fimRotulo, alturaLinha);
                        DesenharComCor(gfx, CorPontilhado, () => gfx.DrawString(pontos, retanguloPontos, fonte, AlinhamentoHorizontal.Direita));
                    }

                    if (!string.IsNullOrEmpty(valor))
                        gfx.DrawString(valor, retanguloPar, fonte, AlinhamentoHorizontal.Direita);

                    px += larguraPar;

                    if (j < linha.Length - 1)
                    {
                        var retanguloSeparador = new RectangleF(px, ly, larguraSeparador, alturaLinha);
                        DesenharComCor(gfx, CorSecundaria, () => gfx.DrawString(Separador, retanguloSeparador, fonte, AlinhamentoHorizontal.Centro));
                        px += larguraSeparador;
                    }
                }
            }
        }

        /// <summary>
        ///     Reduz a fonte somente quando o conteúdo não cabe na largura (valores muito grandes).
        /// </summary>
        private Fonte AjustarFonte(float largura)
        {
            if (_linhas.Count == 0) return _fonte;

            var quantidadePares = _linhas.Max(l => l.Length);
            var larguraRotulos = new float[quantidadePares];
            var larguraValores = new float[quantidadePares];
            MedirColunas(_fonte, larguraRotulos, larguraValores);

            var necessaria = LarguraNecessaria(larguraRotulos, larguraValores, _fonte.MedirLarguraTexto(Separador));
            if (necessaria <= largura || largura <= 0) return _fonte;

            var fonte = _fonte.Clonar();
            fonte.Tamanho = Math.Max(TamanhoFonteMinimo, _fonte.Tamanho * largura / necessaria);
            return fonte;
        }

        private void MedirColunas(Fonte fonte, float[] larguraRotulos, float[] larguraValores)
        {
            foreach (var linha in _linhas)
                for (var j = 0; j < linha.Length; j++)
                {
                    larguraRotulos[j] = Math.Max(larguraRotulos[j], fonte.MedirLarguraTexto(linha[j].Key));
                    larguraValores[j] = Math.Max(larguraValores[j], fonte.MedirLarguraTexto(linha[j].Value));
                }
        }

        private static float LarguraNecessaria(float[] larguraRotulos, float[] larguraValores, float larguraSeparador)
        {
            var total = 0F;
            for (var j = 0; j < larguraRotulos.Length; j++) total += larguraRotulos[j] + EspacoMinimo + larguraValores[j];
            return total + larguraSeparador * (larguraRotulos.Length - 1);
        }

        #endregion
    }
}