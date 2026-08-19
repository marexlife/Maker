using Maker.Items;

namespace Maker.CreateModule;

internal sealed class ModuleTree(string name)
{
    internal DirectoryItem GetModuleTree()
    {
        return new(name);
    }
}