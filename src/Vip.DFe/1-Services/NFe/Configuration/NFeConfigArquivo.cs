using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;

namespace Vip.DFe.NFe.Configuration
{
    public sealed class NFeConfigArquivo
    {
        #region Constructors

        internal NFeConfigArquivo(NFeConfig parent)
        {
            Parent = parent;

            var path = Assembly.GetExecutingAssembly().GetPath();
            Salvar = true;
            DiretorioSchemas = $@"{path}\NFe\Schemas";
            Diretorio = $@"{path}\NFe";
            SchemasCache = new Dictionary<NFeSchema, string>();
        }

        #endregion

        #region Properties

        internal NFeConfig Parent { get; }
        public bool Salvar { get; set; }
        public string DiretorioSchemas { get; set; }
        public string DiretorioAutorizadasBackup { get; set; }
        public string Diretorio { get; set; }
        public Dictionary<NFeSchema, string> SchemasCache { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Retorna o caminho onde será salvo os arquivos Autorizados
        /// </summary>
        public string ObterCaminhoAutorizado(DateTime data, string diretorioBackup = "") =>
            // TODO: Futuramente gerar modelo NFe ou NFce
            ObterCaminho("Autorizado", data, diretorioBackup);

        /// <summary>
        ///     Retornar o caminho onde será salvo os arquivos Assinados
        /// </summary>
        /// <returns></returns>
        public string ObterCaminhoAssinado() => ObterCaminho("Assinado");

        /// <summary>
        ///     Retorna o caminho onde será salvo os arquivos Enviados
        /// </summary>
        public string ObterCaminhoEnviado() => ObterCaminho("Enviado");

        /// <summary>
        ///     Retorna o caminho onde será salvo os arquivos de Retorno
        /// </summary>
        public string ObterCaminhoRetorno() => ObterCaminho("Retorno");

        /// <summary>
        ///     Retorna o caminho onde será salvos os arquivos de Inutilização
        /// </summary>
        /// <returns></returns>
        public string ObterCaminhoInutilizado(string diretorioBackup = "") => ObterCaminho("Inutilizado", diretorioBackup: diretorioBackup);

        /// <summary>
        ///     Gera um caminho para salvar o arquivo desejado
        /// </summary>
        private string ObterCaminho(string caminho, DateTime? data = null, string diretorioBackup = "")
        {
            // Diretório - NFe/
            var pathDefault = $@"{AppDomain.CurrentDomain.BaseDirectory}\NFe";

            string diretorio;
            if (diretorioBackup.IsNotNullOrEmpty())
                diretorio = diretorioBackup;
            else if (Diretorio.IsNotNullOrEmpty())
                diretorio = Diretorio;
            else
                diretorio = pathDefault;

            // NFe/00000000000000
            var cnpj = Parent.CNPJ;
            if (cnpj.IsNotNullOrEmpty()) diretorio = Path.Combine(diretorio, cnpj);

            // NFe/00000000000000/NFCe
            var modeloDescricao = Parent.Modelo.GetDescription();
            if (modeloDescricao.IsNotNullOrEmpty()) diretorio = Path.Combine(diretorio, modeloDescricao);

            // NFe/00000000000000/NFCe/Enviado
            if (caminho.IsNotNullOrEmpty()) diretorio = Path.Combine(diretorio, caminho);

            // NFe/00000000000000/NFCe/Enviado/202009
            if (data.HasValue) diretorio = Path.Combine(diretorio, data.Value.ToString("yyyyMM"));

            if (!Directory.Exists(diretorio)) Directory.CreateDirectory(diretorio);

            return diretorio;
        }

        public string ObterSchema(NFeSchema schema)
        {
            if (SchemasCache.ContainsKey(schema)) return SchemasCache[schema];

            var diretorioSchema = DiretorioSchemas;
            var versao = Parent.Versao;

            string schemaPath;
            switch (schema)
            {
                case NFeSchema.NFe:
                    schemaPath = Path.Combine(diretorioSchema, $"nfe_v{versao.GetDescription()}.xsd");
                    break;
                case NFeSchema.ProcNFe:
                    schemaPath = Path.Combine(diretorioSchema, $"procNFe_v{versao.GetDescription()}.xsd");
                    break;
                case NFeSchema.InutNFe:
                    schemaPath = Path.Combine(diretorioSchema, $"inutNFe_v{versao.GetDescription()}.xsd");
                    break;
                case NFeSchema.ProcInutNFe:
                    schemaPath = Path.Combine(diretorioSchema, $"procInutNFe_v{versao.GetDescription()}.xsd");
                    break;
                case NFeSchema.ConsSitNFe:
                    schemaPath = Path.Combine(diretorioSchema, $"consSitNFe_v{versao.GetDescription()}.xsd");
                    break;
                case NFeSchema.ConsStatServNFe:
                    schemaPath = Path.Combine(diretorioSchema, $"consStatServ_v{versao.GetDescription()}.xsd");
                    break;
                case NFeSchema.EnviNFe:
                    schemaPath = Path.Combine(diretorioSchema, $"enviNFe_v{versao.GetDescription()}.xsd");
                    break;
                case NFeSchema.ConsReciNFe:
                    schemaPath = Path.Combine(diretorioSchema, $"consReciNFe_v{versao.GetDescription()}.xsd");
                    break;
                case NFeSchema.EnvEventoCancNFe:
                    schemaPath = Path.Combine(diretorioSchema, "envEventoCancNFe_v1.00.xsd");
                    break;
                case NFeSchema.EnvCCe:
                    schemaPath = Path.Combine(diretorioSchema, "envCCe_v1.00.xsd");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
            }

            SchemasCache.Add(schema, schemaPath);
            return SchemasCache[schema];
        }

        #endregion
    }
}