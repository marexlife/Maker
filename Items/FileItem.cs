namespace Maker.Items;

public sealed record FileItem(string name, string contents) : IItem
{
    public void Create(string path)
    {
        var filePath = Path.Join(path, name);

        File.WriteAllText(filePath, contents);
    }
}