using Vip.DFe.NFe.ServInutilizacao.Domain;
using Vip.DFe.Service;

namespace Vip.DFe.NFe.ServInutilizacao
{
    public class NFeInutilizacaoResposta : RespostaBase<NFeInutilizacaoResult>
    {
        #region Constructor

        public NFeInutilizacaoResposta(string xmlEnvio, string xmlRetorno, string envelopeSoap, string respostaWs, bool loadRetorno = true) : base(xmlEnvio, xmlRetorno, envelopeSoap, respostaWs, loadRetorno) { }

        #endregion
    }
}