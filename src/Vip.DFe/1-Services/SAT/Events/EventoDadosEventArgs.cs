using System;

namespace Vip.DFe.SAT.Events
{
    public sealed class EventoDadosEventArgs : EventArgs
    {
        #region Properties

        public string Dados { get; set; }

        public string Retorno { get; set; }

        #endregion Property
    }
}