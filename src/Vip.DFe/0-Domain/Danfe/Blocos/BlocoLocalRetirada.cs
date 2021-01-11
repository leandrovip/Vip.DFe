using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Modelo;

namespace Vip.DFe.Danfe.Blocos
{
    internal class BlocoLocalRetirada : BlocoLocalEntregaRetirada
    {
        #region Constructors

        public BlocoLocalRetirada(DanfeViewModel viewModel, Estilo estilo)
            : base(viewModel, estilo, viewModel.LocalRetirada) { }

        #endregion

        #region Properties

        public override string Cabecalho => "Informações do local de retirada";

        #endregion
    }
}