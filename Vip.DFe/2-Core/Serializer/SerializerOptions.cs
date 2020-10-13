using System.Collections.Generic;
using System.Text;

namespace Vip.DFe.Serializer
{
    public class SerializerOptions
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeSerializer" /> class.
        /// </summary>
        internal SerializerOptions()
        {
            ErrosAlertas = new List<string>();
            FormatoAlerta = "TAG:%TAG% ID:%ID%/%TAG%(%DESCRICAO%) - %MSG%.";
            RemoverAcentos = false;
            FormatarXml = true;
            Encoding = Encoding.UTF8;
        }

        #endregion Constructors

        #region Propriedades

        public bool RemoverAcentos { get; set; }

        public bool RemoverEspacos { get; set; }

        public bool FormatarXml { get; set; }

        public bool OmitirDeclaracao { get; set; }

        public Encoding Encoding { get; set; }

        public List<string> ErrosAlertas { get; }

        public string FormatoAlerta { get; set; }

        #endregion Propriedades

        #region Methods

        internal void AddAlerta(string id, string tag, string descricao, string alerta)
        {
            // O Formato da mensagem de erro pode ser alterado pelo usuario alterando-se a property FormatoAlerta: onde;
            // %TAG%       : Representa a TAG; ex: <nLacre>
            // %ID%        : Representa a ID da TAG; ex X34
            // %MSG%       : Representa a mensagem de alerta
            // %DESCRICAO% : Representa a Descrição da TAG

            var s = FormatoAlerta.Clone() as string;
            if (s == null)
                return;

            s = s.Replace("%ID%", id).Replace("%TAG%", $"<{tag}>").Replace("%DESCRICAO%", descricao).Replace("%MSG%", alerta);

            ErrosAlertas.Add(s);
        }

        #endregion Methods
    }
}