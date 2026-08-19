namespace Maker.CreateProject;

internal sealed class ProjectCreator(ProjectCreationInfo projectCreationInfo)
{
    ProjectTree _projectTree = new(projectCreationInfo.ProjectName);

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