using System.Text;
using Vip.DFe.Document;

namespace Vip.DFe.Service
{
    public abstract class RespostaBase<T> : GenericClone<RespostaBase<T>> where T : class
    {
        #region Constructors

        protected RespostaBase(string xmlEnvio, string xmlRetorno, string envelopeSoap, string respostaWs, bool loadRetorno = true)
        {
            XmlEnvio = xmlEnvio;
            XmlRetorno = xmlRetorno;
            EnvelopeSoap = envelopeSoap;
            RetornoWS = respostaWs;

            if (typeof(DFeDocument<T>).IsAssignableFrom(typeof(T)) && loadRetorno) Resultado = DFeDocument<T>.Load(xmlRetorno, Encoding.UTF8);
        }

        #endregion Constructors

        #region Properties

        public string XmlEnvio { get; }

        public string XmlRetorno { get; }

        public string EnvelopeSoap { get; }

        public string RetornoWS { get; }

        public T Resultado { get; protected set; }

        #endregion Properties
    }
}