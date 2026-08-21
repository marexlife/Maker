using Maker.Config;
using Maker.Items;
using Maker.Utils;
using Maker.Actions.Shared;

namespace Maker.Actions.CreateModule;

internal sealed class ModuleTree(ModuleCreationInfo moduleCreationInfo)
{
    private readonly CmakeFlagNameBuilder _cmakeFlagNameBuilder = new(moduleCreationInfo.ModuleName);

    private readonly string _className = NameConverter.ToPascalCase(moduleCreationInfo.ModuleName);
    private readonly string _namespaceName = $"{moduleCreationInfo.ProjectName}::{moduleCreationInfo}";

    private string IncludeGuard
    {
        get
        {
            var screamingSnakeCaseModuleName =
                NameConverter.ToScreamingSnakeCase(moduleCreationInfo.ModuleName);
            var screamingSnakeCaseProjectName =
                NameConverter.ToScreamingSnakeCase(moduleCreationInfo.ProjectName);
            var screamingSnakeCaseClassName = NameConverter.ToScreamingSnakeCase(_className);

            return $"{screamingSnakeCaseProjectName}_{screamingSnakeCaseModuleName}_{screamingSnakeCaseClassName}_H";
        }
    }

    private string ClassHeaderFileName =>
        $"{_className}.{NameConfig.CppHeaderFileNameExtension}";
    private string ClassImplementationFileName =>
        $"{_className}.{NameConfig.CppFileNameExtension}";

    internal DirectoryItem GetModuleTree()
    {
        var moduleDirectory = new DirectoryItem(NameConfig.SourceDirectoryName, [
            new DirectoryItem(moduleCreationInfo.ModuleName, [
                new FileItem(ClassHeaderFileName,
                $$"""
                #ifndef {{IncludeGuard}}
                #define {{IncludeGuard}}
                namespace {{_namespaceName}} {
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
                project({{moduleCreationInfo}}})
                
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
            add_subdirectory({moduleCreationInfo})
            """)
        ]);

        Console.WriteLine(
        $$"""
        Use this to link the new sub-library:

        target_link_library(${PROJECT_NAME} PUBLIC
            {{moduleCreationInfo}}
        )
        """
        );

        return moduleDirectory;
    }
}