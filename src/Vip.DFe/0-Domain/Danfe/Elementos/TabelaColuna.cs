using System;
using Vip.DFe.Danfe.Enum;

namespace Vip.DFe.Danfe.Elementos
{
    internal class TabelaColuna
    {
        #region Constructors

        public TabelaColuna(string[] cabecalho, float porcentagemLargura, AlinhamentoHorizontal alinhamentoHorizontal = AlinhamentoHorizontal.Esquerda)
        {
            Cabecalho = cabecalho ?? throw new ArgumentNullException(nameof(cabecalho));
            PorcentagemLargura = porcentagemLargura;
            AlinhamentoHorizontal = alinhamentoHorizontal;
        }

        #endregion

        #region Properties

        public string[] Cabecalho { get; private set; }
        public float PorcentagemLargura { get; set; }
        public AlinhamentoHorizontal AlinhamentoHorizontal { get; private set; }

        #endregion

        #region Methods

        public override string ToString() => string.Join(" ", Cabecalho);

        #endregion
    }
}