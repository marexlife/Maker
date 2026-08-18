namespace Maker;

internal static class ProjectTree
{
    internal static ProjectDirectory GetProjectTree(string directoryName)
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

        return projectDirectory;
    }
}