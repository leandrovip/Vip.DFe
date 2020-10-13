using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Vip.DFe.Service
{
    public abstract class Soap12ServiceClientBase<T> : ServiceClientBase<T> where T : class
    {
        #region Constructor

        /// <summary>
        ///     Inicializa uma nova instancia da classe <see cref="Soap12ServiceClientBase{T}" />.
        /// </summary>
        /// <param name="url">URL que o serviço irá utilizar</param>
        /// <param name="timeOut">Timeout</param>
        /// <param name="certificado">Certificado digital</param>
        protected Soap12ServiceClientBase(string url, TimeSpan? timeOut = null, X509Certificate2 certificado = null) : base(url, timeOut, certificado)
        {
            var custom = new CustomBinding(Endpoint.Binding);
            var version = custom.Elements.Find<TextMessageEncodingBindingElement>();
            version.MessageVersion = MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None);

            Endpoint.Binding = custom;
        }

        #endregion
    }
}