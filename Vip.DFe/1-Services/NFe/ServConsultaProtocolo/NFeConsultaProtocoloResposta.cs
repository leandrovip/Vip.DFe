using Vip.DFe.NFe.ServConsultaProtocolo.Domain;
using Vip.DFe.Service;

namespace Vip.DFe.NFe.ServConsultaProtocolo
{
    public class NFeConsultaProtocoloResposta : RespostaBase<NFeConsultaProtocoloResult>
    {
        #region Constructors

        public NFeConsultaProtocoloResposta(string xmlEnvio, string xmlRetorno, string envelopeSoap, string respostaWs, bool loadRetorno = true) : base(xmlEnvio, xmlRetorno, envelopeSoap, respostaWs, loadRetorno) { }

        #endregion
    }
}