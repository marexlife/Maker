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
        var classHeaderFileName =
            $"{className}.{NameConfig.CppHeaderFileNameExtension}";
        var classImplementationFileName =
            $"{className}.{NameConfig.CppFileNameExtension}";

        return new(NameConfig.SourceDirectoryName, [
            new DirectoryItem(name, [
                new FileItem(classHeaderFileName,
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
                new FileItem(classImplementationFileName,
                $""""
                #include "{classHeaderFileName}"
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

                add_library(${PROJECT_NAME}
                    {{classImplementationFileName}}
                )

                target_include_directories(${PROJECT_NAME} PUBLIC
                    ${CMAKE_CURRENT_SOURCE_DIR}
                )

                target_compile_options(${PROJECT_NAME} PRIVATE
                    ${{{_cmakeFlagNameBuilder.GetProjectFlagsName()}}}
                )
                """)
            ])
        ]);
    }
}