using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Vip.DFe.Extensions;

namespace Vip.DFe.Helpers
{
    public static class SchemaHelper
    {
        private sealed class SchemaEntry
        {
            public DateTime LastWrite;
            public XmlSchemaSet Schemas;
            public string[] Erros;
            public string[] Avisos;
        }

        private static readonly ConcurrentDictionary<string, SchemaEntry> Cache = new ConcurrentDictionary<string, SchemaEntry>();

        private static SchemaEntry ObterSchema(string schema)
        {
            var path = Path.GetFullPath(schema);
            var lastWrite = File.GetLastWriteTimeUtc(path);
            if (Cache.TryGetValue(path, out var cached) && cached.LastWrite == lastWrite)
                return cached;

            var errorList = new List<string>();
            var avisosList = new List<string>();

            XmlSchema xmlSchema;
            using (var schemaReader = new XmlTextReader(schema))
                xmlSchema = XmlSchema.Read(schemaReader, (sender, args) =>
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
                        errorList.Add("\nErro: " + args.Exception.Message + "\nLinha " + args.Exception.LineNumber + " - Coluna " + args.Exception.LinePosition + "\nSource: " + args.Exception.SourceUri);
                });

            var set = new XmlSchemaSet {XmlResolver = new XmlUrlResolver()};
            set.Add(xmlSchema);
            set.Compile();

            // So entra no cache se a leitura/compilacao nao lancou excecao.
            var entry = new SchemaEntry {LastWrite = lastWrite, Schemas = set, Erros = errorList.ToArray(), Avisos = avisosList.ToArray()};
            Cache[path] = entry;
            return entry;
        }

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

            if (arquivoXml.IsNullOrEmpty() || !File.Exists(schema))
            {
                errorList.Add("Arquivo Xml inválido ou Schema não encontrado.");
                erros = errorList.ToArray();
                avisos = avisosList.ToArray();
                return false;
            }

            try
            {
                var entry = ObterSchema(schema);
                errorList.AddRange(entry.Erros);
                avisosList.AddRange(entry.Avisos);

                var settings = new XmlReaderSettings {ValidationType = ValidationType.Schema, Schemas = entry.Schemas};

                // XmlSchemaSet nao garante thread-safety de instancia: serializa o uso do set compartilhado.
                lock (entry)
                {
                    using var xmlReader = XmlReader.Create(new StringReader(arquivoXml), settings);
                    while (xmlReader.Read()) { }
                }
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