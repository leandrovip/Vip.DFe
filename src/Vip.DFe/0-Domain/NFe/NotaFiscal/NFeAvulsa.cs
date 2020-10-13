using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal
{
    /// <summary>
    ///     Informações do fisco emitente (uso exclusivo do fisco)
    /// </summary>
    public sealed class NFeAvulsa : GenericClone<NFeAvulsa>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     D02 - CNPJ do órgão emitente
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "D02", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     D03 - Órgão emitente
        /// </summary>
        [DFeElement(TipoCampo.Str, "xOrgao", Id = "D03", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XOrgao { get; set; }

        /// <summary>
        ///     D04 - Matrícula do agente do Fisco
        /// </summary>
        [DFeElement(TipoCampo.Str, "matr", Id = "D04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Matr { get; set; }

        /// <summary>
        ///     D05 - Nome do agente do Fisco
        /// </summary>
        [DFeElement(TipoCampo.Str, "xAgente", Id = "D05", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XAgente { get; set; }

        /// <summary>
        ///     D06 - Telefone
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "fone", Id = "D06", Min = 6, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Fone { get; set; }

        /// <summary>
        ///     D07 - Sigla da UF
        /// </summary>
        [DFeElement(TipoCampo.Str, "UF", Id = "D07", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UF { get; set; }

        /// <summary>
        ///     D08 - Número do Documento de Arrecadação de Receita
        /// </summary>
        [DFeElement(TipoCampo.Str, "nDAR", Id = "D08", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NumeroDAR { get; set; }

        /// <summary>
        ///     D09 - Data de emissão do Documento de Arrecadação
        /// </summary>
        [DFeElement(TipoCampo.Dat, "dEmi", Id = "D09", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime DataEmi { get; set; }

        /// <summary>
        ///     D10 - Valor Total constante no Documento de arrecadação de Receita
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDAR", Id = "D10", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VDAR { get; set; }

        /// <summary>
        ///     D11 - Repartição Fiscal emitente
        /// </summary>
        [DFeElement(TipoCampo.Str, "repEmi", Id = "D11", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string RepEmi { get; set; }

        /// <summary>
        ///     D12 - Data de pagamento do Documento de Arrecadação
        /// </summary>
        [DFeElement(TipoCampo.Dat, "dPag", Id = "D12", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime DataPag { get; set; }

        #endregion
    }
}