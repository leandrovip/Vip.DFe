using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public sealed class NFeDetProdutoCombustivel : GenericClone<NFeDetProdutoVeiculo>, INFeProdutoEspecifico
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeDetProdutoCombustivel()
        {
            CIDE = new NFeDetCombustivelCIDE();
            Encerrante = new NFeDetCombustivelEncerrante();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     LA02 - Código de produto da ANP
        ///     Versão 3.00
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.Str, "cProdANP", Id = "LA02", Min = 1, Max = 9, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CProdANP { get; set; }

        /// <summary>
        ///     LA03 - Descrição do produto conforme ANP
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.Str, "descANP", Id = "LA03", Min = 2, Max = 95, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string DescANP { get; set; }

        /// <summary>
        ///     LA03a - Percentual do GLP derivado do petróleo no produto GLP (cProdANP=210203001)
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De4, "pGLP", Id = "LA03a", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PGLP { get; set; }

        /// <summary>
        ///     LA03b - Percentual de Gás Natural Nacional – GLGNn para o produto GLP (cProdANP= 210203001)
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De4, "pGNn", Id = "LA03b", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PGNn { get; set; }

        /// <summary>
        ///     LA03c - Percentual de Gás Natural Importado – GLGNi para o produto GLP (cProdANP= 210203001)
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De4, "pGNi", Id = "LA03c", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PGNi { get; set; }

        /// <summary>
        ///     LA03d - Valor de partida (cProdANP=210203001)
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPart", Id = "LA03d", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPart { get; set; }

        /// <summary>
        ///     LA04 - Código de autorização / registro do CODIF
        /// </summary>
        [DFeElement(TipoCampo.Str, "CODIF", Id = "LA04", Min = 0, Max = 21, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CODIF { get; set; }

        /// <summary>
        ///     LA05 - Quantidade de combustível faturada à temperatura ambiente
        /// </summary>
        [DFeElement(TipoCampo.De4, "qTemp", Id = "LA05", Min = 5, Max = 16, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal QTemp { get; set; }

        /// <summary>
        ///     LA06 - Sigla da UF de consumo
        /// </summary>
        [DFeElement(TipoCampo.Str, "UFCons", Id = "LA06", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UFCons { get; set; }

        /// <summary>
        ///     LA07 - Informações da CIDE
        /// </summary>
        [DFeElement("CIDE", Id = "LA07", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeDetCombustivelCIDE CIDE { get; set; }

        /// <summary>
        ///     LA11 - Informações do grupo de “encerrante”
        /// </summary>
        [DFeElement("encerrante", Id = "LA11", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeDetCombustivelEncerrante Encerrante { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeEncerrante()
        {
            return Encerrante.NBico != 0;
        }

        private bool ShouldSerializeCIDE()
        {
            return CIDE.vCIDE != 0;
        }

        private bool ShouldSerializePGLP()
        {
            return CProdANP == "210203001";
        }

        private bool ShouldSerializePGNn()
        {
            return CProdANP == "210203001";
        }

        private bool ShouldSerializePGNi()
        {
            return CProdANP == "210203001";
        }

        private bool ShouldSerializeVPart()
        {
            return CProdANP == "210203001";
        }

        #endregion
    }
}