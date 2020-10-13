using System.ServiceModel;
using System.Xml;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServRetAutorizacao
{
    [MessageContract(WrapperName = "nfeRetAutorizacaoRequest", IsWrapped = false)]
    public class RetAutorizacaoRequest : RequestBase
    {
        #region Constructor

        public RetAutorizacaoRequest(XmlNode mensagem)
        {
            Mensagem = mensagem;
        }

        #endregion
    }
}