using System.ComponentModel;
using System.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.InformacaoAdicional
{
    public sealed class NFeInformacaoAdicional : GenericClone<NFeInformacaoAdicional>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeInformacaoAdicional()
        {
            ObsCont = new DFeCollection<NFeObsCont>();
            ObsFisco = new DFeCollection<NFeObsFisco>();
            ProcRef = new DFeCollection<NFeProcRef>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Z02 - Informações Adicionais de Interesse do Fisco
        /// </summary>
        [DFeElement(TipoCampo.Str, "infAdFisco", Id = "Z02", Min = 1, Max = 2000, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string InformacaoFisco { get; set; }

        /// <summary>
        ///     Z03 - Informações Complementares de interesse do Contribuinte
        /// </summary>
        [DFeElement(TipoCampo.Custom, "infCpl", Id = "Z03", Min = 1, Max = 5000, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string InformacaoComplementar { get; set; }

        /// <summary>
        ///     Z04 - Grupo Campo de uso livre do contribuinte
        ///     <para>Ocorrência: 0-10</para>
        /// </summary>
        [DFeCollection("obsCont", Id = "Z04", MinSize = 0, MaxSize = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeObsCont> ObsCont { get; set; }

        /// <summary>
        ///     Z07 - Grupo Campo de uso livre do Fisco
        ///     <para>Ocorrência: 0-10</para>
        /// </summary>
        [DFeCollection("obsFisco", Id = "Z07", MinSize = 0, MaxSize = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeObsFisco> ObsFisco { get; set; }

        /// <summary>
        ///     Z10 - Grupo Processo referenciado
        ///     <para>Ocorrência: 0-100</para>
        /// </summary>
        [DFeCollection("procRef", Id = "Z10", MinSize = 0, MaxSize = 100, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeProcRef> ProcRef { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeObsCont() => ObsCont.Any();

        private bool ShouldSerializeObsFisco() => ObsFisco.Any();

        private bool ShouldSerializeProcRef() => ProcRef.Any();

        private string SerializeInformacaoComplementar() => InformacaoComplementar.RemoveBreakline();

        private object DeserializeInformacaoComplementar(string value) => value;

        private string SerializeInformacaoFisco() => InformacaoFisco.RemoveBreakline();

        private object DeserializeInformacaoFisco(string value) => value;

        #endregion
    }
}