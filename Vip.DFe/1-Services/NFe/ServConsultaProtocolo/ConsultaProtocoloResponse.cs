using System.ServiceModel;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServConsultaProtocolo
{
    [MessageContract(WrapperName = "nfeConsultaNFResponse", IsWrapped = false)]
    public class ConsultaProtocoloResponse : ResponseBase { }
}