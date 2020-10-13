using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public sealed class NFeDetProdutoVeiculo : GenericClone<NFeDetProdutoVeiculo>, INFeProdutoEspecifico
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     J02 - Tipo da operação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpOp", Id = "J02", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTipoOperacaoVeiculo TipoOperacao { get; set; }

        /// <summary>
        ///     J03 - Chassi do veículo
        /// </summary>
        [DFeElement(TipoCampo.Str, "chassi", Id = "J03", Min = 17, Max = 17, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Chassi { get; set; }

        /// <summary>
        ///     J04 - Cor(Código de cada montadora)
        /// </summary>
        [DFeElement(TipoCampo.Str, "cCor", Id = "J04", Min = 1, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CodigoCor { get; set; }

        /// <summary>
        ///     J05 - Descrição da Cor
        /// </summary>
        [DFeElement(TipoCampo.Str, "xCor", Id = "J05", Min = 1, Max = 40, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XCor { get; set; }

        /// <summary>
        ///     J06 - Potência Motor (CV)
        /// </summary>
        [DFeElement(TipoCampo.Str, "pot", Id = "J06", Min = 1, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Pot { get; set; }

        /// <summary>
        ///     J07 - Cilindradas
        /// </summary>
        [DFeElement(TipoCampo.Str, "cilin", Id = "J07", Min = 1, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cilin { get; set; }

        /// <summary>
        ///     J08 - Peso Líquido
        /// </summary>
        [DFeElement(TipoCampo.De4, "pesoL", Id = "J08", Min = 4, Max = 9, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PesoL { get; set; }

        /// <summary>
        ///     J09 - Peso Bruto
        /// </summary>
        [DFeElement(TipoCampo.De4, "pesoB", Id = "J09", Min = 4, Max = 9, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PesoB { get; set; }

        /// <summary>
        ///     J10 - Serial (série)
        /// </summary>
        [DFeElement(TipoCampo.Str, "nSerie", Id = "J10", Min = 1, Max = 9, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NSerie { get; set; }

        /// <summary>
        ///     J11 - Tipo de combustível. Utilizar Tabela RENAVAM (v2.0)
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpComb", Id = "J11", Min = 1, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTipoCombustivel TipoCombustivel { get; set; }

        /// <summary>
        ///     J12 - Número de Motor
        /// </summary>
        [DFeElement(TipoCampo.Str, "nMotor", Id = "J12", Min = 1, Max = 21, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NumeroMotor { get; set; }

        /// <summary>
        ///     J13 - Capacidade Máxima de Tração
        /// </summary>
        [DFeElement(TipoCampo.De4, "CMT", Id = "J13", Min = 4, Max = 9, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal CMT { get; set; }

        /// <summary>
        ///     J14 - Distância entre eixos
        /// </summary>
        [DFeElement(TipoCampo.Str, "dist", Id = "J14", Min = 1, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Dist { get; set; }

        /// <summary>
        ///     J16 - Ano Modelo de Fabricação
        /// </summary>
        [DFeElement(TipoCampo.Int, "anoMod", Id = "J16", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int AnoMod { get; set; }

        /// <summary>
        ///     J17 - Ano de Fabricação
        /// </summary>
        [DFeElement(TipoCampo.Int, "anoFab", Id = "J17", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int AnoFab { get; set; }

        /// <summary>
        ///     J18 - Tipo de Pintura
        /// </summary>
        [DFeElement(TipoCampo.Str, "tpPint", Id = "J18", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string TpPint { get; set; }

        /// <summary>
        ///     J19 - Tipo de Veículo
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "tpVeic", Id = "J19", Min = 1, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string TipoVeiculo { get; set; }

        /// <summary>
        ///     J20 - Espécie de Veículo
        /// </summary>
        [DFeElement(TipoCampo.Int, "espVeic", Id = "J20", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int EspVeic { get; set; }

        /// <summary>
        ///     J21 - Condição do VIN
        /// </summary>
        [DFeElement(TipoCampo.Enum, "VIN", Id = "J21", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeCondicaoVin VIN { get; set; }

        /// <summary>
        ///     J22 - Condição do Veículo
        /// </summary>
        [DFeElement(TipoCampo.Enum, "condVeic", Id = "J22", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeCondicaoVeiculo CondicaoVeiculo { get; set; }

        /// <summary>
        ///     J23 - Código Marca Modelo
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "cMod", Id = "J23", Min = 1, Max = 6, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CMod { get; set; }

        /// <summary>
        ///     J24 - Código da Cor
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "cCorDENATRAN", Id = "J24", Min = 1, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CCorDENATRAN { get; set; }

        /// <summary>
        ///     J25 - Capacidade máxima de lotação
        /// </summary>
        [DFeElement(TipoCampo.Int, "lota", Id = "J25", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int Lota { get; set; }

        /// <summary>
        ///     J26 - Restrição
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpRest", Id = "J26", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTipoRestricao TipoRestricao { get; set; }

        #endregion
    }
}