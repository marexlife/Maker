namespace Maker.Actions.Shared;

internal static class ProjectNameInferService
{
    internal static string InferProjectName()
    {
        var currentPath = Directory.GetCurrentDirectory();
        var info = Directory.GetParent(currentPath);

        return info != null ? info.Name : AskUserForProjectName();
    }

    private static string AskUserForProjectName()
    {
        while (true)
        {
            Console.WriteLine("Could not infer project name, please enter");

            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Your input is empty");

                continue;
            }

            return input;
        }
    }
}