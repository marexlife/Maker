using Maker.Config;
using Maker.Items;
using Maker.Utils;

namespace Maker.CreateModule;

internal sealed class ModuleTree(string name)
{
    internal DirectoryItem GetModuleTree()
    {
        var projectName = ProjectNameInferService.InferProjectName();
        var screamingSnakeCaseModuleName = NameConverter.ToScreamingSnakeCase(name);
        var screamingSnakeCaseProjectName = NameConverter.ToScreamingSnakeCase(projectName);
        var includeGuard = $"{screamingSnakeCaseProjectName}_{screamingSnakeCaseModuleName}_H";
        var namespaceName = $"{projectName}::{name}";
        var className = NameConverter.ToPascalCase(name);

        return new(NameConfig.SourceDirectoryName, [
            new DirectoryItem(name, [
                new FileItem(name,
                $$"""
                #ifndef {{includeGuard}}
                #define {{includeGuard}}
                namespace {{namespaceName}} {
                class {{className}} final {
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