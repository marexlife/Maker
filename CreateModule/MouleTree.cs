using Maker.Config;
using Maker.Items;
using Maker.Utils;
using Maker.CreationShared;

namespace Maker.CreateModule;

internal sealed class ModuleTree(string name)
{
    private CmakeFlagNameBuilder _cmakeFlagNameBuilder = new(name);

    internal DirectoryItem GetModuleTree()
    {
        var projectName =
            ProjectNameInferService.InferProjectName();
        var screamingSnakeCaseModuleName =
            NameConverter.ToScreamingSnakeCase(name);
        var screamingSnakeCaseProjectName =
            NameConverter.ToScreamingSnakeCase(projectName);
        var includeGuard =
            $"{screamingSnakeCaseProjectName}_{screamingSnakeCaseModuleName}_H";
        var namespaceName =
            $"{projectName}::{name}";
        var className =
            NameConverter.ToPascalCase(name);
        var classHeaderName =
            $"{className}.{NameConfig.CppHeaderFileNameExtension}";
        var classFileName =
            $"{className}.{NameConfig.CppFileNameExtension}";

        return new(NameConfig.SourceDirectoryName, [
            new DirectoryItem(name, [
                new FileItem(classHeaderName,
                $$"""
                #ifndef {{includeGuard}}
                #define {{includeGuard}}
                namespace {{namespaceName}} {
                class {{className}} final {
                   public:
                };
                }
                #endif // {{includeGuard}}
                """
                ),
                new FileItem(classFileName,
                $""""
                #include "{classHeaderName}"
                """"
                ),
                new FileItem(NameConfig.CMakeLists,
                $$"""
                cmake_minimum_required(VERSION 3.20)
                project({{name}}})
                
                include(${CMAKE_SOURCE_DIR}/cmake/flags.cmake)

                set(CMAKE_CXX_STANDARD 20)
                set(CMAKE_CXX_STANDARD_REQUIRED ON)
                set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

                add_executable(${PROJECT_NAME}
                    main.cpp
                )

                target_compile_options(${PROJECT_NAME} PRIVATE
                    ${{{_cmakeFlagNameBuilder.GetProjectFlagsName()}}}
                )
                """)
            ])
        ]);
    }
}