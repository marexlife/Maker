internal static class FailureHandler
{
    internal static void HandleFailure()
    {
        Console.WriteLine("Please provide an argument");

        Environment.Exit(-1);
    }
}