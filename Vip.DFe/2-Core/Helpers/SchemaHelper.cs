using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Vip.DFe.Extensions;

namespace Vip.DFe.Helpers
{
    public static class SchemaHelper
    {
        /// <summary>
        ///     Valida o arquivo XML com o schema informado
        /// </summary>
        /// <param name="arquivoXml">Arquivo XML a ser validado</param>
        /// <param name="schema">Schema</param>
        /// <param name="erros">Erros encontrados</param>
        /// <param name="avisos">Avisos encontrados</param>
        public static bool ValidarXml(string arquivoXml, string schema, out string[] erros, out string[] avisos)
        {
            var errorList = new List<string>();
            var avisosList = new List<string>();

            if (arquivoXml.IsNullOrEmpty())
            {
                errorList.Add("Arquivo Xml não encontrado.");
                erros = errorList.ToArray();
                avisos = avisosList.ToArray();
                return false;
            }

            if (!File.Exists(schema))
            {
                errorList.Add("Arquivo de Schema não encontrado.");
                erros = errorList.ToArray();
                avisos = avisosList.ToArray();
                return false;
            }

            try
            {
                var xmlSchema = XmlSchema.Read(new XmlTextReader(schema), (sender, args) =>
                {
                    switch (args.Severity)
                    {
                        case XmlSeverityType.Warning:
                            avisosList.Add(args.Message);
                            break;

                        case XmlSeverityType.Error:
                            errorList.Add(args.Message);
                            break;
                    }

                    // Erro na validação do schema XSD
                    if (args.Exception != null)
                        errorList.Add("\nErro: " + args.Exception.Message + "\nLinha " + args.Exception.LinePosition + " - Coluna "
                                      + args.Exception.LineNumber + "\nSource: " + args.Exception.SourceUri);
                });

                var settings = new XmlReaderSettings {ValidationType = ValidationType.Schema};
                settings.Schemas.Add(xmlSchema);

                using var xmlReader = XmlReader.Create(new StringReader(arquivoXml), settings);
                while (xmlReader.Read()) { }
            }
            catch (System.Exception exception)
            {
                errorList.Add(exception.Message);
            }

            erros = errorList.ToArray();
            avisos = avisosList.ToArray();
            errorList = null;
            avisosList = null;

            return erros.Length < 1;
        }
    }
}