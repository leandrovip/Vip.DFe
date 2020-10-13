using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Municipal
{
    public class Issqn : GenericClone<Issqn>, INFeImposto
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public Issqn()
        {
            CPais = 1058;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     U02 - Valor da Base de Cálculo do ISSQN
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "U02", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     U03 - Alíquota do ISSQN
        /// </summary>
        [DFeElement(TipoCampo.De4, "vAliq", Id = "U03", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VAliq { get; set; }

        /// <summary>
        ///     U04 - Valor do ISSQN
        /// </summary>
        [DFeElement(TipoCampo.De2, "vISSQN", Id = "U04", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIssqn { get; set; }

        /// <summary>
        ///     U05 - Código do município de ocorrência do fato gerador do ISSQN
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "cMunFG", Id = "U05", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CMunFG { get; set; }

        /// <summary>
        ///     U06 - Item da Lista de Serviços
        /// </summary>
        [DFeElement(TipoCampo.Str, "cListServ", Id = "U06", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CListServ { get; set; }

        /// <summary>
        ///     U07 - Valor dedução para redução da Base de Cálculo
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDeducao", Id = "U07", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VDeducao { get; set; }

        /// <summary>
        ///     U08 - Valor outras retenções
        /// </summary>
        [DFeElement(TipoCampo.De2, "vOutro", Id = "U08", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VOutro { get; set; }

        /// <summary>
        ///     U09 - Valor desconto incondicionado
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDescIncond", Id = "U09", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VDescIncond { get; set; }

        /// <summary>
        ///     U10 - Valor desconto condicionado
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDescCond", Id = "U10", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VDescCond { get; set; }

        /// <summary>
        ///     U11 - Valor retenção ISS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vISSRet", Id = "U11", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIssRet { get; set; }

        /// <summary>
        ///     U12 - Indicador da exigibilidade do ISS
        /// </summary>
        [DFeElement(TipoCampo.Enum, "indISS", Id = "U12", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeIndicadorIss IndIss { get; set; }

        /// <summary>
        ///     U13 - Código do serviço prestado dentro do município
        /// </summary>
        [DFeElement(TipoCampo.Str, "cServico", Id = "U13", Min = 1, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CServico { get; set; }

        /// <summary>
        ///     U14 - Código do Município de incidência do imposto
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "cMun", Id = "U14", Min = 7, Max = 7, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CMun { get; set; }

        /// <summary>
        ///     U15 - Código do País onde o serviço foi prestado
        /// </summary>
        [DFeElement(TipoCampo.Int, "cPais", Id = "U15", Min = 4, Max = 4, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int CPais { get; set; }

        /// <summary>
        ///     U16 - Número do processo judicial ou administrativo de suspensão da exigibilidade
        /// </summary>
        [DFeElement(TipoCampo.Str, "nProcesso", Id = "U16", Min = 1, Max = 30, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NProcesso { get; set; }

        /// <summary>
        ///     U17 - Indicador de incentivo Fiscal
        /// </summary>
        [DFeElement(TipoCampo.Enum, "indIncentivo", Id = "U17", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeIndIncentivo IndIncentivo { get; set; }

        #endregion
    }
}