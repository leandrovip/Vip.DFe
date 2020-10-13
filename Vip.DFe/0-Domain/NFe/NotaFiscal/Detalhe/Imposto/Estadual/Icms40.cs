using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public sealed class Icms40 : GenericClone<Icms40>, INFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public Icms40()
        {
            Cst = "40";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [DFeElement(TipoCampo.Enum, "orig", Id = "N11", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public OrigemMercadoria Origem { get; set; }

        /// <summary>
        ///     N12- Situação Tributária - CST 40, 41 ou 50
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "N12", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        /// <summary>
        ///     N27a - Valor do ICMS desonerado
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSDeson", Id = "N27a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcmsDeson { get; set; }

        /// <summary>
        ///     N28 - Motivo da desoneração do ICMS
        /// </summary>
        [DFeElement(TipoCampo.Enum, "motDesICMS", Id = "N28", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeMotivoDesoneracao MotDesIcms { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeMotDesIcms()
        {
            return VIcmsDeson > 0;
        }

        #endregion
    }
}