using System.ComponentModel;
using System.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Identificacao;

public class NFeGCompraGov : GenericClone<NFeGCompraGov>, INotifyPropertyChanged
{
    #region Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Properties

    /// <summary>
    ///     BB02 - Tipo de ente governamental que realizou a compra
    /// </summary>
    [DFeElement(TipoCampo.Enum, "tpEnteGov", Id = "BB02", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
    public NFeTipoCompraGov TpEnteGov { get; set; }

    /// <summary>
    ///     BB03 - Percentual de redução da alíquota em compra governamental
    /// </summary>
    [DFeElement(TipoCampo.De4, "pRedutor", Id = "BB03", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
    public decimal PRedutor { get; set; }

    /// <summary>
    ///     BB04 - Tipo de operação com o ente governamental
    /// </summary>
    [DFeElement(TipoCampo.Enum, "tpOperGov", Id = "BB04", Ocorrencia = Ocorrencia.Obrigatoria)]
    public NFeTipoOperacaoGov TpOperGov { get; set; }

    /// <summary>
    ///     BB05 - Chave de acesso do documento fiscal anterior
    /// </summary>
    [DFeCollection("refDFeAnt", Id = "BB05", MinSize = 0, MaxSize = 99, Ocorrencia = Ocorrencia.NaoObrigatoria)]
    public DFeCollection<string> RefDfeAnt { get; set; }

    #endregion

    #region Methods

    private bool ShouldSerializeRefDfeAnt() => RefDfeAnt != null && RefDfeAnt.Any();

    #endregion
}