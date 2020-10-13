using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Vip.DFe.Attributes;
using Vip.DFe.Cryptography.Models;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.Evento
{
    [DFeSignInfoElement("infEvento")]
    [DFeRoot("evento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public sealed class NFeEvento : DFeSignDocument<NFeEvento>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeEvento()
        {
            InfEvento = new NFeInfEvento();
            Signature = new DFeSignature();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     HP05 - Versão do leiaute do evento
        /// </summary>
        [DFeAttribute(TipoCampo.Enum, "versao", Id = "HP05", Min = 1, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeVersao Versao { get; set; }

        /// <summary>
        ///     HP06 - Grupo de informações do registro do Evento
        /// </summary>
        [DFeElement("infEvento", Id = "HP06", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeInfEvento InfEvento { get; set; }

        #endregion

        #region Method

        public void Assinar(X509Certificate2 certificado, SaveOptions saveOptions)
        {
            Guard.Against<ArgumentNullException>(certificado == null, "Certificado não pode ser nulo.");

            if (InfEvento.Id.IsNullOrEmpty() || InfEvento.Id.Length < 54)
            {
                var idEvento = $"ID{InfEvento.TpEvento.GetDFeValue()}{InfEvento.Chave}{InfEvento.NSeqEvento:D2}";
                InfEvento.Id = idEvento;
            }

            AssinarDocumento(certificado, saveOptions, false, SignDigest.SHA1);
        }

        public string ObterNomeXml()
        {
            return $"{InfEvento.Chave}-eve-{InfEvento.TpEvento.GetDescription().ToLower()}.xml";
        }

        #endregion
    }
}