# OS Information in .NET

- Import this package in your code

```C#
using System.Runtime.InteropServices;
```

## OS Version

- Get OS Version Information

```C#
string osVersion = RuntimeInformation.OSDescription;
Console.WriteLine($"OS Version: {osVersion}");
```

## OS Platform

- Get OS Platform Information

```C#
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    Console.WriteLine("Running on Windows");
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    Console.WriteLine("Running on Linux");
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    Console.WriteLine("Running on macOS");
}
else
{
    Console.WriteLine("Unknown OS Platform");
}
```

## Architecture (x64, x86, ARM)

- Get System Architecture Information

```C#
Console.WriteLine($"Process Architecture: {RuntimeInformation.ProcessArchitecture}");
```

## .NET Runtime Version

- Get .NET Runtime Version Information

```C#
string frameworkDescription = RuntimeInformation.FrameworkDescription;
Console.WriteLine($"Framework Description: {frameworkDescription}");
```