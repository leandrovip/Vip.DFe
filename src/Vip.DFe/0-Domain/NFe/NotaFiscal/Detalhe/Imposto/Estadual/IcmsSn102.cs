using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public sealed class IcmsSn102 : GenericClone<IcmsSn102>, INFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [DFeElement(TipoCampo.Enum, "orig", Id = "N11", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public OrigemMercadoria Origem { get; set; }

        /// <summary>
        ///     N12a - Código de Situação da Operação – Simples Nacional. CSOSN = 102, 103, 300, 400
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CSOSN", Id = "N12a", Min = 3, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Csosn { get; set; }

        #endregion
    }
}