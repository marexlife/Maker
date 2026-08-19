using Maker.Config;

namespace Maker.PickAction;

internal sealed class InvalidUserArgumentException() : Exception(
    $"""
    Invalid Arguments, use {CommandConfig.HelpCommand} 
    to get to the help screen.
    """
);