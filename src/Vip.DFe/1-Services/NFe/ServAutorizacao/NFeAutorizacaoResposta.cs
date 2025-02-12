using Vip.DFe.NFe.NotaFiscal;
using Vip.DFe.NFe.ServAutorizacao.Domain;
using Vip.DFe.Service;

namespace Vip.DFe.NFe.ServAutorizacao
{
    public class NFeAutorizacaoResposta : RespostaBase<NFeAutorizacaoResult>
    {
        #region Constructor

        public NFeAutorizacaoResposta(string xmlEnvio, string xmlRetorno, string envelopeSoap, string respostaWs, bool loadRetorno = true) : base(xmlEnvio, xmlRetorno, envelopeSoap, respostaWs, loadRetorno) { }

        #endregion

        #region Properties

        public NFeProc NFeAutorizada { get; internal set; }

        #endregion
    }
}