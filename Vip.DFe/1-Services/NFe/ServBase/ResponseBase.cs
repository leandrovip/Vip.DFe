using System.ServiceModel;
using System.Xml;

namespace Vip.DFe.NFe.ServBase
{
    [MessageContract]
    public abstract class ResponseBase
    {
        #region Propriedades

        [MessageBodyMember(Name = "nfeResultMsg", Order = 0)]
        public XmlNode Mensagem;

        #endregion Propriedades
    }
}