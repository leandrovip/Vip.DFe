using System.ServiceModel;
using System.Xml;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServAutorizacao
{
    [MessageContract(WrapperName = "nfeAutorizacaoLoteRequest", IsWrapped = false)]
    public sealed class AutorizacaoRequest : RequestBase
    {
        #region Constructors

        public AutorizacaoRequest(XmlNode mensagem)
        {
            Mensagem = mensagem;
        }

        #endregion Constructors
    }
}