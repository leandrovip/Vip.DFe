using System.ServiceModel;
using Vip.DFe.NFe.ServInutilizacao;

namespace Vip.DFe.NFe.Interfaces
{
    [ServiceContract(Name = "NfeInutilizacaoSoap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4")]
    public interface INFeInutilizacao
    {
        [XmlSerializerFormat(SupportFaults = true)]
        [OperationContract(Name = "nfeInutilizacaoNF", Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4/nfeInutilizacaoNF", ReplyAction = "*")]
        InutilizacaoResponse Inutilizar(InutilizacaoRequest request);
    }
}