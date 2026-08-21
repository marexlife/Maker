# Maker
## What does it do
You can create projects for C++. <br>
Those will be very much like Google's layout, but with CMake, not Bazel. <br>
Tough you should be able modify the C# source code pretty easily <br>
to get what what project layout for what you may wish. <br>

### The commands you can use:
'mod my_module' to create a new project module <br>
'new my_project' to create a new project <br>
'run' to run your project <br>
'clear' to clear your project <br>

## Build & Run
You'll need .NET 10: https://dotnet.microsoft.com/en-us/download

Than in the project directory run: <br>
```dotnet run project``` <br>
there you you should see a project directory with a cmake <br>
setup and main.cpp in project/src/main <br>

Look in bin/Debug/net10.0/Maker[.exe] (.exe if your on Windows) <br>
Run that (and leave it in this directory to start the project)
