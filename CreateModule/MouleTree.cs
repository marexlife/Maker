using Maker.Config;
using Maker.Items;
using Maker.Utils;
using Maker.CreationShared;

namespace Maker.CreateModule;

internal sealed class ModuleTree(string moduleName)
{
    private readonly CmakeFlagNameBuilder _cmakeFlagNameBuilder = new(moduleName);

    private readonly string _projectName = ProjectNameInferService.InferProjectName();

    private string IncludeGuard
    {
        get
        {
            var screamingSnakeCaseModuleName =
                NameConverter.ToScreamingSnakeCase(moduleName);
            var screamingSnakeCaseProjectName =
                NameConverter.ToScreamingSnakeCase(_projectName);
            return $"{screamingSnakeCaseProjectName}_{screamingSnakeCaseModuleName}_H";
        }
    }

    private string NamespaceName => $"{_projectName}::{moduleName}";
    private readonly string _className = $"{NameConverter.ToPascalCase(moduleName)}r";
    private string ClassHeaderFileName =>
        $"{_className}.{NameConfig.CppHeaderFileNameExtension}";
    private string ClassImplementationFileName =>
        $"{_className}.{NameConfig.CppFileNameExtension}";

    internal DirectoryItem GetModuleTree()
    {

        var moduleDirectory = new DirectoryItem(NameConfig.SourceDirectoryName, [
            new DirectoryItem(moduleName, [
                new FileItem(ClassHeaderFileName,
                $$"""
                #ifndef {{IncludeGuard}}
                #define {{IncludeGuard}}
                namespace {{NamespaceName}} {
                class {{_className}} final {
                   public:
                };
                }
                #endif // {{IncludeGuard}}
                """
                ),
                new FileItem(ClassImplementationFileName,
                $""""
                #include "{ClassHeaderFileName}"
                """"
                ),
                new FileItem(NameConfig.CMakeLists,
                $$"""
                cmake_minimum_required(VERSION 3.20)
                project({{moduleName}}})
                
                include(${CMAKE_SOURCE_DIR}/cmake/flags.cmake)

                set(CMAKE_CXX_STANDARD 20)
                set(CMAKE_CXX_STANDARD_REQUIRED ON)
                set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

                add_library(${PROJECT_NAME}
                    {{ClassImplementationFileName}}
                )

                target_include_directories(${PROJECT_NAME} PUBLIC
                    ${CMAKE_CURRENT_SOURCE_DIR}
                )

                target_compile_options(${PROJECT_NAME} PRIVATE
                    ${{{_cmakeFlagNameBuilder.GetProjectFlagsName()}}}
                )
                """)
            ]),
            new FileItem(NameConfig.CMakeLists,
            $"""
            add_subdirectory({moduleName})
            """)
        ]);

        Console.WriteLine(
        $$"""
        Use this to link the new sub-library:

        target_link_library(${PROJECT_NAME} PUBLIC
            {{moduleName}}
        )
        """
        );

        return moduleDirectory;
    }
}