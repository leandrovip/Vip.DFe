using System.Collections.Generic;
using System.Text;
using Vip.DFe.Extensions;

namespace Vip.DFe.SAT.Response
{
    public class SatResponse
    {
        #region Constructors

        public SatResponse(string resposta, Encoding encoding)
        {
            /*
			***** RETORNOS DO SAT POR COMANDO *****
			AtivarSAT....................: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ, CSR
			ComunicarCertificadoICPBRASIL: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ
			EnviarDadosVenda.............: numeroSessao, EEEEE, CCCC, mensagem, cod, mensagemSEFAZ, base64, timeStamp, chaveConsulta, valorTotalCFe, CPFCNPJValue, assinaturaQRCOD
			CancelarUltimaVenda..........: numeroSessao, EEEEE, CCCC, mensagem, cod, mensagemSEFAZ, base64, timeStamp, chaveConsulta, valorTotalCFe, CPFCNPJValue, assinaturaQRCOD
			ConsultarSAT.................: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ
			TesteFimAFim.................: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ, base64, timeStamp, numDocFiscal, chaveConsulta
			ConsultarStatusOperacional...: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ, ConteudoRetorno
			ConsultarNumeroSessao........: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ   (ou retorno da Sessão consultada)
            ConsultarUltimaSessaoFiscal..: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ   (ou retorno da ultima Sessão fiscal)
			ConfigurarInterfaceDeRede....: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ
			AssociarAssinatura...........: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ
			AtualizarSoftwareSAT.........: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ
			ExtrairLogs..................: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ, base64
			BloquearSAT..................: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ
			DesbloquearSAT...............: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ
			TrocarCodigoDeAtivacao.......: numeroSessao, EEEEE, mensagem, cod, mensagemSEFAZ
			*/

            RetornoStr = encoding.GetString(Encoding.Default.GetBytes(resposta));
            RetornoLst = new List<string>();
            RetornoLst.AddRange(RetornoStr.Split('|'));

            if (RetornoLst.Count > 1)
            {
                NumeroSessao = RetornoLst[0].ToInt32();
                CodigoDeRetorno = RetornoLst[1].ToInt32();
            }

            var idx = 2;
            if (RetornoLst.Count <= idx) return;

            //Enviar e Cancelar venda tem um campo a mais no inicio da resposta(CCCC)
            var value = RetornoLst[idx].Trim();
            if (value.Length == 4 && value.IsNumeric())
            {
                CodigoDeErro = RetornoLst[idx].ToInt32();
                idx = 3;
            }

            if (RetornoLst.Count > idx + 2)
            {
                MensagemRetorno = RetornoLst[idx];
                CodigoSEFAZ = RetornoLst[idx + 1].ToInt32();
                MensagemSEFAZ = RetornoLst[idx + 2];
            }
            else
            {
                MensagemRetorno = resposta;
            }
        }

        #endregion Constructors

        #region Propriedades

        public int NumeroSessao { get; }

        public int CodigoDeRetorno { get; }

        public int CodigoDeErro { get; }

        public string MensagemRetorno { get; }

        public int CodigoSEFAZ { get; }

        public string MensagemSEFAZ { get; }

        public List<string> RetornoLst { get; }

        public string RetornoStr { get; }

        #endregion Propriedades
    }
}