using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs;

/// <summary>
///     Grupo de informações da composição do valor do IBS para o Simples Nacional
/// </summary>
public class IbsSimplesNacional : GenericClone<IbsSimplesNacional>, INotifyPropertyChanged
{
    #region Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Properties

    /// <summary>
    ///     UB115c - Alíquota efetiva do IBS para os optantes do Simples Nacional
    /// </summary>
    [DFeElement(TipoCampo.De4, "pIBSSN", Id = "UB115c", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal PIbsSN { get; set; }

    /// <summary>
    ///     UB115d - Valor do IBS para os optantes do Simples Nacional
    /// </summary>
    [DFeElement(TipoCampo.De2, "vIBSSN", Id = "UB115d", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal VIbsSN { get; set; }

    #endregion
}