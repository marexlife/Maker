namespace Maker.Items;

public sealed record DirectoryItem(
    string name, IItem[] projectItems) : IItem
{
    public DirectoryItem(string name) : this(name, [])
    {
    }

    public IItem[] ProjectItems { get; } = projectItems;

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