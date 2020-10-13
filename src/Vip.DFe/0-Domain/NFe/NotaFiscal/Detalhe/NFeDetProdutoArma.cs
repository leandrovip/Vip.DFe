using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public sealed class NFeDetProdutoArma : GenericClone<NFeDetProdutoArma>, INFeProdutoEspecifico
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     L02 - Indicador do tipo de arma de fogo
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpArma", Id = "L02", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTipoArma TipoArma { get; set; }

        /// <summary>
        ///     L03 - Número de série da arma
        /// </summary>
        [DFeElement(TipoCampo.Str, "nSerie", Id = "L03", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NSerie { get; set; }

        /// <summary>
        ///     L04 - Número de série do cano
        /// </summary>
        [DFeElement(TipoCampo.Str, "nCano", Id = "L04", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NCano { get; set; }

        /// <summary>
        ///     L05 - Descrição completa da arma, compreendendo: calibre, marca, capacidade, tipo de funcionamento, comprimento e
        ///     demais elementos que permitam a sua perfeita identificação.
        /// </summary>
        [DFeElement(TipoCampo.Str, "descr", Id = "L05", Min = 1, Max = 256, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Descricao { get; set; }

        #endregion
    }
}