using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Via de transporte internacional informada na Declaração de Importação(DI)
    ///     <para />
    ///     1=Marítima;
    ///     2=Fluvial;
    ///     3=Lacustre;
    ///     4=Aérea;
    ///     5=Postal
    ///     6=Ferroviária;
    ///     7=Rodoviária;
    ///     8=Conduto / Rede Transmissão;
    ///     9=Meios Próprios;
    ///     10=Entrada / Saída ficta; 11=Courier; 12=Handcarry. (NT 2013/005 v 1.10).
    /// </summary>
    public enum NFeTpViaTransp
    {
        [DFeEnum("1")] Maritima = 1,
        [DFeEnum("2")] Fluvial = 2,
        [DFeEnum("3")] Lacustre = 3,
        [DFeEnum("4")] Aerea = 4,
        [DFeEnum("5")] Postal = 5,
        [DFeEnum("6")] Ferroviaria = 6,
        [DFeEnum("7")] Rodoviaria = 7,
        [DFeEnum("8")] CondutoRedeTrasmissao = 8,
        [DFeEnum("9")] MeiosProprios = 9,
        [DFeEnum("10")] EntradaSaidaFicta = 10,
        [DFeEnum("11")] Courier = 11,
        [DFeEnum("12")] Handcarry = 12
    }
}