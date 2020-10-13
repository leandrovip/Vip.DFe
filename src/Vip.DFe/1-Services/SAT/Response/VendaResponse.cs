using System;
using System.IO;
using System.Text;
using Vip.DFe.Extensions;
using Vip.DFe.SAT.CupomFiscal;

namespace Vip.DFe.SAT.Response
{
    /// <summary>
    ///     Classe que retorna a resposta do Sat quando usado o metodo de venda.
    /// </summary>
    /// <seealso cref="VendaResponse" />
    public sealed class VendaResponse : SatResponse
    {
        #region Constructors

        /// <summary>
        ///     Inicializa uma nova instancia da classe <see cref="VendaResponse" />.
        /// </summary>
        public VendaResponse(string retorno, Encoding encoding) : base(retorno, encoding)
        {
            if (CodigoDeRetorno != 6000 || RetornoLst.Count < 6) return;
            using (var stream = new MemoryStream(Convert.FromBase64String(RetornoLst[6]))) Venda = CFe.Load(stream, encoding);
            if (RetornoLst.Count > 8) ChaveConsulta = RetornoLst[8];

            #region Comments

            //O QRCode é montado a partir dos valores retornados pelo SAT

            //Posições dos campos de retorno
            //numeroSessao, EEEEE, CCCC, mensagem, cod, mensagemSEFAZ, base64, timeStamp, chaveConsulta, valorTotalCFe, CPFCNPJValue, assinaturaQRCOD

            //Cadeia para o QRCode
            //chaveConsulta|timeStamp|valorTotal|CPFCNPJValue|assinaturaQRCODE

            #endregion Comments

            QRCode = $"{RetornoLst[8].OnlyNumbers()}|{RetornoLst[7]}|{RetornoLst[9]}|{RetornoLst[10]}|{RetornoLst[11]}";
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     Retorna o CFe caso tenha sido realizado com sucesso.
        /// </summary>
        public CFe Venda { get; private set; }

        /// <summary>
        ///     Retorna o número do cumpo fiscal.
        /// </summary>
        public int NumeroCupomFiscal => Venda?.InfCFe?.Ide?.NCFe ?? 0;

        /// <summary>
        ///     Retorna a chave de consulta do CFe caso tenha sido realizado com sucesso.
        /// </summary>
        public string ChaveConsulta { get; private set; }

        /// <summary>
        ///     Retorna o QRCode caso tenha sido realizado com sucesso.
        /// </summary>
        public string QRCode { get; private set; }

        #endregion Propriedades
    }
}