using System.ServiceModel;
using System.Xml;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServStatusServico
{
    [MessageContract(WrapperName = "nfeStatusServicoNFRequest", IsWrapped = false)]
    public class StatusServicoRequest : RequestBase
    {
        #region Constructor

        public StatusServicoRequest(XmlNode mensagem)
        {
            Mensagem = mensagem;
        }

        #endregion
    }
}