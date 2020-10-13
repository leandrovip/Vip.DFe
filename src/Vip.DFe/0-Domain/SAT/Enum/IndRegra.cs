using Vip.DFe.Attributes;

namespace Vip.DFe.SAT.Enum
{
    /// <summary>
    ///     Indicador da regra de c�lculo utilizada para Valor Bruto dos Produtos e Servi�os:
    ///     <para>A - Arredondamento</para>
    ///     <para>T - Truncamento</para>
    /// </summary>
    public enum IndRegra
    {
        [DFeEnum("A")] Arredondamento,
        [DFeEnum("T")] Truncamento
    }
}