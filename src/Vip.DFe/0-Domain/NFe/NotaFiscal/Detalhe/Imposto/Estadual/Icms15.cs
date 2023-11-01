using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual;

/// <summary>
///     N03a 0 Grupoo Tributação do ICMS monofásico
/// </summary>
public class Icms15 : GenericClone<Icms15>, INotifyPropertyChanged
{
    #region Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Constructor

    public Icms15()
    {
        Cst = "15";
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
    ///     N37a - Quantidade tributada
    /// </summary>
    [DFeElement(TipoCampo.De4, "qBCMono", Id = "N37a", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
    public decimal QtdeBCMono { get; set; }

    /// <summary>
    ///     N38 - Alíquota ad rem do imposto
    /// </summary>
    [DFeElement(TipoCampo.De4, "adRemICMS", Id = "N38", Min = 4, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal AdRemICMS { get; set; }

    /// <summary>
    ///     N39 - Valor do ICMS próprio
    /// </summary>
    [DFeElement(TipoCampo.De2, "vICMSMono", Id = "N39", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal VICMSMono { get; set; }

    /// <summary>
    ///     N39a - Quantidade tributada sujeita a retenção
    /// </summary>
    [DFeElement(TipoCampo.De4, "qBCMonoReten", Id = "N39a", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
    public decimal QtdeBCMonoReten { get; set; }

    /// <summary>
    ///     N40 - Alíquota ad rem do imposto
    /// </summary>
    [DFeElement(TipoCampo.De4, "adRemICMSReten", Id = "N40", Min = 4, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal AdRemICMSReten { get; set; }

    /// <summary>
    ///     N41 - Valor do ICMS próprio
    /// </summary>
    [DFeElement(TipoCampo.De2, "vICMSMonoReten", Id = "N41", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal VICMSMonoReten { get; set; }

    #endregion
}