namespace Maker;

internal abstract record ProjectItem
{
    internal abstract void Create(string path);
}