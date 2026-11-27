using System;
using org.pdfclown.documents.contents.colorSpaces;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    /// <summary>
    ///     Conteúdo de uma célula da <see cref="Tabela" /> com desenho próprio (leiaute da NT 2026.010).
    /// </summary>
    internal abstract class CelulaTabela
    {
        #region Fields

        /// <summary>
        ///     Cor dos textos secundários (rótulos, informações adicionais).
        /// </summary>
        public static readonly DeviceRGBColor CorSecundaria = new DeviceRGBColor(0.33, 0.33, 0.33);

        /// <summary>
        ///     Cor das linhas pontilhadas entre rótulo e valor.
        /// </summary>
        public static readonly DeviceRGBColor CorPontilhado = new DeviceRGBColor(0.6, 0.6, 0.6);

        #endregion

        #region Methods

        /// <summary>
        ///     Altura ocupada pelo conteúdo para a largura informada.
        /// </summary>
        public abstract float MedirAltura(float largura);

        /// <summary>
        ///     Desenha o conteúdo.
        /// </summary>
        /// <param name="gfx">Destino</param>
        /// <param name="x">Posição horizontal</param>
        /// <param name="y">Posição vertical</param>
        /// <param name="largura">Largura útil da célula</param>
        /// <param name="altura">Altura útil da linha da tabela (maior altura entre as células da linha)</param>
        public abstract void Desenhar(Gfx gfx, float x, float y, float largura, float altura);

        protected static void DesenharComCor(Gfx gfx, DeviceRGBColor cor, Action desenhar)
        {
            if (cor == null)
            {
                desenhar();
                return;
            }

            gfx.PrimitiveComposer.BeginLocalState();
            gfx.PrimitiveComposer.SetFillColor(cor);
            desenhar();
            gfx.PrimitiveComposer.End();
        }

        #endregion
    }
}