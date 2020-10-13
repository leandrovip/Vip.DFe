using System.ServiceModel;
using Vip.DFe.NFe.ServStatusServico;

namespace Vip.DFe.NFe.Interfaces
{
    [ServiceContract(Name = "NfeStatusServico4Soap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4")]
    public interface INFeStatusServico
    {
        [XmlSerializerFormat(SupportFaults = true)]
        [OperationContract(Name = "nfeStatusServicoNF", Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4/nfeStatusServicoNF", ReplyAction = "*")]
        StatusServicoResponse StatusServico(StatusServicoRequest request);
    }
}