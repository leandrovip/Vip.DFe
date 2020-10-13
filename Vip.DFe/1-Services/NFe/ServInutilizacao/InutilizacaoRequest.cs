using System.ServiceModel;
using System.Xml;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServInutilizacao
{
    [MessageContract(WrapperName = "nfeInutilizacaoNFRequest", IsWrapped = false)]
    public class InutilizacaoRequest : RequestBase
    {
        #region Constructor

        public InutilizacaoRequest(XmlNode mensagem)
        {
            Mensagem = mensagem;
        }

        #endregion
    }
}