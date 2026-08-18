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
        ProjectDirectory projectDirectory = new(directoryName, [
            new ProjectDirectory("src", [
                new ProjectFile(
                "main.cpp",
                """
                #include <iostream>
                
                int main() {
                    std::cout << "Hi!\n";
                }
                """)
            ])
        ]);

        projectDirectory.Create();
    }
}