namespace Maker;

internal sealed class InvalidUserArgumentException(string message) : Exception(message);