using Vip.DFe.Danfe.Enum;
using Vip.DFe.Graphics;
using Vip.DFe.Helpers;

namespace Vip.DFe.Danfe.Elementos
{
    /// <summary>
    ///     Campo para valores numéricos.
    /// </summary>
    internal class CampoNumerico : Campo
    {
        #region Constructors

        public CampoNumerico(string cabecalho, double? conteudoNumerico, Estilo estilo, int casasDecimais = 2) : base(cabecalho, null, estilo, AlinhamentoHorizontal.Direita)
        {
            CasasDecimais = casasDecimais;
            ConteudoNumerico = conteudoNumerico;
        }

        #endregion

        #region Properties

        private double? ConteudoNumerico { get; set; }
        public int CasasDecimais { get; set; }

        #endregion

        #region Methods

        protected override void DesenharConteudo(Gfx gfx)
        {
            base.Conteudo = ConteudoNumerico?.ToString($"N{CasasDecimais}", DanfeHelper.Cultura);
            base.DesenharConteudo(gfx);
        }

        #endregion
    }
}