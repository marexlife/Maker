using Maker.Items;
using Maker.Config;
using Maker.Actions.Shared;

namespace Maker.Actions.CreateProject;

internal sealed class ProjectTree(string projectName)
{
    private readonly CmakeFlagNameBuilder _cmakeFlagNameBuilder = new(projectName);

    internal DirectoryItem GetProjectTree()
    {
        return new(projectName, [
            new DirectoryItem(NameConfig.SourceDirectoryName, [
                new FileItem(
                    NameConfig.CMakeLists,
                    $"""
                    cmake_minimum_required(VERSION 3.20)

                    add_subdirectory({NameConfig.MainModuleName})
                    """
                ),
                new DirectoryItem(NameConfig.MainModuleName,[
                    new FileItem(
                        NameConfig.CMakeLists,
                        $$"""
                        cmake_minimum_required(VERSION 3.20)
                        project({{NameConfig.MainModuleName}})
                        
                        include(${CMAKE_SOURCE_DIR}/cmake/flags.cmake)

                        set(CMAKE_CXX_STANDARD 20)
                        set(CMAKE_CXX_STANDARD_REQUIRED ON)
                        set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

                        add_executable(${PROJECT_NAME}
                            main.{{NameConfig.CppFileNameExtension}}
                        )

                        target_compile_options(${PROJECT_NAME} PRIVATE
                            ${{{_cmakeFlagNameBuilder.GetProjectFlagsName()}}}
                        )
                        """
                    ),
                    new FileItem(
                        $"main.{NameConfig.CppFileNameExtension}",
                        """
                        #include <iostream>
                    
                        int main() {
                            std::cout << "Hi!\n";
                        }
                        """
                    )
                ])
            ]),
            new DirectoryItem("cmake", [
                new FileItem(
                    "flags.cmake",
                    $"""
                    cmake_minimum_required(VERSION 3.20)

                    if (MSVC)
                        set({_cmakeFlagNameBuilder.GetProjectFlagsName()} 
                            /W4
                        )
                    else()
                        set({_cmakeFlagNameBuilder.GetProjectFlagsName()} 
                            -Wall
                            -Wextra
                            -Wpedantic
                            -Wconversion
                            -Wnrvo
                            -Werror
                        )
                    endif()
                    """
                )
            ]),
            new FileItem(
                NameConfig.CMakeLists,
                $"""
                cmake_minimum_required(VERSION 3.20)
                project({projectName})

                add_subdirectory({NameConfig.SourceDirectoryName})
                """
            ),
            new FileItem("run.sh",
            $"""
            cmake . -B build -GNinja
            cmake --build build
            {NameConfig.GetPosixExecutionPath(projectName)}
            """)
        ]);
    }


}