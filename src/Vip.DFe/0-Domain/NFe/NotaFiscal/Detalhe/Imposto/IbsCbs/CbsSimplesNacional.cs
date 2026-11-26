using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs;

/// <summary>
///     Grupo de informações da composição do valor da CBS para o Simples Nacional
/// </summary>
public class CbsSimplesNacional : GenericClone<CbsSimplesNacional>, INotifyPropertyChanged
{
    #region Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Properties

    /// <summary>
    ///     UB115f - Alíquota efetiva da CBS para os optantes do Simples Nacional
    /// </summary>
    [DFeElement(TipoCampo.De4, "pCBSSN", Id = "UB115f", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal PCbsSN { get; set; }

    /// <summary>
    ///     UB115g - Valor da CBS para os optantes do Simples Nacional
    /// </summary>
    [DFeElement(TipoCampo.De2, "vCBSSN", Id = "UB115g", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal VCbsSN { get; set; }

    #endregion
}