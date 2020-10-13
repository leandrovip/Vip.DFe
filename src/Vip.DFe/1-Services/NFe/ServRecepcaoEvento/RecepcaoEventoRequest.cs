using System.ServiceModel;
using System.Xml;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServRecepcaoEvento
{
    [MessageContract(WrapperName = "nfeRecepcaoEventoRequest", IsWrapped = false)]
    public class RecepcaoEventoRequest : RequestBase
    {
        #region Constructor

        public RecepcaoEventoRequest(XmlNode mensagem)
        {
            Mensagem = mensagem;
        }

        #endregion
    }
}