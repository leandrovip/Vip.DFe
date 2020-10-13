using Vip.DFe.NFe.NotaFiscal;
using Vip.DFe.NFe.ServRetAutorizacao.Domain;
using Vip.DFe.Service;

namespace Vip.DFe.NFe.ServRetAutorizacao
{
    public class NFeRetAutorizacaoResposta : RespostaBase<NFeRetAutorizacaoResult>
    {
        #region Constructor

        public NFeRetAutorizacaoResposta(string xmlEnvio, string xmlRetorno, string envelopeSoap, string respostaWs, bool loadRetorno = true) : base(xmlEnvio, xmlRetorno, envelopeSoap, respostaWs, loadRetorno)
        {
            NFeAutorizadas = new NFeProc[0];
        }

        #endregion

        #region Properties

        public NFeProc[] NFeAutorizadas { get; internal set; }

        #endregion
    }
}