namespace Maker;

internal static class ActionPicker
{
    internal static void PickAction(
            string[] args,
            Action noUserArgumentAction,
            Action tooMuchUserArgumentsAction,
            Action<string> tryCreateProjectAction)
    {
        switch (args.Length)
        {
            case 0:
                noUserArgumentAction.Invoke();
                break;
            case 1:
                tryCreateProjectAction.Invoke(args[0]);
                break;
            default:
                tooMuchUserArgumentsAction();
                break;
        }
    }
}