using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal
{
    /// <summary>
    ///     YB01 - Grupo de Informações do Intermediador da Transação
    /// </summary>
    public class NFeIntermediador : GenericClone<NFeIntermediador>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     YB02 - CNPJ do Intermediador da Transação (agenciador, plataforma de delivery, marketplace e similar) de serviços e
        ///     de negócios.
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "YB02", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cnpj { get; set; }

        /// <summary>
        ///     YB03 - Identificador cadastrado no intermediador. Nome do usuário ou identificação do perfil do vendedor no site do
        ///     intermediador(agenciador, plataforma de delivery, marketplace e similar) de serviços e de negócios
        /// </summary>
        [DFeElement(TipoCampo.Str, "idCadIntTran", Id = "YB03", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string IdCadIntTran { get; set; }

        #endregion
    }
}