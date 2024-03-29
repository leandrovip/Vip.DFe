﻿using System.Drawing;
using org.pdfclown.documents.contents.xObjects;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Extensions;
using Vip.DFe.Graphics;

namespace Vip.DFe.Danfe.Elementos
{
    internal class IdentificacaoEmitente : ElementoBase
    {
        #region Constructors

        public IdentificacaoEmitente(Estilo estilo, DanfeViewModel viewModel) : base(estilo)
        {
            ViewModel = viewModel;
            Logo = null;
        }

        #endregion

        #region Properties

        public DanfeViewModel ViewModel { get; private set; }
        public XObject Logo { get; set; }

        #endregion

        #region Methods

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            // 7.7.6 Conteúdo do Quadro Dados do Emitente
            // Deverá estar impresso em negrito.A razão social e/ ou nome fantasia deverá ter tamanho
            // mínimo de doze(12) pontos, ou 17 CPP e os demais dados do emitente, endereço,
            // município, CEP, fone / fax deverão ter tamanho mínimo de oito(8) pontos, ou 17 CPP.

            var rp = BoundingBox.InflatedRetangle(0.75F);
            const float alturaMaximaLogoHorizontal = 9F; // old is 14
            var alturaLinhaTexto = 1.9f;
            var logotipoHorizontal = false;

            var f2 = Estilo.CriarFonteNegrito(12);
            var f3 = Estilo.CriarFonteRegular(8);

            if (Logo == null)
            {
                var f1 = Estilo.CriarFonteRegular(6);
                gfx.DrawString("IDENTIFICAÇÃO DO EMITENTE", rp, f1, AlinhamentoHorizontal.Centro);
                rp = rp.CutTop(f1.AlturaLinha);
            }
            else
            {
                RectangleF rLogo;

                if (Logo.Size.Width > Logo.Size.Height) //Logo Horizontal
                {
                    logotipoHorizontal = true;
                    alturaLinhaTexto = 1.6f;
                    rLogo = new RectangleF(rp.X, rp.Y, rp.Width, alturaMaximaLogoHorizontal);
                    rp = rp.CutTop(alturaMaximaLogoHorizontal);
                }
                else //Logo Vertical/Quadrado
                {
                    var lw = rp.Height * Logo.Size.Width / Logo.Size.Height;
                    rLogo = new RectangleF(rp.X, rp.Y, lw, rp.Height);
                    rp = rp.CutLeft(lw);
                }

                gfx.ShowXObject(Logo, rLogo);
            }

            var emitente = ViewModel.Emitente;
            var nome = emitente.RazaoSocial;
            var nome2 = emitente.NomeFantasia.TrimVip();

            if (ViewModel.PreferirEmitenteNomeFantasia)
            {
                nome = emitente.NomeFantasia.IsNotNullOrEmpty() ? emitente.NomeFantasia : emitente.RazaoSocial;
                nome2 = emitente.RazaoSocial;
            }

            var ts = new TextStack(rp) {LineHeightScale = alturaLinhaTexto}
                .AddLine(nome, f2)
                .AddLine(nome2, f3);

            if (logotipoHorizontal)
            {
                ts.AddLine(emitente.EnderecoLinha1.Trim() + " " + emitente.EnderecoLinha2.Trim(), f3);
            }
            else
            {
                ts.AddLine(emitente.EnderecoLinha1.Trim(), f3);
                ts.AddLine(emitente.EnderecoLinha2.Trim(), f3);
            }

            ts.AddLine(emitente.EnderecoLinha3.Trim(), f3);

            ts.AlinhamentoHorizontal = AlinhamentoHorizontal.Centro;
            ts.AlinhamentoVertical = AlinhamentoVertical.Centro;
            ts.Draw(gfx);
        }

        #endregion
    }
}