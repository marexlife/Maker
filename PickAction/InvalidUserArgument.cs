namespace Maker.PickAction;

internal sealed class InvalidUserArgumentException(
    string message
) : Exception(message);