using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Cryptography.Models;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.Protocolo
{
    [DFeRoot("protNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class NFeProtNFe : DFeSignDocument<NFeProtNFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeProtNFe()
        {
            Signature = new DFeSignature();
            InfProt = new NFeInfProt();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     PR02 - Versão do leiaute das informações de Protocolo.
        /// </summary>
        [DFeAttribute(TipoCampo.Enum, "versao", Id = "PR02", Min = 4, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeVersao Versao { get; set; }

        /// <summary>
        ///     PR03 - Informações do Protocolo de resposta. TAG a ser assinada
        /// </summary>
        [DFeElement("infProt", Id = "PR03", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeInfProt InfProt { get; set; }

        #endregion
    }
}