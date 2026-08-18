using Maker;

internal sealed record ProjectFile(string name) : ProjectItem
{
    internal override void Create(string path)
    {
        var filePath = Path.Join(path, name);

        File.Create(filePath);
    }
}