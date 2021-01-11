using Vip.DFe.Danfe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    internal class NumeroNfSerie : ElementoBase
    {
        #region Constructors

        public NumeroNfSerie(Estilo estilo, string nfNumero, string nfSerie) : base(estilo)
        {
            NfNumero = nfNumero;
            NfSerie = nfSerie;
        }

        #endregion

        #region Properties

        public string NfNumero { get; private set; }
        public string NfSerie { get; private set; }

        #endregion

        #region Methods

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            var r = BoundingBox.InflatedRetangle(1);

            var f1 = Estilo.CriarFonteNegrito(14);
            var f2 = Estilo.CriarFonteNegrito(11F);

            gfx.DrawString("NF-e", r, f1, AlinhamentoHorizontal.Centro);

            r = r.CutTop(f1.AlturaLinha);

            TextStack ts = new TextStack(r)
                {
                    AlinhamentoHorizontal = AlinhamentoHorizontal.Centro,
                    AlinhamentoVertical = AlinhamentoVertical.Centro,
                    LineHeightScale = 1F
                }
                .AddLine($"Nº.: {NfNumero}", f2)
                .AddLine($"Série: {NfSerie}", f2);

            ts.Draw(gfx);
        }

        #endregion
    }
}