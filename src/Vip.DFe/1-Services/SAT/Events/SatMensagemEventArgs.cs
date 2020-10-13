using System;

namespace Vip.DFe.SAT.Events
{
    public sealed class SatMensagemEventArgs : EventArgs
    {
        #region Constructor

        public SatMensagemEventArgs(int codigo, string mensagem)
        {
            Codigo = codigo;
            Mensagem = mensagem;
        }

        #endregion Constructor

        #region Properties

        public int Codigo { get; private set; }

        public string Mensagem { get; private set; }

        #endregion Properties
    }
}