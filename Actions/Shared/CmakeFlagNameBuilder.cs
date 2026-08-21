using Maker.Utils;

namespace Maker.Actions.Shared;

internal sealed class CmakeFlagNameBuilder(string name)
{
    internal string GetProjectFlagsName()
    {
        var screamingSnakeCaseName =
            NameConverter.ToScreamingSnakeCase(name);

        return $"{screamingSnakeCaseName}_FLAGS";
    }
}