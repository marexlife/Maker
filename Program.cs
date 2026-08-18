namespace Maker;

static class Program
{
    private static void Main(string[] args) => ActionPicker.PickAction(
        args,
        FailureHandler.HandleNoUserArgument,
        FailureHandler.HandleToMuchUserArguments,
        ProjectCreator.TryCreateProject
    );
}





