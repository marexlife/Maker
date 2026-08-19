using Maker.Config;
using Maker.Items;
using Maker.Utils;

namespace Maker.CreateModule;

internal sealed class ModuleTree(string name)
{
    internal DirectoryItem GetModuleTree()
    {
        var includeGuard = $"{NameConverter.ToScreamingSnakeCase(name)}_H";

        return new(NameConfig.SourceDirectoryName, [
            new DirectoryItem(name, [
                new FileItem(name,
                $$"""
                #ifndef {{includeGuard}}
                #define {{includeGuard}}
                namespace {{name}} {
                class {{name}} final {
                   public:
                };
                }
                #endif {{includeGuard}}
                """
                )
            ])
        ]);
    }
}