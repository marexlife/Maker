using Maker;

public sealed record ProjectFile(string name, string contents) : IProjectItem
{
    public void Create(string path)
    {
        var filePath = Path.Join(path, name);

        File.WriteAllText(filePath, contents);
    }
}