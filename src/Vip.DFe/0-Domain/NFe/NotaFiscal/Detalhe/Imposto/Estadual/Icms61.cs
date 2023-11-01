using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual;

/// <summary>
///     N08a - Grupo Tributação do ICMS monofásico
/// </summary>
public class Icms61 : GenericClone<Icms61>, INotifyPropertyChanged
{
    #region Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Constructor

    public Icms61()
    {
        Cst = "61";
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
    ///     N43a - Quantidade tributada retida anteriormente
    /// </summary>
    [DFeElement(TipoCampo.De4, "qBCMonoRet", Id = "N43a", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
    public decimal QtdeBCMonoRef { get; set; }

    /// <summary>
    ///     N44 - Alíquota ad rem do imposto retido anteriormente
    /// </summary>
    [DFeElement(TipoCampo.De4, "adRemICMSRet", Id = "N44", Min = 4, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal AdRemICMSRet { get; set; }

    /// <summary>
    ///     N45 - Valor do ICMS próprio retido anteriormente
    /// </summary>
    [DFeElement(TipoCampo.De2, "vICMSMonoRet", Id = "N45", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal VICMSMonoRef { get; set; }

    #endregion
}