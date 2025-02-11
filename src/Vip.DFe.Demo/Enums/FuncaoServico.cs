using System.ComponentModel;

namespace Vip.DFe.Demo.Enums
{
    public enum FuncaoServico
    {
        [Description("01 - Autorização Lote")] AutorizacaoLote = 1,
        [Description("02 - Autorizacao")] Autorizacao = 2,
        [Description("03 - Consultar Autorização")] ConsultarAutorizacao = 3,
        [Description("04 - Consultar por Chave Acesso")] ConsultarPorChaveAcesso = 4,
        [Description("05 - Consultar Status Serviço")] ConsultarStatusServico = 5,
        [Description("06 - Cancelar Documento")] CancelarDocumento = 6,
        [Description("07 - Inutilizar Numeração")] InutilizarNumeracao = 7,
        [Description("08 - Carta de Correção")] CartaCorrecao = 8,
        [Description("09 - Gerar DANFE")] GerarDanfe = 9,
        [Description("10 - Gerar DANFE Evento")] GerarDanfeEvento = 10,
        [Description("11 - Gerar XML")] GerarXml = 11,
    }
}