using Maker.Config;
using Maker.Actions.CreateModule;
using Maker.Actions.CreateProject;
using Maker.Actions.RunProject;
using Maker.Actions.Shared;

using Maker.UserHelp;

namespace Maker.PickAction;

internal sealed class ActionPicker(
    Action<ProjectCreationInfo> tryCreateProjectAction,
    Action<ModuleCreationInfo> tryCreateModuleAction,
    Action<RunProjectInfo> runProjectAction)
{
    private readonly string[] _args = Environment.GetCommandLineArgs();

    internal void TryPickAction()
    {
        try
        {
            DoPickAction();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    private void DoPickAction()
    {
        Action action = _args.Length switch
        {
            2 => ExecuteArgumentCommandAction,
            3 => ExecuteTwoArgumentCommandAction,
            _ => throw new InvalidUserArgumentException(),
        };

        action.Invoke();
    }

    private void ExecuteArgumentCommandAction()
    {
        var command = _args[1];

        Action action = command switch
        {
            CommandConfig.HelpCommand => UserHelpScreen.DisplayHelp,
            _ => throw new InvalidUserArgumentException()
        };

        action.Invoke();
    }

    private void ExecuteTwoArgumentCommandAction()
    {
        var command = _args[1];
        var name = _args[2];

        Action action = command switch
        {
            CommandConfig.ModuleCommand => () => tryCreateModuleAction.Invoke(
                new ModuleCreationInfo(name, ProjectNameInferService.InferProjectName())
            ),
            CommandConfig.ProjectCommand => () => tryCreateProjectAction.Invoke(
                new ProjectCreationInfo(name)
            ),
            CommandConfig.RunCommand => () => runProjectAction.Invoke(
                new RunProjectInfo(ProjectNameInferService.InferProjectName()
            )),
            _ => throw new InvalidUserArgumentException(),
        };

        action.Invoke();
    }
}