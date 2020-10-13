using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.Configuration
{
    /// <summary>
    ///     Classe que guardar as informações referente aos WEBSERVICES
    /// </summary>
    public class NFeEndereco
    {
        #region Constructors

        public NFeEndereco(NFeTipoServico tipoServico, NFeModelo modelo, string autorizadora, NFeVersao versao, TipoAmbiente ambiente, string url)
        {
            TipoServico = tipoServico;
            Modelo = modelo;
            Autorizadora = autorizadora;
            Versao = versao;
            Ambiente = ambiente;
            Url = url;
        }

        #endregion

        #region Properties

        public NFeTipoServico TipoServico { get; private set; }
        public NFeModelo Modelo { get; private set; }
        public string Autorizadora { get; private set; }
        public NFeVersao Versao { get; private set; }
        public TipoAmbiente Ambiente { get; private set; }
        public string Url { get; private set; }

        #endregion
    }
}