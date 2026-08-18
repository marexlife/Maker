namespace Maker;

internal static class FailureHandler
{
    internal static void HandleNoUserArgument()
    {
        Console.WriteLine("Please provide an argument");

        Environment.Exit(-1);
    }

    internal static void HandleToMuchUserArguments()
    {
        Console.WriteLine("Please provide only one argument");

        Environment.Exit(-2);
    }
}