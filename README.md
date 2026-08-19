# Maker
## What does it do
You can create projects for C++.
Those will be very much like Google's layout, but with CMake, not Bazel.
Tough you should be able modify the C# source code pretty easily 
to get what what project layout for what you may wish. <br>
```Maker new my_project```

You can also create cmake-sublibraries where 
you can link your main executable to them. <br>
```Maker mod new_module```

## Build & Run
You'll need .NET 10: https://dotnet.microsoft.com/en-us/download

Than in the project directory run: 
```dotnet run project```
there you you should see a project directory with a cmake 
setup and main.cpp in project/src/main

Look in bin/Debug/net10.0/Maker[.exe] (.exe if your on Windows)
Run that (and leave it in this directory to start the project)
