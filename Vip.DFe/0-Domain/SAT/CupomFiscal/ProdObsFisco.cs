using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Classe ProdObsFisco. Grupo do campo de uso livre do Fisco
    /// </summary>
    public sealed class ProdObsFisco : GenericClone<ProdObsFisco>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        ///     Identificação do campo. No caso de combustíveis, preencher com “Cod.Produto ANP”
        /// </summary>
        [DFeAttribute("xCampoDet", Id = "I18A", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XCampoDet { get; set; }

        /// <summary>
        ///     Conteúdo do campo. No caso de combustíveis e/ou lubrificantes, quando informado “CFOP 5656 – Venda de combustível
        ///     ou lubrificante adquirido ou recebido de terceiros destinado a consumidor ou usuário final”, informar código de
        ///     produto do Sistema de Informações de Movimentação de produtos - SIMP (http://www.anp.gov.br/simp). Informar
        ///     999999999 se o produto não possuir código de produto ANP.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xTextoDet", Id = "I19", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XTextoDet { get; set; }

        #endregion Properties
    }
}