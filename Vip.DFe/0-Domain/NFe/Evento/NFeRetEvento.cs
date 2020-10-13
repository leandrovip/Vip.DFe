using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Cryptography.Models;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.Evento
{
    [DFeRoot("retEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public sealed class NFeRetEvento : DFeSignDocument<NFeRetEvento>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeRetEvento()
        {
            InfEvento = new NFeRetInfEvento();
            Signature = new DFeSignature();
        }

        #endregion

        #region Properties

        [DFeAttribute(TipoCampo.Enum, "versao", Min = 3, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeVersao Versao { get; set; }

        [DFeElement("infEvento", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeRetInfEvento InfEvento { get; set; }

        #endregion
    }
}