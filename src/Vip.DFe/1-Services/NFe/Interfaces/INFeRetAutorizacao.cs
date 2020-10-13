using System.ServiceModel;
using Vip.DFe.NFe.ServRetAutorizacao;

namespace Vip.DFe.NFe.Interfaces
{
    [ServiceContract(Name = "NfeRetAutorizacao4Soap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4")]
    public interface INFeRetAutorizacao
    {
        [XmlSerializerFormat(SupportFaults = true)]
        [OperationContract(Name = "nfeRetAutorizacao", Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4/nfeRetAutorizacao", ReplyAction = "*")]
        RetAutorizacaoResponse ConsultaAutorizacao(RetAutorizacaoRequest request);
    }
}