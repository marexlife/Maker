namespace Maker;

public static class Program
{
    public static void Main(string[] args)
    {
        switch (args.Length)
        {
            case 0:
                FailureHandler.HandleNoUserArgument();
                break;
            case 1:
                ProjectCreator.TryCreateProject(args[0]);
                break;
            default:
                FailureHandler.HandleToMuchUserArguments();
                break;
        }
    }

}




