namespace Maker;

public static class Program
{
    public static void Main() => ActionPicker.PickAction(
        ArgumentFailureHandler.HandleNoUserArgument,
        ArgumentFailureHandler.HandleToMuchUserArguments,
        ProjectCreator.TryCreateProject
    );
}

