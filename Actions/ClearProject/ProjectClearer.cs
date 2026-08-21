using Maker.Config;

namespace Maker.Actions.ClearProject;

internal static class ProjectClearer
{
    internal static void ClearProject()
    {
        try
        {
            Directory.Delete(NameConfig.BuildDirectoryName, true);
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Nothing to clear");

            return;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            return;
        }

        try
        {
            Directory.Delete(NameConfig.BuildDirectoryName, true);
        }
        catch (DirectoryNotFoundException)
        {
            return;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            return;
        }
    }
}