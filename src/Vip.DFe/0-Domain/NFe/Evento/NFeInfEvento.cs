using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.Evento
{
    /// <summary>
    ///     HP06 - Grupo de informações do registro do Evento
    /// </summary>
    public sealed class NFeInfEvento : GenericClone<NFeInfEvento>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeInfEvento()
        {
            DetEvento = new NFeDetEvento();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     HP07 - Grupo de informações do registro do Evento
        /// </summary>
        [DFeAttribute(TipoCampo.Str, "Id", Id = "HP07", Min = 54, Max = 54, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Id { get; set; }

        /// <summary>
        ///     HP08 - Código do órgão de recepção do Evento.
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cOrgao", Id = "HP08", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CodigoUF COrgao { get; set; }

        /// <summary>
        ///     HP09 - Identificação do Ambiente: 1=Produção /2=Homologação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpAmb", Id = "HP09", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public TipoAmbiente TpAmb { get; set; }

        /// <summary>
        ///     HP10 - CNPJ do autor do Evento
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "HP10", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     HP11 - CPF do autor do Evento
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "HP11", Min = 11, Max = 11, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CPF { get; set; }

        /// <summary>
        ///     HP12 - Chave de Acesso da NF-e vinculada ao Evento
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "chNFe", Id = "HP12", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Chave { get; set; }

        /// <summary>
        ///     HP13 - Data e hora do evento
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhEvento", Id = "HP13", Min = 19, Max = 19, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhEvento { get; set; }

        /// <summary>
        ///     HP14 - Código do evento
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpEvento", Id = "HP14", Min = 6, Max = 6, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTipoEvento TpEvento { get; set; }

        /// <summary>
        ///     HP15 - Sequencial do evento para o mesmo tipo de evento.
        /// </summary>
        [DFeElement(TipoCampo.Int, "nSeqEvento", Id = "HP15", Min = 1, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NSeqEvento { get; set; }

        /// <summary>
        ///     HP16 - Versão do detalhe do evento
        /// </summary>
        [DFeElement(TipoCampo.Enum, "verEvento", Id = "HP16", Min = 3, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeVersao VerEvento { get; set; }

        /// <summary>
        ///     HP17 - Informações do Evento (Cancelamento, Carta de Correcao, EPEC, Manifestação)
        /// </summary>
        [DFeElement("detEvento", Id = "HP17", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeDetEvento DetEvento { get; set; }

        [DFeIgnore]
        public string Documento
        {
            get => CNPJ.IsNotNullOrEmpty() ? CNPJ : CPF;
            set
            {
                if (value.Length == 11)
                    CPF = value;
                else
                    CNPJ = value;
            }
        }

        #endregion

        #region Methods

        private bool ShouldSerializeCNPJ()
        {
            return CNPJ.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeCPF()
        {
            return CPF.IsNotNullOrEmpty();
        }

        #endregion
    }
}