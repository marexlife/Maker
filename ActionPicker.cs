
namespace Maker;

internal static class ActionPicker
{
    internal static void PickAction(
            Action noUserArgumentAction,
            Action tooMuchUserArgumentsAction,
            Action<string> tryCreateProjectAction)
    {
        var args = Environment.GetCommandLineArgs();

        switch (args.Length)
        {
            case 0:
            case 1:
                noUserArgumentAction.Invoke();
                break;
            case 2:
                tryCreateProjectAction.Invoke(args[1]);
                break;
            default:
                tooMuchUserArgumentsAction();
                break;
        }
    }
}