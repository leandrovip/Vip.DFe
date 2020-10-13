using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.Shared.Enum
{
	/// <summary>
	///     Identificação do Ambiente
	///     <para>1 - Produção</para>
	///     <para>2 - Homologação</para>
	/// </summary>
	public enum TipoAmbiente : byte
    {
        /// <summary>
        ///     Produção
        /// </summary>
        [DFeEnum("1")] [Description("Produção")]
        Producao = 1,

        /// <summary>
        ///     Homologação
        /// </summary>
        [DFeEnum("2")] [Description("Homologação")]
        Homologacao = 2
    }
}