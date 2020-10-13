using System;

namespace Vip.DFe.SAT.Events
{
    public sealed class NumeroSessaoEventArgs : EventArgs
    {
        #region Construtor

        public NumeroSessaoEventArgs(int sessao)
        {
            Sessao = sessao;
        }

        #endregion

        #region Properties

        public int Sessao { get; set; }

        #endregion
    }
}