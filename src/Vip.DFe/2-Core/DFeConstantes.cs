namespace Vip.DFe
{
    internal class DFeConstantes
    {
        #region Códigos de Retorno

        public static readonly int[] NFeProcessada = {100, 110, 150, 301, 302, 303};

        public static readonly int[] NFeAutorizada = {100, 150};

        public static readonly int[] NFeDenegada = {110, 301, 302, 303};

        public static readonly int[] EventoProcessado = {128, 135, 136, 151, 155};

        #endregion

        #region Mensagens

        public const string Isento = "ISENTO";
        public const string NFeHomologacao = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
        public const string Cancelamento = "Cancelamento";
        public const string CartaoCorecao = "Carta de Correcao";

        public const string CondicaoUso =
            @"A Carta de Correcao e disciplinada pelo paragrafo 1o-A do art. 7o do Convenio S/N, de 15 de dezembro de 1970 e pode ser utilizada para regularizacao de erro ocorrido na emissao de documento fiscal, desde que o erro nao esteja relacionado com: I - as variaveis que determinam o valor do imposto tais como: base de calculo, aliquota, diferenca de preco, quantidade, valor da operacao ou da prestacao; II - a correcao de dados cadastrais que implique mudanca do remetente ou do destinatario; III - a data de emissao ou de saida.";

        #endregion
    }
}