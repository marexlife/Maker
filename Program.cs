using Maker.Actions.CreateProject;
using Maker.Actions.CreateModule;
using Maker.Actions.RunProject;

using Maker.PickAction;

namespace Maker;

internal static class Program
{
    private static void Main() => new ActionPicker(
        projectInfo => new ProjectCreator(projectInfo).TryCreateProject(),
        moduleInfo => new ModuleCreator(moduleInfo).TryCreateModule()
    ).TryPickAction();
}

