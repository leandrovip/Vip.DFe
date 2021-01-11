using System.Drawing;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    /// <summary>
    ///     Campo de única linha.
    /// </summary>
    internal class Campo : ElementoBase
    {
        #region Constructors

        public Campo(string cabecalho, string conteudo, Estilo estilo, AlinhamentoHorizontal alinhamentoHorizontalConteudo = AlinhamentoHorizontal.Esquerda) : base(estilo)
        {
            Cabecalho = cabecalho;
            Conteudo = conteudo;
            AlinhamentoHorizontalConteudo = alinhamentoHorizontalConteudo;
            IsConteudoNegrito = true;
            Height = DanfeConstantes.CampoAltura;
        }

        #endregion

        #region Properties

        public virtual string Cabecalho { get; set; }
        public virtual string Conteudo { get; set; }
        public AlinhamentoHorizontal AlinhamentoHorizontalConteudo { get; set; }
        public bool IsConteudoNegrito { get; set; }

        public RectangleF RetanguloDesenhvael => BoundingBox.InflatedRetangle(Estilo.PaddingSuperior, Estilo.PaddingInferior, Estilo.PaddingHorizontal);

        #endregion

        #region Methods

        protected virtual void DesenharCabecalho(Gfx gfx)
        {
            if (!string.IsNullOrWhiteSpace(Cabecalho)) gfx.DrawString(Cabecalho.ToUpper(), RetanguloDesenhvael, Estilo.FonteCampoCabecalho);
        }

        protected virtual void DesenharConteudo(Gfx gfx)
        {
            var rDesenhavel = RetanguloDesenhvael;
            var texto = Conteudo;

            var fonte = IsConteudoNegrito ? Estilo.FonteCampoConteudoNegrito : Estilo.FonteCampoConteudo;
            fonte = fonte.Clonar();

            if (!string.IsNullOrWhiteSpace(Conteudo))
            {
                var textWidth = fonte.MedirLarguraTexto(Conteudo);

                // Trata o overflown
                if (textWidth > rDesenhavel.Width)
                {
                    fonte.Tamanho = rDesenhavel.Width * fonte.Tamanho / textWidth;

                    if (fonte.Tamanho < Estilo.FonteTamanhoMinimo)
                    {
                        fonte.Tamanho = Estilo.FonteTamanhoMinimo;

                        texto = "...";
                        string texto2;

                        for (int i = 1; i <= Conteudo.Length; i++)
                        {
                            texto2 = Conteudo.Substring(0, i) + "...";
                            if (fonte.MedirLarguraTexto(texto2) < rDesenhavel.Width)
                                texto = texto2;
                            else
                                break;
                        }
                    }
                }

                gfx.DrawString(texto, rDesenhavel, fonte, AlinhamentoHorizontalConteudo, AlinhamentoVertical.Base);
            }
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
            DesenharCabecalho(gfx);
            DesenharConteudo(gfx);
        }

        public override string ToString() => Cabecalho;

        #endregion
    }
}