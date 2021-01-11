using System;
using Vip.DFe.Attributes;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Extensions;
using Vip.DFe.Graphics;
using Vip.DFe.Helpers;

namespace Vip.DFe.Danfe.Elementos
{
    [AlturaFixa]
    internal class Duplicata : ElementoBase
    {
        #region Fields

        private static readonly string[] Chaves = {"Número", "Vencimento:", "Valor:"};

        #endregion

        #region Constructors

        public Duplicata(Estilo estilo, DuplicataViewModel viewModel) : base(estilo)
        {
            ViewModel = viewModel;
            FonteA = estilo.CriarFonteRegular(7.5F);
            FonteB = estilo.CriarFonteNegrito(7.5F);
        }

        #endregion

        #region Properties

        public Fonte FonteA { get; private set; }
        public Fonte FonteB { get; private set; }
        public DuplicataViewModel ViewModel { get; private set; }

        public override float Height
        {
            get => 3 * FonteB.AlturaLinha + Estilo.PaddingSuperior + Estilo.PaddingInferior;
            set => throw new NotSupportedException();
        }

        #endregion

        #region Methods

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            var r = BoundingBox.InflatedRetangle(Estilo.PaddingSuperior, Estilo.PaddingInferior, Estilo.PaddingHorizontal);

            string[] valores = {ViewModel.Numero, ViewModel.Vecimento.Formatar(), ViewModel.Valor.FormatarMoeda()};

            for (int i = 0; i < Chaves.Length; i++)
            {
                gfx.DrawString(Chaves[i], r, FonteA);
                gfx.DrawString(valores[i], r, FonteB, AlinhamentoHorizontal.Direita);
                r = r.CutTop(FonteB.AlturaLinha);
            }
        }

        #endregion
    }
}