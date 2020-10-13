using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Identificacao
{
    public sealed class NFeRefNf : GenericClone<NFeRefNf>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeRefNf()
        {
            Mod = NFeRefNfMod.Modelo1;
            Serie = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     BA04 - Código da UF do emitente
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cUF", Id = "BA04", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CodigoUF CodigoUF { get; set; }

        /// <summary>
        ///     BA05 - Ano e Mês de emissão da NF-e
        /// </summary>
        [DFeElement(TipoCampo.Int, "AAMM", Id = "BA05", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int AAMM { get; set; }

        /// <summary>
        ///     BA06 - CNPJ do emitente
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "BA06", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     BA07 - Modelo do Documento Fiscal
        /// </summary>
        [DFeElement(TipoCampo.Enum, "mod", Id = "BA07", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeRefNfMod Mod { get; set; }

        /// <summary>
        ///     BA08 - Série do Documento Fiscal
        /// </summary>
        [DFeElement(TipoCampo.Int, "serie", Id = "BA08", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int Serie { get; set; }

        /// <summary>
        ///     BA09 - Número do Documento Fiscal
        /// </summary>
        [DFeElement(TipoCampo.Int, "nNF", Id = "BA09", Min = 1, Max = 9, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NumeroNF { get; set; }

        #endregion
    }
}