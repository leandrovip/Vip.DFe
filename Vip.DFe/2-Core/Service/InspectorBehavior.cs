using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Vip.DFe.Service
{
    internal class InspectorBehavior : IEndpointBehavior
    {
        #region Fields

        private readonly EventHandler<MessageEventArgs> beforeSendRequest;
        private readonly EventHandler<MessageEventArgs> afterReceiveReply;

        #endregion Fields

        #region Constructors

        public InspectorBehavior(EventHandler<MessageEventArgs> beforeSendDFeRequest, EventHandler<MessageEventArgs> afterReceiveDFeReply)
        {
            beforeSendRequest = beforeSendDFeRequest;
            afterReceiveReply = afterReceiveDFeReply;
        }

        #endregion Constructors

        #region Methods

        public void Validate(ServiceEndpoint endpoint) { }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            var messageInspector = new DFeMessageInspector
            {
                BeforeSendDFeRequest = beforeSendRequest,
                AfterReceiveDFeReply = afterReceiveReply
            };

            clientRuntime.ClientMessageInspectors.Add(messageInspector);
        }

        #endregion Methods
    }
}