# <img src="/src/icon.png" height="30px"> Verify.SystemJson

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/ej794va900x9257f?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-SystemJson)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.SystemJson.svg)](https://www.nuget.org/packages/Verify.SystemJson/)

Adds [Verify](https://github.com/VerifyTests/Verify) support for converting System.Text.Json types.

**See [Milestones](../../milestones?state=closed) for release notes.**


## NuGet package

https://nuget.org/packages/Verify.SystemJson/


## Usage

<!-- snippet: enable -->
<a id='snippet-enable'></a>
```cs
[ModuleInitializer]
public static void Init() =>
    VerifySystemJson.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### JsonDocument

<!-- snippet: JsonDocumentSample -->
<a id='snippet-JsonDocumentSample'></a>
```cs
[Test]
public Task JsonDocumentSample()
{
    var json =
        """
        {
          "short": {
            "original": "http://www.foo.com/",
            "short": "foo",
            "error": {
              "code": 0,
              "msg": "No action taken"
            }
          }
        }
        """;

    var document = JsonDocument.Parse(json);
    return Verify(document);
}
```
<sup><a href='/src/Tests/Samples.cs#L4-L27' title='Snippet source file'>snippet source</a> | <a href='#snippet-JsonDocumentSample' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Samples.JsonDocumentSample.verified.txt -->
<a id='snippet-Samples.JsonDocumentSample.verified.txt'></a>
```txt
{
  short: {
    original: http://www.foo.com/,
    short: foo,
    error: {
      code: 0,
      msg: No action taken
    }
  }
}
```
<sup><a href='/src/Tests/Samples.JsonDocumentSample.verified.txt#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.JsonDocumentSample.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

### Strict Json

Note that the above does not result in json files. [This is by design](https://github.com/VerifyTests/Verify/blob/main/docs/serializer-settings.md#not-valid-json). If json files are required then use [UseStrictJson](https://github.com/VerifyTests/Verify/blob/main/docs/serializer-settings.md#usestrictjson)

This can be done at the Globally in a ModuleInitializer

<!-- snippet: StrictJson -->
<a id='snippet-StrictJson'></a>
```cs
[ModuleInitializer]
public static void Init() =>
    VerifierSettings.UseStrictJson();
```
<sup><a href='/src/TestsStrictJson/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-StrictJson' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Or at the test level

<!-- snippet: JsonDocumentSampleStrictJson -->
<a id='snippet-JsonDocumentSampleStrictJson'></a>
```cs
[Test]
public Task JsonDocumentSample()
{
    var json =
        """
        {
          "short": {
            "original": "http://www.foo.com/",
            "short": "foo",
            "error": {
              "code": 0,
              "msg": "No action taken"
            }
          }
        }
        """;

    var document = JsonDocument.Parse(json);
    return Verify(document)
        .UseStrictJson();
}
```
<sup><a href='/src/TestsStrictJson/Samples.cs#L4-L28' title='Snippet source file'>snippet source</a> | <a href='#snippet-JsonDocumentSampleStrictJson' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Samples.JsonDocumentSample.verified.json -->
<a id='snippet-Samples.JsonDocumentSample.verified.json'></a>
```json
{
  "short": {
    "original": "http://www.foo.com/",
    "short": "foo",
    "error": {
      "code": 0,
      "msg": "No action taken"
    }
  }
}
```
<sup><a href='/src/TestsStrictJson/Samples.JsonDocumentSample.verified.json#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.JsonDocumentSample.verified.json' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Ignoring and Scrubbing

Values in the json can be ignored or scrubbed:

<!-- snippet: ScrubIgnoreMember -->
<a id='snippet-ScrubIgnoreMember'></a>
```cs
[Test]
public Task ScrubIgnoreMemberSample()
{
    var json =
        """
        {
          "node": {
            "original": "http://www.foo.com/",
            "short": "foo",
            "error": {
              "code": 0,
              "msg": "No action taken"
            }
          }
        }
        """;

    var document = JsonDocument.Parse(json);
    return Verify(document)
        .ScrubMember("short")
        .IgnoreMember("msg");
}
```
<sup><a href='/src/Tests/Samples.cs#L28-L53' title='Snippet source file'>snippet source</a> | <a href='#snippet-ScrubIgnoreMember' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Samples.ScrubIgnoreMemberSample.verified.txt -->
<a id='snippet-Samples.ScrubIgnoreMemberSample.verified.txt'></a>
```txt
{
  node: {
    original: http://www.foo.com/,
    short: Scrubbed,
    error: {
      code: 0
    }
  }
}
```
<sup><a href='/src/Tests/Samples.ScrubIgnoreMemberSample.verified.txt#L1-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.ScrubIgnoreMemberSample.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Dates and Guid scrubbing

Json values that map to known date and time formats are scrubbed. See [Guids scrubbing](https://github.com/VerifyTests/Verify/blob/main/docs/guids.md) and [Date scrubbing](https://github.com/VerifyTests/Verify/blob/main/docs/dates.md)

<!-- snippet: GuidsAndDates -->
<a id='snippet-GuidsAndDates'></a>
```cs
[Test]
public Task GuidsAndDatesSample()
{
    var json =
        """
        {
          "node": {
            "date": "10/01/2023",
            "short": "foo",
            "error": {
              "guid": "123e4567-e89b-12d3-a456-426614174000",
              "msg": "No action taken"
            }
          }
        }
        """;

    var document = JsonDocument.Parse(json);
    return Verify(document);
}
```
<sup><a href='/src/Tests/Samples.cs#L54-L77' title='Snippet source file'>snippet source</a> | <a href='#snippet-GuidsAndDates' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Samples.GuidsAndDatesSample.verified.txt -->
<a id='snippet-Samples.GuidsAndDatesSample.verified.txt'></a>
```txt
{
  node: {
    date: Date_1,
    short: foo,
    error: {
      guid: Guid_1,
      msg: No action taken
    }
  }
}
```
<sup><a href='/src/Tests/Samples.GuidsAndDatesSample.verified.txt#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.GuidsAndDatesSample.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Inline dates and Guids

Inline dates and Guids can be scrubbed:

<!-- snippet: InlineGuidsAndDates -->
<a id='snippet-InlineGuidsAndDates'></a>
```cs
[Test]
public Task InlineGuidsAndDatesSample()
{
    var json =
        """
        {
          "node": {
            "date": "2023/10/01",
            "short": "foo 2023/10/01",
            "error": {
              "guid": "123e4567-e89b-12d3-a456-426614174000",
              "msg": "No action taken 123e4567-e89b-12d3-a456-426614174000"
            }
          }
        }
        """;

    var document = JsonDocument.Parse(json);
    return Verify(document)
        .ScrubInlineDates("yyyy/MM/dd")
        .ScrubInlineGuids();
}
```
<sup><a href='/src/Tests/Samples.cs#L78-L103' title='Snippet source file'>snippet source</a> | <a href='#snippet-InlineGuidsAndDates' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Samples.InlineGuidsAndDatesSample.verified.txt -->
<a id='snippet-Samples.InlineGuidsAndDatesSample.verified.txt'></a>
```txt
{
  node: {
    date: Date_1,
    short: foo Date_1,
    error: {
      guid: Guid_1,
      msg: No action taken Guid_1
    }
  }
}
```
<sup><a href='/src/Tests/Samples.InlineGuidsAndDatesSample.verified.txt#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-Samples.InlineGuidsAndDatesSample.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Inline date and Guids scrubbing can also be defined globally:

  * [VerifierSettings.ScrubInlineDateTimes](https://github.com/VerifyTests/Verify/blob/main/docs/dates.md#globally-2)
  * [VerifierSettings.ScrubInlineGuids](https://github.com/VerifyTests/Verify/blob/main/docs/guids.md#globally-1)


## Icon

[Pattern](https://thenounproject.com/term/pattern/1070611/) designed by [Trevor Dsouza](https://thenounproject.com/TDsouza/) from [The Noun Project](https://thenounproject.com/).
