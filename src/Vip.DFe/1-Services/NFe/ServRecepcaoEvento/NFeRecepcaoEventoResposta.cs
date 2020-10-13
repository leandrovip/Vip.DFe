using Vip.DFe.NFe.Evento;
using Vip.DFe.NFe.ServRecepcaoEvento.Domain;
using Vip.DFe.Service;

namespace Vip.DFe.NFe.ServRecepcaoEvento
{
    public class NFeRecepcaoEventoResposta : RespostaBase<NFeRecepcaoEventoResult>
    {
        #region Constructor

        public NFeRecepcaoEventoResposta(string xmlEnvio, string xmlRetorno, string envelopeSoap, string respostaWs) : base(xmlEnvio, xmlRetorno, envelopeSoap, respostaWs)
        {
            ProcEvento = new NFeProcEvento();
        }

        #endregion

        #region Properties

        public NFeProcEvento ProcEvento { get; set; }

        #endregion
    }
}