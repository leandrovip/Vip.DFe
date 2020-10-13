using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal
{
    /// <summary>
    ///     Grupo Exportação
    /// </summary>
    public sealed class NFeExporta : GenericClone<NFeExporta>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     ZA02 - Sigla da UF de Embarque ou de transposição de fronteira
        /// </summary>
        [DFeElement(TipoCampo.Str, "UFSaidaPais", Id = "ZA02", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UFSaidaPais { get; set; }

        /// <summary>
        ///     ZA03 - Descrição do Local de Embarque ou de transposição de fronteira
        /// </summary>
        [DFeElement(TipoCampo.Str, "xLocExporta", Id = "ZA03", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XLocExporta { get; set; }

        /// <summary>
        ///     ZA04 - Descrição do local de despacho
        /// </summary>
        [DFeElement(TipoCampo.Str, "xLocDespacho", Id = "ZA04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XLocDespacho { get; set; }

        #endregion
    }
}