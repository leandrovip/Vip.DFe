using System.ComponentModel;
using System.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Identificacao;

public class NFeGPagAntecipado : GenericClone<NFeGPagAntecipado>, INotifyPropertyChanged
{
    #region Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Properties

    /// <summary>
    ///     BB05 - Chave de acesso do documento fiscal anterior
    /// </summary>
    [DFeCollection("refNFe", Id = "BC02", MinSize = 1, MaxSize = 99, Ocorrencia = Ocorrencia.Obrigatoria)]
    public DFeCollection<string> RefNFe { get; set; }

    #endregion

    #region Methods

    private bool ShouldSerializeRefNFe() => RefNFe != null && RefNFe.Any();

    #endregion
}