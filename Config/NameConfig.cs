using System.Diagnostics;

namespace Maker.Config;

internal static class NameConfig
{
    internal const string CMakeLists = "CMakeLists.txt";
    internal const string SourceDirectoryName = "src";
    internal const string CppFileNameExtension = "cpp";
    internal const string CppHeaderFileNameExtension = "h";
    internal const string BuildDirectoryName = "build";
    internal const string MainModuleName = "main";


    internal static string GetPosixExecutionPath(string projectName)
    {
        return GetOsExecutionPath(projectName, OsConfigKind.Posix);
    }

    internal static string GetVariableExecutionPath(string projectName)
    {
        return GetOsExecutionPath(projectName, OsConfigKind.Variable);
    }

    private static string GetOsExecutionPath(
        string projectName,
        OsConfigKind osConfigKind
    )
    {
        var executableFileName = osConfigKind switch
        {
            OsConfigKind.Variable => GetOsVariableExecutableFileName(projectName),
            OsConfigKind.Posix => GetPosixExecutableFileName(projectName),
            OsConfigKind.None or _ => throw new UnreachableException()
        };

        var path = Path.Join(
            ".",
            BuildDirectoryName,
            SourceDirectoryName,
            MainModuleName,
            executableFileName
        );

        return path;
    }

    private static string GetOsVariableExecutableFileName(string projectName)
    {
        return OperatingSystem.IsWindows() ?
            $"{projectName}.exe" :
            GetPosixExecutableFileName(projectName);
    }

    private static string GetPosixExecutableFileName(string projectName)
    {
        return projectName;
    }

}