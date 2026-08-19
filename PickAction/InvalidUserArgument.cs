using Maker.Config;

namespace Maker.PickAction;

internal sealed class InvalidUserArgumentException() : Exception(
    $"""
    Invalid Arguments, use {NameConfig.HelpCommand} 
    to get to the help screen.
    """
);