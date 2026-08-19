namespace Maker;

internal static class Program
{
    private static void Main() => ActionPicker.PickAction(
        ArgumentFailureHandler.HandleNoUserArgument,
        ArgumentFailureHandler.HandleToMuchUserArguments,
        ProjectCreator.TryCreateProject
    );
}

