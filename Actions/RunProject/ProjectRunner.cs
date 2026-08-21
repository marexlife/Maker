using System.Diagnostics;
using Maker.Config;

namespace Maker.Actions.RunProject;

internal sealed class ProjectRunner(ProjectRunInfo projectRunInfo)
{
    internal void TryRunProject()
    {
        try
        {
            RunProject();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private void RunProject()
    {
        try
        {
            RunNinjaGeneration();
        }
        catch
        {
            RunDefaultGeneration();
        }

        BuildProject();

        ExecuteFile();
    }

    private static void RunNinjaGeneration()
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "cmake",
            Arguments = $". -B {NameConfig.BuildDirectoryName} -GNinja"
        });
    }

    private static void RunDefaultGeneration()
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "cmake",
            Arguments = $". -B {NameConfig.BuildDirectoryName}"
        });
    }

    private static void BuildProject()
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "cmake",
            Arguments = $"--build {NameConfig.BuildDirectoryName}"
        });
    }

    private void ExecuteFile()
    {
        var executionPath = NameConfig.GetVariableExecutionPath(
            projectRunInfo.ProjectName
        );

        Process.Start(new ProcessStartInfo
        {
            FileName = executionPath,
        });
    }
}