using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public sealed class IcmsSn101 : GenericClone<IcmsSn101>, INFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public IcmsSn101()
        {
            Csosn = "101";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [DFeElement(TipoCampo.Enum, "orig", Id = "N11", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public OrigemMercadoria Origem { get; set; }

        /// <summary>
        ///     N12a - Código de Situação da Operação – Simples Nacional
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CSOSN", Id = "N12a", Min = 3, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Csosn { get; private set; }

        /// <summary>
        ///     N29 - pCredSN - Alíquota aplicável de cálculo do crédito (Simples Nacional).
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCredSN", Id = "N29", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PCredSn { get; set; }

        /// <summary>
        ///     N30 - Valor crédito do ICMS que pode ser aproveitado nos termos do art. 23 da LC 123 (Simples Nacional)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCredICMSSN", Id = "N30", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCredIcmsSn { get; set; }

        #endregion
    }
}