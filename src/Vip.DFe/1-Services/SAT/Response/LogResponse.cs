using System;
using System.Text;

namespace Vip.DFe.SAT.Response
{
    public sealed class LogResponse : SatResponse
    {
        #region Constructors

        public LogResponse(string retorno, Encoding encoding) : base(retorno, encoding)
        {
            if (CodigoDeRetorno != 15000) return;
            if (RetornoLst.Count > 5) Log = encoding.GetString(Convert.FromBase64String(RetornoLst[5]));
        }

        #endregion Constructors

        #region Properties

        public string Log { get; private set; }

        #endregion Properties
    }
}