namespace Maker;

internal static class ArgumentFailureHandler
{
    internal static void HandleNoUserArgument()
    {
        Console.WriteLine("Please provide an argument");

        Environment.Exit(-1);
    }
}