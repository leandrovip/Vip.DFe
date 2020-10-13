using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Vip.DFe.Extensions;

namespace Vip.DFe.Service
{
    public abstract class ServiceClientBase<T> : ClientBase<T>, IDisposable where T : class
    {
        #region Constructors

        /// <summary>
        ///     Inicializa uma nova instancia da classe <see cref="ServiceClientBase{T}" />.
        /// </summary>
        protected ServiceClientBase(string url, TimeSpan? timeOut = null, X509Certificate2 certificado = null) : base(new BasicHttpBinding(), new EndpointAddress(url))
        {
            if (!(Endpoint?.Binding is BasicHttpBinding binding)) return;

            binding.UseDefaultWebProxy = true;

            if (ClientCredentials != null)
                ClientCredentials.ClientCertificate.Certificate = certificado;

            if (url.Trim().ToLower().StartsWith("https"))
                binding.Security.Mode = BasicHttpSecurityMode.Transport;

            if (certificado != null)
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            var endpointInspector = new InspectorBehavior((sender, args) => BeforeSendDFeRequest(args.Message),
                (sender, args) => AfterReceiveDFeReply(args.Message));

            Endpoint.EndpointBehaviors.Add(endpointInspector);

            if (!timeOut.HasValue) return;

            binding.OpenTimeout = timeOut.Value;
            binding.ReceiveTimeout = timeOut.Value;
            binding.SendTimeout = timeOut.Value;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        protected virtual void BeforeSendDFeRequest(string message) { }

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        protected virtual void AfterReceiveDFeReply(string message) { }

        #endregion Methods

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            if (State == CommunicationState.Faulted) Abort();
            if (State.IsIn(CommunicationState.Closed, CommunicationState.Closing)) return;
            Close();
        }

        #endregion IDisposable
    }
}