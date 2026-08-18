namespace Maker;

public sealed record ProjectDirectory(
    string name, IProjectItem[] projectItems) : IProjectItem
{
    public ProjectDirectory(string name) : this(name, [])
    {
    }

    public IProjectItem[] ProjectItems { get; } = projectItems;

    public void Create()
    {
        var projectPath = Directory.GetCurrentDirectory();

        Create(projectPath);
    }

    public void Create(string path)
    {
        var newPath = Path.Join(path, name);
        Directory.CreateDirectory(newPath);

        foreach (var projectItem in projectItems)
        {
            projectItem.Create(newPath);
        }
    }
}