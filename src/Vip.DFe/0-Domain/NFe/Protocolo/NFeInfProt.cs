using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.Protocolo
{
    public class NFeInfProt : GenericClone<NFeInfProt>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     PR04 - Identificador da TAG a ser assinada, somente precisa ser informado se a UF assinar a resposta
        /// </summary>
        [DFeAttribute(TipoCampo.Str, "ID", Id = "PR04", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Id { get; set; }

        /// <summary>
        ///     PR05 - Identificação do Ambiente
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpAmb", Id = "PR05", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public TipoAmbiente Ambiente { get; set; }

        /// <summary>
        ///     PR06 - Versão do Aplicativo que processou a consulta.
        /// </summary>
        [DFeElement(TipoCampo.Str, "verAplic", Id = "PR06", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string VerAplic { get; set; }

        /// <summary>
        ///     PR07 - Chave de Acesso da NF-e
        /// </summary>
        [DFeElement(TipoCampo.Str, "chNFe", Id = "PR07", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string ChNFe { get; set; }

        /// <summary>
        ///     PR08 - Data e hora de recebimento
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhRecbto", Id = "PR08", Min = 19, Max = 19, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhRecbto { get; set; }

        /// <summary>
        ///     PR09 - Número do Protocolo da NF-e
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "nProt", Id = "PR09", Min = 15, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NProt { get; set; }

        /// <summary>
        ///     PR10 - Digest Value da NF-e processada Utilizado para conferir a integridade da NFe original.
        /// </summary>
        [DFeElement(TipoCampo.Str, "digVal", Id = "PR10", Min = 28, Max = 28, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string DigVal { get; set; }

        /// <summary>
        ///     PR11 - Código do status da resposta.
        /// </summary>
        [DFeElement(TipoCampo.Int, "cStat", Id = "PR11", Min = 3, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CStat { get; set; }

        /// <summary>
        ///     PR12 - Descrição literal do status da resposta.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMotivo", Id = "PR12", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Motivo { get; set; }

        #endregion
    }
}