using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual;

public class Icms53 : GenericClone<Icms53>, INotifyPropertyChanged
{
    #region Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Constructor

    public Icms53()
    {
        Cst = "53";
    }

    #endregion

    #region Properties

    /// <summary>
    ///     N11 - Origem da Mercadoria
    /// </summary>
    [DFeElement(TipoCampo.Enum, "orig", Id = "N11", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
    public OrigemMercadoria Origem { get; set; }

    /// <summary>
    ///     N12- Situação Tributária - 02
    /// </summary>
    [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "N12", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
    public string Cst { get; private set; }

    /// <summary>
    ///     N41a - Quantidade tributada diferida
    /// </summary>
    [DFeElement(TipoCampo.De4, "qBCMonoDif", Id = "N41a", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
    public decimal QtdeBCMonoDif { get; set; }

    /// <summary>
    ///     N42 - Alíquota ad rem do imposto diferido
    /// </summary>
    [DFeElement(TipoCampo.De4, "adRemICMSDif", Id = "N42", Min = 4, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal AdRemICMSDif { get; set; }

    /// <summary>
    ///     N43 - Valor do ICMS próprio diferido
    /// </summary>
    [DFeElement(TipoCampo.De2, "vICMSMonoDif", Id = "N43", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal VICMSMonoDif { get; set; }

    #endregion
}