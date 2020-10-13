using System.ServiceModel;
using Vip.DFe.NFe.ServConsultaProtocolo;

namespace Vip.DFe.NFe.Interfaces
{
    [ServiceContract(Name = "NfeConsultaProtocolo4Soap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4")]
    public interface INFeConsultaProtocolo
    {
        [XmlSerializerFormat(SupportFaults = true)]
        [OperationContract(Name = "nfeConsultaNF", Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4/nfeConsultaNF", ReplyAction = "*")]
        ConsultaProtocoloResponse Consultar(ConsultaProtocoloRequest request);
    }
}