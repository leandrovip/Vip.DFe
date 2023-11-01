using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe;

public class NFeDetCombustivelOrigComb : GenericClone<NFeDetCombustivelOrigComb>, INotifyPropertyChanged
{
    #region Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Properties

    /// <summary>
    ///     LA19 - Indicador de importacão
    /// </summary>
    [DFeElement(TipoCampo.Enum, "indImport", Id = "LA19", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
    public NFeIndImport IndImport { get; set; }

    /// <summary>
    ///     LA20 - Código da Uf
    /// </summary>
    [DFeElement(TipoCampo.Enum, "cUFOrig", Id = "LA20", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
    public int UfOrigem { get; set; }

    /// <summary>
    ///     LA21 - Percentual originário para a UF
    /// </summary>
    [DFeElement(TipoCampo.De4, "pOrig", Id = "LA21", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal POrig { get; set; }

    #endregion
}