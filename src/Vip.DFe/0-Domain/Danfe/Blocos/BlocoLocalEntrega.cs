using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Modelo;

namespace Vip.DFe.Danfe.Blocos
{
    internal class BlocoLocalEntrega : BlocoLocalEntregaRetirada
    {
        #region Constructors

        public BlocoLocalEntrega(DanfeViewModel viewModel, Estilo estilo)
            : base(viewModel, estilo, viewModel.LocalEntrega) { }

        #endregion

        #region Properties

        public override string Cabecalho => "Informações do local de entrega";

        #endregion
    }
}