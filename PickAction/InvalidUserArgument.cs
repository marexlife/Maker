using Maker.Config;

namespace Maker.PickAction;

internal sealed class InvalidUserArgumentException() : Exception(
    $"""
    Invalid Argument(s):
    Use '{CommandConfig.HelpCommand}', to get to the help screen.
    """
);