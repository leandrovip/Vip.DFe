using System.ServiceModel;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServAutorizacao
{
    [MessageContract(WrapperName = "nfeAutorizacaoLoteResponse", IsWrapped = false)]
    public sealed class AutorizacaoResponse : ResponseBase { }
}