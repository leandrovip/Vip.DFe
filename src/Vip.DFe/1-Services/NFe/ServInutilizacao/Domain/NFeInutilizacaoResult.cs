using Vip.DFe.Attributes;
using Vip.DFe.Cryptography.Models;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.ServInutilizacao.Domain
{
    [DFeRoot("retInutNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public sealed class NFeInutilizacaoResult : DFeDocument<NFeInutilizacaoResult>
    {
        #region Constructors

        public NFeInutilizacaoResult()
        {
            InfInut = new NFeInfInut();
            Signature = new DFeSignature();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     DR02 - Versão do Layout
        /// </summary>
        [DFeAttribute(TipoCampo.Enum, "versao", Id = "DR02", Min = 3, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeVersao Versao { get; set; }

        /// <summary>
        ///     DR03 - Dados da resposta - TAG a ser assinada
        /// </summary>
        [DFeElement("infInut", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeInfInut InfInut { get; set; }

        public DFeSignature Signature { get; set; }

        #endregion
    }
}