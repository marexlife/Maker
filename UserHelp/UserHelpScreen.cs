using Maker.Config;

namespace Maker.UserHelp;

internal static class UserHelpScreen
{
    internal const string HelpScreen =
    $"""
    Use {CommandConfig.HelpCommand} to get to here.
    Use '{CommandConfig.ModuleCommand} my_module' to create a new project module.
    Use '{CommandConfig.ProjectCommand} my_project' to create a new project.
    """;

    internal static void DisplayHelp()
    {
        Console.WriteLine(HelpScreen);
    }
}