using System;
using System.IO;
using System.Xml;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Evento;

namespace Vip.DFe.Danfe.Modelo
{
    public static class DanfeEventoViewModelCreator
    {
        #region Methods

        internal static DanfeEventoViewModel CreateFromXmlStream(Stream stream)
        {
            try
            {
                var evento = NFeProcEvento.Load(stream);
                return CreateFromXml(evento);
            }
            catch (System.Exception ex)
            {
                if (ex.InnerException is XmlException e) throw new System.Exception($"Não foi possível interpretar o Xml. Linha {e.LineNumber} Posição {e.LinePosition}.");
                throw new XmlException("O Xml não parece ser um Evento processado.", ex);
            }
        }

        internal static DanfeEventoViewModel CreateFromXmlString(string xml)
        {
            try
            {
                var evento = NFeProcEvento.Load(xml);
                return CreateFromXml(evento);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Não foi possível interpretar o texto Xml.", ex);
            }
        }

        /// <summary>
        ///     Cria o modelo a partir de uma string xml.
        /// </summary>
        public static DanfeEventoViewModel CriarDeStringXml(string str)
        {
            if (str.IsNullOrEmpty()) throw new ArgumentNullException(nameof(str));
            return CreateFromXmlString(str);
        }

        /// <summary>
        ///     Cria o modelo a partir de um arquivo xml.
        /// </summary>
        /// <param name="caminho"></param>
        /// <returns></returns>
        public static DanfeEventoViewModel CriarDeArquivoXml(string caminho)
        {
            using var sr = new StreamReader(caminho, true);
            return CreateFromXmlStream(sr.BaseStream);
        }

        /// <summary>
        ///     Cria o modelo a partir de um arquivo xml contido num stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>Modelo</returns>
        public static DanfeEventoViewModel CriarDeArquivoXml(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            return CreateFromXmlStream(stream);
        }

        public static DanfeEventoViewModel CreateFromXml(NFeProcEvento procEvento)
        {
            var infEvento = procEvento.Evento.InfEvento;
            var retInfEvento = procEvento.RetEvento.InfEvento;
            var model = new DanfeEventoViewModel
            {
                ChaveAcesso = infEvento.Chave,
                Orgao = (int) infEvento.COrgao,
                TipoAmbiente = (int) infEvento.TpAmb,
                DataHoraEvento = infEvento.DhEvento.DateTime,
                TipoEvento = infEvento.TpEvento,
                DescricaoEvento = infEvento.DetEvento.DescEvento,
                SequenciaEvento = infEvento.NSeqEvento,
                CodigoStatus = retInfEvento.CStat.ToString(),
                Motivo = retInfEvento.xMotivo,
                Protocolo = retInfEvento.NProt,
                Justificativa = infEvento.DetEvento.XJust,
                Correcao = infEvento.DetEvento.XCorrecao,
                CondicaoUso = infEvento.DetEvento.XCondUso
            };

            return model;
        }

        #endregion
    }
}