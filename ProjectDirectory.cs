namespace Maker;

internal sealed record ProjectDirectory(
    string name, ProjectItem[] projectItems) : ProjectItem
{
    internal ProjectDirectory(string name) : this(name, [])
    {
    }

    internal ProjectItem[] ProjectItems { get; } = projectItems;

    internal void Create()
    {
        Create(Directory.GetCurrentDirectory());
    }

    internal override void Create(string path)
    {
        var info = Directory.CreateDirectory(name);
        var newPath = Path.Join(path, info.Name);

        foreach (var projectItem in projectItems)
        {
            projectItem.Create(newPath);
        }
    }
}