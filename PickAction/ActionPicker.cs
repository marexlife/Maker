using Maker.CreateModule;
using Maker.CreateProject;

namespace Maker.PickAction;

internal sealed class ActionPicker(
    Action<ProjectCreationInfo> tryCreateProjectAction,
    Action<ModuleCreationInfo> tryCreateModuleAction)
{
    private readonly string[] _args = Environment.GetCommandLineArgs();
    const int WantedArgumentCount = 3;

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
            WantedArgumentCount => () => PickSuccessAction(),
            > WantedArgumentCount => () => throw new InvalidUserArgumentException(
                $"Provide at least {WantedArgumentCount} Arguments"
            ),
            < WantedArgumentCount => () => throw new InvalidUserArgumentException(
                $"Provide not more then {WantedArgumentCount} Arguments"
            )
        };

        action.Invoke();
    }

    private void PickSuccessAction()
    {
        var command = _args[1];
        var name = _args[2];

        Action action = command switch
        {
            "mod" => () => tryCreateModuleAction.Invoke(
                new ModuleCreationInfo(name)
            ),
            "project" => () => tryCreateProjectAction.Invoke(
                new ProjectCreationInfo(name)
            ),
            _ => throw new InvalidUserArgumentException(
                "Your first argument is not a know command."
            ),
        };

        action.Invoke();
    }
}