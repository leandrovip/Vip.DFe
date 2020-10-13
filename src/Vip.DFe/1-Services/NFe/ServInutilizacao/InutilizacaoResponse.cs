using System.ServiceModel;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServInutilizacao
{
    [MessageContract(WrapperName = "nfeInutilizacaoNFResponse", IsWrapped = false)]
    public class InutilizacaoResponse : ResponseBase { }
}