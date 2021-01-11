using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;

namespace Vip.DFe.Serializer
{
    public class DFeSerializer
    {
        #region Constantes

        /// <summary>
        ///     Tamanho maior que o m�ximo permitido
        /// </summary>
        internal const string ErrMsgMaior = "Tamanho maior que o m�ximo permitido";

        /// <summary>
        ///     Tamanho menor que o m�nimo permitido
        /// </summary>
        internal const string ErrMsgMenor = "Tamanho menor que o m�nimo permitido";

        /// <summary>
        ///     Nenhum valor informado
        /// </summary>
        internal const string ErrMsgVazio = "Nenhum valor informado";

        /// <summary>
        ///     Conte�do inv�lido
        /// </summary>
        internal const string ErrMsgInvalido = "Conte�do inv�lido";

        /// <summary>
        ///     Numero m�ximo de casas decimais permitidas
        /// </summary>
        internal const string ErrMsgMaximoDecimais = "Numero m�ximo de casas decimais permitidas";

        /// <summary>
        ///     N�mero de ocorr�ncias maior que o m�ximo permitido - M�ximo
        /// </summary>
        internal const string ErrMsgMaiorMaximo = "N�mero de ocorr�ncias maior que o m�ximo permitido - M�ximo";

        /// <summary>
        ///     O numero final n�o pode ser menor que o inicial
        /// </summary>
        internal const string ErrMsgFinalMenorInicial = "O numero final n�o pode ser menor que o inicial";

        /// <summary>
        ///     Arquivo n�o encontrado
        /// </summary>
        internal const string ErrMsgArquivoNaoEncontrado = "Arquivo n�o encontrado";

        /// <summary>
        ///     Somente um campo deve ser preenchido
        /// </summary>
        internal const string ErrMsgSomenteUm = "Somente um campo deve ser preenchido";

        /// <summary>
        ///     N�mero de ocorr�ncias menor que o m�nimo permitido - M�nimo
        /// </summary>
        internal const string ErrMsgMenorMinimo = "N�mero de ocorr�ncias menor que o m�nimo permitido - M�nimo";

        /// <summary>
        ///     CNPJ(MF)
        /// </summary>
        internal const string DscCnpj = "CNPJ(MF)";

        /// <summary>
        ///     CPF
        /// </summary>
        internal const string DscCpf = "CPF";

        #endregion Constantes

        #region Fields

        private readonly Type tipoDFe;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DFeSerializer{T}" /> class.
        /// </summary>
        internal DFeSerializer(Type tipo)
        {
            Guard.Against<ArgumentException>(!tipo.HasAttribute<DFeRootAttribute>(), "N�o � uma classe DFe !");

            tipoDFe = tipo;
            Options = new SerializerOptions();
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     Gets the options.
        /// </summary>
        /// <value>The options.</value>
        public SerializerOptions Options { get; }

        #endregion Propriedades

        #region Methods

        #region Create

        /// <summary>
        ///     Creates the serializer.
        /// </summary>
        /// <param name="tipo">The tipo.</param>
        public static DFeSerializer CreateSerializer(Type tipo) => new DFeSerializer(tipo);

        #endregion Create

        #region Serialize

        /// <summary>
        ///     Serializes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="path">The xml.</param>
        public bool Serialize(object item, string path)
        {
            Guard.Against<ArgumentException>(item.GetType() != tipoDFe, "Tipo diferente do informado");

            Options.ErrosAlertas.Clear();

            if (item.IsNull())
            {
                Options.ErrosAlertas.Add("O item � nulo !");
                return false;
            }

            var xmldoc = Serialize(item);
            var ret = !Options.ErrosAlertas.Any();
            var xml = xmldoc.AsString(Options.FormatarXml, !Options.OmitirDeclaracao, Options.Encoding);
            File.WriteAllText(path, xml, Options.Encoding);

            return ret;
        }

        /// <summary>
        ///     Serializes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="stream">The stream.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Serialize(object item, Stream stream)
        {
            Guard.Against<ArgumentException>(item.GetType() != tipoDFe, "Tipo diferente do informado");

            Options.ErrosAlertas.Clear();
            if (item.IsNull())
            {
                Options.ErrosAlertas.Add("O item � nulo !");
                return false;
            }

            var xmldoc = Serialize(item);
            var ret = !Options.ErrosAlertas.Any();
            var xml = xmldoc.AsString(Options.FormatarXml, !Options.OmitirDeclaracao, Options.Encoding);

            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms, Options.Encoding))
            {
                sw.WriteLine(xml);
                sw.Flush();
                ms.WriteTo(stream);
            }

            stream.Position = 0;
            return ret;
        }

        private XDocument Serialize(object item)
        {
            var xmldoc = Options.OmitirDeclaracao ? new XDocument() : new XDocument(new XDeclaration("1.0", "UTF-8", null));
            var rooTag = tipoDFe.GetAttribute<DFeRootAttribute>();
            var rootName = rooTag.Name;

            if (rootName.IsNullOrEmpty())
            {
                var root = tipoDFe.GetRootName(item);
                rootName = root.IsNullOrEmpty() ? tipoDFe.Name : root;
            }

            var rootElement = ObjectSerializer.Serialize(item, tipoDFe, rootName, rooTag.Namespace, Options);
            xmldoc.Add(rootElement);
            xmldoc.RemoveEmptyNs();

            return xmldoc;
        }

        #endregion Serialize

        #region Deserialize

        /// <summary>
        ///     Deserializes the specified xml.
        /// </summary>
        /// <param name="xml">The xml.</param>
        /// <returns>System.Object.</returns>
        public object Deserialize(string xml)
        {
            var content = File.Exists(xml) ? File.ReadAllText(xml, Options.Encoding) : xml;
            var xmlDoc = XDocument.Parse(content);
            return Deserialize(xmlDoc);
        }

        /// <summary>
        ///     Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>System.Object.</returns>
        public object Deserialize(Stream stream)
        {
            using (var reader = new StreamReader(stream, Options.Encoding))
            {
                stream.Position = 0;
                var xmlDoc = XDocument.Parse(reader.ReadToEnd());
                return Deserialize(xmlDoc);
            }
        }

        private object Deserialize(XDocument xmlDoc)
        {
            Options.ErrosAlertas.Clear();

            var rootTag = tipoDFe.GetAttribute<DFeRootAttribute>();

            var rootNames = new List<string>();
            if (!rootTag.Name.IsNullOrEmpty())
            {
                rootNames.Add(rootTag.Name);
                rootNames.Add(tipoDFe.Name);
            }
            else
            {
                rootNames.AddRange(tipoDFe.GetRootNames());
                rootNames.Add(tipoDFe.Name);
            }

            var xmlNode = (from node in xmlDoc.Descendants()
                where node.Name.LocalName.IsIn(rootNames)
                select node).FirstOrDefault();

            Guard.Against<VipException>(xmlNode == null, "Nenhum objeto root encontrado !");

            var returnValue = ObjectSerializer.Deserialize(tipoDFe, xmlNode, Options);

            return returnValue;
        }

        #endregion Deserialize

        #endregion Methods
    }
}