using System.ServiceModel;
using Vip.DFe.NFe.ServRecepcaoEvento;

namespace Vip.DFe.NFe.Interfaces
{
	[ServiceContract(Name = "NFeRecepcaoEvento4Soap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4")]
	public interface INFeRecepcaoEvento
    {
		[XmlSerializerFormat(SupportFaults = true)]
		[OperationContract(Name = "nfeRecepcaoEvento", Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4/nfeRecepcaoEvento", ReplyAction = "*")]
		RecepcaoEventoResponse RecepcaoEvento(RecepcaoEventoRequest request);
	}
}