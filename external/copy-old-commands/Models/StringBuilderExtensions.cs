using System.Runtime.CompilerServices;
using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public static class StringBuilderExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static StringBuilder Ident(this StringBuilder builder, int level)
    {
        for (int i = 0; i < level; i++)
            builder.Append("    ");

        return builder;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static StringBuilder IdentPlus(this StringBuilder builder, int level)
    {
        return builder.Ident(level).Append("    ");
    }
}