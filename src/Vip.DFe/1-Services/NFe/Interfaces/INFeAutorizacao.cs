using System.ServiceModel;
using Vip.DFe.NFe.ServAutorizacao;

namespace Vip.DFe.NFe.Interfaces
{
    [ServiceContract(Name = "NfeAutorizacao4Soap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")]
    public interface INFeAutorizacao
    {
        [XmlSerializerFormat(SupportFaults = true)]
        [OperationContract(Name = "nfeAutorizacaoLote", Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLote", ReplyAction = "*")]
        AutorizacaoResponse AutorizacaoLote(AutorizacaoRequest request);
    }
}