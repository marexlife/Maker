namespace Maker.CreateProject;

internal sealed class ProjectCreator(ProjectCreationInfo projectCreationInfo)
{
    ProjectTree _projectTree = new(projectCreationInfo.ProjectName);

    internal void TryCreateProject()
    {
        try
        {
            DoCreateProject();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private void DoCreateProject() => _projectTree.GetProjectTree().Create();
}