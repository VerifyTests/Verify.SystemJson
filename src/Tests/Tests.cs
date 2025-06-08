using System.Text.Encodings.Web;
using System.Text.Unicode;
using NUnit.Framework.Legacy;
using JsonSerializer = System.Text.Json.JsonSerializer;

[TestFixture]
public class Tests
{
    string json = """
                  {
                    "short": {
                      "original": "http://www.foo.com/",
                      "short": "foo",
                      "error": {
                        "code": 0,
                        "msg": "No action taken"
                      },
                      "cars": ["Ford", "BMW", "Fiat"],
                      "bool": true,
                      "decimal": 4.2,
                      "withQuotes": "\"value\"",
                      "withUnicodeQuotes": "\u0022value\u0022"
                    }
                  }
                  """;

#pragma warning disable CA1869
    [Test]
    public void EncodingTest()
    {
        var value = new
        {
            value = "\""
        };
        json = JsonSerializer.Serialize(value);
        ClassicAssert.AreEqual(
            """
            {
              "value": "\u0022"
            }
            """,
            json);
    }
    [Test]
    public void EncodingWithRelaxedTest()
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        var value = new
        {
            value = "\""
        };
        json = JsonSerializer.Serialize(value, options);
        ClassicAssert.AreEqual(
            """
            {
              "value": "\""
            }
            """,
            json);
    }

    [Test]
    public Task ScrubMember() =>
        Verify(JsonDocument.Parse(json)).ScrubMember("error");

    [Test]
    public Task TestJsonElement() =>
        Verify(JsonDocument.Parse(json).RootElement);

    [Test]
    public Task TestJsonNode()
    {
        var jsonNode = JsonNode.Parse(json);
        return Verify(jsonNode);
    }

    [Test]
    public Task TestJsonObject() =>
        Verify(JsonNode.Parse(json)!.AsObject());

    [Test]
    public Task NullValue() =>
        Verify(
                JsonDocument.Parse(
                    """
                    {
                      "short": {
                        "a": null,
                        "error": "a"
                      }
                    }
                    """))
            .AddExtraSettings(
                _ =>
                {
                    _.DefaultValueHandling = DefaultValueHandling.Include;
                    _.NullValueHandling = NullValueHandling.Include;
                });

    [Test]
    public Task Numbers() =>
        Verify(
            JsonDocument.Parse(
                """
                {
                    "int": 1,
                    "double": 4.4
                }
                """));
}