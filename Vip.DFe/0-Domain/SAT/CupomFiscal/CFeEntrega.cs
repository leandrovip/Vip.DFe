using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo de identificação do Local de entrega
    /// </summary>
    public sealed class CFeEntrega : GenericClone<CFeEntrega>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        /// <summary>
        ///     Logradouro
        /// </summary>
        [DFeElement(TipoCampo.Str, "xLgr", Id = "G02", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XLgr { get; set; }

        /// <summary>
        ///     Número
        /// </summary>
        [DFeElement(TipoCampo.Str, "nro", Id = "G03", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Nro { get; set; }

        /// <summary>
        ///     Complemento
        /// </summary>
        [DFeElement(TipoCampo.Str, "xCpl", Id = "G04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XCpl { get; set; }

        /// <summary>
        ///     Bairro
        /// </summary>
        [DFeElement(TipoCampo.Str, "xBairro", Id = "G05", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XBairro { get; set; }

        /// <summary>
        ///     Nome do município
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMun", Id = "G06", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XMun { get; set; }

        /// <summary>
        ///     Sigla da UF
        /// </summary>
        [DFeElement(TipoCampo.Str, "UF", Id = "G07", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UF { get; set; }

        #endregion Propriedades
    }
}