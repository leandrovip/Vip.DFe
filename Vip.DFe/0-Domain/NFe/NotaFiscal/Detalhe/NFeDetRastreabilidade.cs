using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public sealed class NFeDetRastreabilidade : GenericClone<NFeDetRastreabilidade>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     I81 - Número do Lote do produto
        ///     Versão 4.0
        /// </summary>
        [DFeElement(TipoCampo.Str, "nLote", Id = "I81", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NLote { get; set; }

        /// <summary>
        ///     I82 - Quantidade de produto no Lote
        ///     Versão 4.0
        /// </summary>
        [DFeElement(TipoCampo.De3, "qLote", Id = "I82", Min = 3, Max = 11, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QLote { get; set; }

        /// <summary>
        ///     I83 - Data de fabricação/ Produção
        ///     Versão 4.0
        /// </summary>
        [DFeElement(TipoCampo.Dat, "dFab", Id = "I83", Min = 10, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTime DFab { get; set; }

        /// <summary>
        ///     I84 - Data de validade
        ///     Versão 4.0
        /// </summary>
        [DFeElement(TipoCampo.Dat, "dVal", Id = "I84", Min = 10, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTime DVal { get; set; }

        /// <summary>
        ///     I85 - Código de Agregação
        ///     Versão 4.0
        /// </summary>
        [DFeElement(TipoCampo.Str, "cAgreg", Id = "I85", Min = 1, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CAgreg { get; set; }

        #endregion
    }
}