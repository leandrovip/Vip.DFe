using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public sealed class NFeDetProdutoMed : GenericClone<NFeDetProdutoMed>, INFeProdutoEspecifico
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     K01a - Código de Produto da ANVISA
        ///     VERSÃO 4.00
        /// </summary>
        [DFeElement(TipoCampo.Str, "cProdANVISA", Id = "K01a", Min = 1, Max = 13, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string cProdANVISA { get; set; }

        /// <summary>
        ///     K01b - Motivo da isenção da ANVISA. Informar número da decisão que o isenta (RDC).
        ///     VERSÃO 4.00
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMotivoIsencao", Id = "K01b", Min = 1, Max = 255, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XMotivoIsencao { get; set; }

        /// <summary>
        ///     K06 - Preço máximo consumidor
        ///     Versão 3.00
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPMC", Id = "K06", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VPMC { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeXMotivoIsencao()
        {
            return cProdANVISA.Equals(DFeConstantes.Isento);
        }

        #endregion
    }
}