using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Transporte
{
    /// <summary>
    ///     Grupo Informações do Transporte
    /// </summary>
    public sealed class NFeTransporte : GenericClone<NFeTransporte>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeTransporte()
        {
            ModFrete = NFeModalidadeFrete.SemFrete;
            Transporta = new NFeTransporta();
            RetTransp = new NFeRetTransp();
            VeicTransp = new NFeVeicTransp();
            Reboque = new DFeCollection<NFeReboqueTransp>();
            Vol = new DFeCollection<NFeVolTransp>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     X02 - Modalidade do frete
        /// </summary>
        [DFeElement(TipoCampo.Enum, "modFrete", Id = "X02", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeModalidadeFrete ModFrete { get; set; }

        /// <summary>
        ///     X03 - Grupo Transportador
        /// </summary>
        [DFeElement("transporta", Id = "X03", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeTransporta Transporta { get; set; }

        /// <summary>
        ///     X11 - Grupo Retenção ICMS transporte
        /// </summary>
        [DFeElement("retTransp", Id = "X11", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeRetTransp RetTransp { get; set; }

        /// <summary>
        ///     X18 - Grupo Veículo Transporte
        /// </summary>
        [DFeElement("veicTransp", Id = "X18", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeVeicTransp VeicTransp { get; set; }

        /// <summary>
        ///     X22 - Grupo Reboque
        ///     <para>Ocorrência: 0-5</para>
        /// </summary>
        [DFeCollection("reboque", Id = "X22", MinSize = 0, MaxSize = 5, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeReboqueTransp> Reboque { get; set; }

        /// <summary>
        ///     X25a - Identificação do vagão
        /// </summary>
        [DFeElement(TipoCampo.Str, "vagao", Id = "X25a", Min = 1, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Vagao { get; set; }

        /// <summary>
        ///     X25b - Identificação da balsa
        /// </summary>
        [DFeElement(TipoCampo.Str, "balsa", Id = "X25b", Min = 1, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Balsa { get; set; }

        /// <summary>
        ///     X26 - Grupo Volumes
        ///     <para>Ocorrência: 0-5000</para>
        /// </summary>
        [DFeCollection("vol", Id = "X26", MinSize = 0, MaxSize = 5000, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeVolTransp> Vol { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeTransporta()
        {
            return Transporta.Cpf.IsNotNullOrEmpty() || Transporta.Cnpj.IsNotNullOrEmpty() || Transporta.XNome.IsNotNullOrEmpty() ||
                   Transporta.XEnder.IsNotNullOrEmpty() || Transporta.IE.IsNotNullOrEmpty() || Transporta.XMun.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeRetTransp()
        {
            return RetTransp.VServ > 0;
        }

        private bool ShouldSerializeVeicTransp()
        {
            return VeicTransp.Placa.IsNotNullOrEmpty() && VeicTransp.UF.IsNotNullOrEmpty();
        }

        #endregion-
    }
}