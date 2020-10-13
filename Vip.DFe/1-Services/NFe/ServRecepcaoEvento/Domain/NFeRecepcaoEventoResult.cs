using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Evento;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.ServRecepcaoEvento.Domain
{
    [DFeRoot("retEnvEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class NFeRecepcaoEventoResult : DFeDocument<NFeRecepcaoEventoResult>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeRecepcaoEventoResult()
        {
            RetEvento = new DFeCollection<NFeRetEvento>();
        }

        #endregion

        #region Properties

        [DFeAttribute(TipoCampo.Enum, "versao", Min = 1, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeVersao Versao { get; set; }

        [DFeElement(TipoCampo.StrNumber, "idLote", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string IdLote { get; set; }

        [DFeElement(TipoCampo.Enum, "tpAmb", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public TipoAmbiente Ambiente { get; set; }

        [DFeElement(TipoCampo.Str, "verAplic", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string VersaoAplicacao { get; set; }

        [DFeElement(TipoCampo.Enum, "cOrgao", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CodigoUF COrgao { get; set; }

        [DFeElement(TipoCampo.Int, "cStat", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CStat { get; set; }

        [DFeElement(TipoCampo.Str, "xMotivo", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Motivo { get; set; }

        [DFeCollection("retEvento", MinSize = 0, MaxSize = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeRetEvento> RetEvento { get; set; }

        #endregion
    }
}