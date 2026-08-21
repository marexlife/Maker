using Maker.Config;

namespace Maker.Actions.ClearProject;

internal static class ProjectClearer
{
    internal static void ClearProject()
    {
        Directory.Delete(NameConfig.BuildDirectoryName, true);
        Directory.Delete(NameConfig.ClangdCacheDirectoryName, true);
    }
}