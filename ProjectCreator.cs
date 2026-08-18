namespace Maker;

internal static class ProjectCreator
{
    internal static void TryCreateProject(string directoryName)
    {
        try
        {
            DoCreateProject(directoryName);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private static void DoCreateProject(string directoryName)
    {
        ProjectDirectory projectDirectory = new(directoryName);

        projectDirectory.Create();
    }
}