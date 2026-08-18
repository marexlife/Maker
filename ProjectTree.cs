namespace Maker;

internal sealed class ProjectTree(string projectName)
{
    const string CMakeLists = "CMakeLists.txt";
    const string SourceDirectoryName = "src";

    private string GetProjectFlagsName()
    {
        var screamingSnakeCaseName = Converter.ToScreamingSnakeCase(projectName);

        return $"{screamingSnakeCaseName}_FLAGS";
    }

    internal ProjectDirectory GetProjectTree()
    {
        return new(projectName, [
            new ProjectDirectory(SourceDirectoryName, [
                new ProjectFile(
                    CMakeLists,
                    $"""
                    cmake_minimum_required(VERSION 3.20)

                    add_subdirectory(main)
                    """
                ),
                new ProjectDirectory("main",[
                    new ProjectFile(
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

                        target_compile_flags(${PROJECT_NAME} PRIVATE
                            ${{{GetProjectFlagsName()}}}
                        )
                        """
                    ),
                    new ProjectFile(
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
            new ProjectDirectory("cmake", [
                new ProjectFile(
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
            new ProjectFile(
                CMakeLists,
                $"""
                cmake_minimum_required(VERSION 3.20)
                project({projectName})

                add_subdirectory({SourceDirectoryName})
                """
            )
        ]);
    }
}