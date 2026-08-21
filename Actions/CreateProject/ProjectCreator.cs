namespace Maker.Actions.CreateProject;

internal sealed class ProjectCreator(ProjectCreationInfo projectCreationInfo)
{
    private readonly ProjectTree _projectTree = new(
        projectCreationInfo.ProjectName
    );

    internal void TryCreateProject()
    {
        try
        {
            _projectTree.GetProjectTree().Create();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}