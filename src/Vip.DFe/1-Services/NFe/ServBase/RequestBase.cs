using System.ServiceModel;
using System.Xml;

namespace Vip.DFe.NFe.ServBase
{
    [MessageContract]
    public abstract class RequestBase
    {
        [MessageBodyMember(Name = "nfeDadosMsg", Order = 0)]
        public XmlNode Mensagem;
    }
}