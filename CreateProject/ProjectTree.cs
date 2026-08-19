using Maker.Items;

namespace Maker.CreateProject;

internal sealed class ProjectTree(string projectName)
{
    const string CMakeLists = "CMakeLists.txt";
    const string SourceDirectoryName = "src";


    internal DirectoryItem GetProjectTree()
    {
        return new(projectName, [
            new DirectoryItem(SourceDirectoryName, [
                new FileItem(
                    CMakeLists,
                    $"""
                    cmake_minimum_required(VERSION 3.20)

                    add_subdirectory(main)
                    """
                ),
                new DirectoryItem("main",[
                    new FileItem(
                        CMakeLists,
                        $$"""
                        cmake_minimum_required(VERSION 3.20)
                        project(main)
                        
                        include(${CMAKE_SOURCE_DIR}/cmake/flags.cmake)

                        set(CMAKE_CXX_STANDARD 20)
                        set(CMAKE_CXX_STANDARD_REQUIRED ON)
                        set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

                        add_executable(${PROJECT_NAME}
                            main.cpp
                        )

                        target_compile_options(${PROJECT_NAME} PRIVATE
                            ${{{GetProjectFlagsName()}}}
                        )
                        """
                    ),
                    new FileItem(
                        "main.cpp",
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
                        set({GetProjectFlagsName()} /W4)
                    else()
                        set({GetProjectFlagsName()} 
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
                CMakeLists,
                $"""
                cmake_minimum_required(VERSION 3.20)
                project({projectName})

                add_subdirectory({SourceDirectoryName})
                """
            )
        ]);
    }

    private string GetProjectFlagsName()
    {
        var screamingSnakeCaseName = Converter.ToScreamingSnakeCase(projectName);

        return $"{screamingSnakeCaseName}_FLAGS";
    }
}