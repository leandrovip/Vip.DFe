using System;
using Xunit;

namespace Vip.DFe.Tests
{
    /// <summary>
    ///     Fact executado somente no Windows. Usado em testes que geram PDF do DANFE: o Vip.Pdf usa
    ///     System.Drawing.Common, que a partir do .NET 7 só é suportado no Windows (no Linux lança
    ///     PlatformNotSupportedException). Nos demais sistemas o teste aparece como ignorado.
    /// </summary>
    public sealed class WindowsFactAttribute : FactAttribute
    {
        public WindowsFactAttribute()
        {
            if (!OperatingSystem.IsWindows())
                Skip = "Requer Windows: System.Drawing.Common (usado pelo Vip.Pdf) não é suportado em outros sistemas.";
        }
    }
}
