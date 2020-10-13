using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal
{
    /// <summary>
    ///     ZD01 - Informações do Responsável Técnico pela emissão do DFe
    /// </summary>
    public class NFeResponsavelTecnico : GenericClone<NFeResponsavelTecnico>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     ZD02 - CNPJ da pessoa jurídica responsável pelo sistema utilizado na emissão do documento fiscal eletrônico
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "ZD02", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cnpj { get; set; }

        /// <summary>
        ///     ZD04 - Nome da pessoa a ser contatada
        /// </summary>
        [DFeElement(TipoCampo.Str, "xContato", Id = "ZD04", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XContato { get; set; }

        /// <summary>
        ///     ZD05 - E-mail da pessoa jurídica a ser contatada
        /// </summary>
        [DFeElement(TipoCampo.Str, "email", Id = "ZD05", Min = 6, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Email { get; set; }

        /// <summary>
        ///     ZD06 - Telefone da pessoa jurídica/física a ser contatada
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "fone", Id = "ZD06", Min = 6, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Fone { get; set; }

        /// <summary>
        ///     ZD08 - Identificador do CSRT
        /// </summary>
        [DFeElement(TipoCampo.Int, "idCSRT", Id = "ZD08", Min = 2, Max = 2, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public int IdCSRT { get; set; }

        /// <summary>
        ///     ZD09 - Hash do CSRT
        /// </summary>
        [DFeElement(TipoCampo.Str, "hashCSRT", Id = "ZD09", Min = 28, Max = 28, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string HashCSRT { get; set; }

        #endregion
    }
}