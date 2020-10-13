using System.ServiceModel;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServRecepcaoEvento
{
    [MessageContract(WrapperName = "nfeRecepcaoEventoResponse", IsWrapped = false)]
    public class RecepcaoEventoResponse : ResponseBase { }
}