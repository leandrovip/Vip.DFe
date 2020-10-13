using System;

namespace Vip.DFe.Service
{
    internal class MessageEventArgs : EventArgs
    {
        #region Constructors

        public MessageEventArgs(string message)
        {
            Message = message;
        }

        #endregion Constructors

        #region Properties

        public string Message { get; }

        #endregion Properties
    }
}