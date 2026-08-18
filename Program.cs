namespace Maker;


public static class Program
{
    public static void Main(string[] args)
    {
        switch (args.Length)
        {
            case 0:
                FailureHandler.HandleFailure();
                break;
            default:
                ProjectCreator.TryCreateProject(args[0]);
                break;
        }
    }

}




