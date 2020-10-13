using System.ComponentModel;
using System.IO;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.Evento
{
    [DFeRoot("procEventoNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public sealed class NFeProcEvento : DFeDocument<NFeProcEvento>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeProcEvento()
        {
            Evento = new NFeEvento();
            RetEvento = new NFeRetEvento();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     ZR02 - Versão do documento
        /// </summary>
        [DFeAttribute(TipoCampo.Enum, "versao", Id = "ZR02", Min = 1, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeVersao Versao { get; set; }

        /// <summary>
        ///     ZR03 - Evento
        /// </summary>

        [DFeElement("evento", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeEvento Evento { get; set; }

        /// <summary>
        ///     ZR05 - Evento
        /// </summary>
        [DFeElement("retEvento", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeRetEvento RetEvento { get; set; }

        [DFeIgnore] public bool Processado => RetEvento.InfEvento.CStat.IsIn(DFeConstantes.EventoProcessado);

        #endregion

        #region Methods

        public void Gravar(NFeConfig configuracoes)
        {
            if (!configuracoes.Arquivos.Salvar) return;

            var caminho = configuracoes.Arquivos.ObterCaminhoAutorizado(RetEvento.InfEvento.DhRegEvento.DateTime);
            var nomeArquivo = $"{RetEvento.InfEvento.Chave}_{RetEvento.InfEvento.TpEvento.GetValueOrDefault().GetDFeValue()}_{RetEvento.InfEvento.NSeqEvento:00}-procEventoNFe.xml";
            Save(Path.Combine(caminho, nomeArquivo));
        }

        #endregion
    }
}