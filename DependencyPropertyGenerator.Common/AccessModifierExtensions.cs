using System.ComponentModel;

namespace DependencyPropertyGenerator.Common;

public static class AccessModifierExtensions
{
    public static string ToCode(this AccessModifier accessModifier) => accessModifier switch
    {
        AccessModifier.Public => "public",
        AccessModifier.Protected => "protected",
        AccessModifier.Internal => "internal",
        AccessModifier.Private => "private",
        AccessModifier.ProtectedInternal => "protected internal",
        AccessModifier.PrivateProtected => "private internal",
        _ => throw new InvalidEnumArgumentException()
    };
}
