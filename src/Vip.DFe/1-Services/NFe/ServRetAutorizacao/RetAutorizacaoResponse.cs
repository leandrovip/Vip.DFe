using System.ServiceModel;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServRetAutorizacao
{
    [MessageContract(WrapperName = "nfeRetAutorizacaoResponse", IsWrapped = false)]
    public class RetAutorizacaoResponse : ResponseBase { }
}