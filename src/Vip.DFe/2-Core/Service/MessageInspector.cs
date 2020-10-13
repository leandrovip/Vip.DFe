using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Vip.DFe.Extensions;
using Vip.DFe.Helpers;

namespace Vip.DFe.Service
{
    internal class DFeMessageInspector : IClientMessageInspector
    {
        #region Events

        public EventHandler<MessageEventArgs> BeforeSendDFeRequest;
        public EventHandler<MessageEventArgs> AfterReceiveDFeReply;

        #endregion Events

        #region Methods

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var args = new MessageEventArgs(MessageHelper.ToXml(ref request));
            BeforeSendDFeRequest.Raise(this, args);
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            var dfeArgs = new MessageEventArgs(MessageHelper.ToXml(ref reply));
            AfterReceiveDFeReply.Raise(this, dfeArgs);
        }

        #endregion Methods
    }
}