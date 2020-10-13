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
        ///     Identifica��o do campo. No caso de combust�veis, preencher com �Cod.Produto ANP�
        /// </summary>
        [DFeAttribute("xCampoDet", Id = "I18A", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XCampoDet { get; set; }

        /// <summary>
        ///     Conte�do do campo. No caso de combust�veis e/ou lubrificantes, quando informado �CFOP 5656 � Venda de combust�vel
        ///     ou lubrificante adquirido ou recebido de terceiros destinado a consumidor ou usu�rio final�, informar c�digo de
        ///     produto do Sistema de Informa��es de Movimenta��o de produtos - SIMP (http://www.anp.gov.br/simp). Informar
        ///     999999999 se o produto n�o possuir c�digo de produto ANP.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xTextoDet", Id = "I19", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XTextoDet { get; set; }

        #endregion Properties
    }
}