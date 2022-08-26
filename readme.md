# <img src="/src/icon.png" height="30px"> Verify.SystemJson

[![Build status](https://ci.appveyor.com/api/projects/status/ej794va900x9257f?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-SystemJson)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.SystemJson.svg)](https://www.nuget.org/packages/Verify.SystemJson/)

Adds [Verify](https://github.com/VerifyTests/Verify) support for converting System.Text.Json types.


## NuGet package

https://nuget.org/packages/Verify.SystemJson/


## Usage

<!-- snippet: ModuleInitializer.cs -->
<a id='snippet-ModuleInitializer.cs'></a>
```cs
public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() =>
        VerifySystemJson.Enable();
}
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L1-L6' title='Snippet source file'>snippet source</a> | <a href='#snippet-ModuleInitializer.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Pattern](https://thenounproject.com/term/pattern/130185/) designed by [Jonathan Li](https://thenounproject.com/jjjon/) from [The Noun Project](https://thenounproject.com/).
