using System.ServiceModel;
using System.Xml;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServConsultaProtocolo
{
    [MessageContract(WrapperName = "nfeConsultaNFRequest", IsWrapped = false)]
    public class ConsultaProtocoloRequest : RequestBase
    {
        #region Constructor

        public ConsultaProtocoloRequest(XmlNode mensagem)
        {
            Mensagem = mensagem;
        }

        #endregion
    }
}