using System;

namespace Vip.DFe.Enum
{
    [Flags]
    public enum SaveOptions
    {
        None = 1 << 0,
        RemoveAccents = 1 << 1,
        RemoveSpaces = 1 << 2,
        DisableFormatting = 1 << 3,
        OmitDeclaration = 1 << 4
    }
}