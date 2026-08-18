namespace Maker;

public static class Program
{
    public static void Main() => ActionPicker.PickAction(
        FailureHandler.HandleNoUserArgument,
        FailureHandler.HandleToMuchUserArguments,
        ProjectCreator.TryCreateProject
    );
}

