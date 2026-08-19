namespace Maker.Items;

public sealed record FileItem(string name, string contents) : IItem
{
    public void Create(string path)
    {
        var filePath = Path.Join(path, name);

        var textBuffer = string.Empty;

        if (File.Exists(filePath))
        {
            textBuffer += File.ReadAllText(filePath);
        }

        textBuffer += contents;

        File.WriteAllText(filePath, textBuffer);
    }
}