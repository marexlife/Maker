# Maker
## What does it do
You can create projects for C++.
Those will be very much like Google's layout, but with CMake, not Bazel.
Tough you should be able modify the C# source code pretty easily 
to get what what project layout for what you may whish. 

## Build & Run
You'll need .NET 10: https://dotnet.microsoft.com/en-us/download

Than in the project directory run: 
```dotnet run project```
there you you should see a project directory with a cmake 
setup and main.cpp in project/src/main

If this succeeded run:
```dotnet publish```


and than look in the bin/Release/net10.0/[Your OS and Arch]/native/Maker[.exe]
(.exe when your on Windows)

That should be your final executable.