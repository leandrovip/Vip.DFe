using System.ServiceModel;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServStatusServico
{
    [MessageContract(WrapperName = "nfeStatusServicoNFResponse", IsWrapped = false)]
    public class StatusServicoResponse : ResponseBase { }
}