using Maker.CreateProject;
using Maker.CreateModule;

namespace Maker;

internal static class Program
{
    private static void Main() => new ActionPicker(
        projectInfo => new ProjectCreator(projectInfo).TryCreateProject(),
        moduleInfo => new ModuleCreator(moduleInfo).TryCreateModule()
    ).TryPickAction();
}

