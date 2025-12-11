using System.ComponentModel;

namespace Vip.DFe.Demo.Enums
{
    public enum FuncaoServico
    {
        [Description("01 - Autorizacao")] Autorizacao = 1,
        [Description("02 - Autorização Lote")] AutorizacaoLote = 2,
        [Description("03 - Consultar Autorização")] ConsultarAutorizacao = 3,
        [Description("04 - Consultar por Chave Acesso")] ConsultarPorChaveAcesso = 4,
        [Description("05 - Consultar Status Serviço")] ConsultarStatusServico = 5,
        [Description("06 - Cancelar Documento")] CancelarDocumento = 6,
        [Description("07 - Cancelar por Substituição")] CancelarSubstituicao = 7,
        [Description("08 - Inutilizar Numeração")] InutilizarNumeracao = 8,
        [Description("09 - Carta de Correção")] CartaCorrecao = 9,
        [Description("10 - Gerar DANFE")] GerarDanfe = 10,
        [Description("11 - Gerar DANFE Evento")] GerarDanfeEvento = 11,
        [Description("12 - Gerar XML")] GerarXml = 12
    }
}