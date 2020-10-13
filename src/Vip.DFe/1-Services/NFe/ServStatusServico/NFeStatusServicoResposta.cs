using Vip.DFe.NFe.ServStatusServico.Domain;
using Vip.DFe.Service;

namespace Vip.DFe.NFe.ServStatusServico
{
    public class NFeStatusServicoResposta : RespostaBase<NFeStatusServicoResult>
    {
        #region Constructor

        public NFeStatusServicoResposta(string xmlEnvio, string xmlRetorno, string envelopeSoap, string respostaWs, bool loadRetorno = true) : base(xmlEnvio, xmlRetorno, envelopeSoap, respostaWs, loadRetorno) { }

        #endregion
    }
}