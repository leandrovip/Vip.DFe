using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Identificacao
{
    /// <summary>
    ///     Informações do Cupom Fiscal referenciado
    /// </summary>
    public sealed class NFeRefEcf : GenericClone<NFeRefEcf>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     BA21 - Modelo do Documento Fiscal
        /// </summary>
        [DFeElement(TipoCampo.Enum, "mod", Id = "BA21", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeRefEcfMod Mod { get; set; }

        /// <summary>
        ///     BA22 - Número de ordem sequencial do ECF
        /// </summary>
        [DFeElement(TipoCampo.Int, "nECF", Id = "BA22", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NumeroECF { get; set; }

        /// <summary>
        ///     BA23 - Número do Contador de Ordem de Operação - COO
        /// </summary>
        [DFeElement(TipoCampo.Int, "nCOO", Id = "BA23", Min = 1, Max = 6, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NumeroCOO { get; set; }

        #endregion
    }
}