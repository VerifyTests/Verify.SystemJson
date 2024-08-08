using System.Text.Json;
using System.Text.Json.Nodes;

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
            }
          }
        }
        """;

    [Fact]
    public Task TestJsonDocument() =>
        Verify(JsonDocument.Parse(json));

    [Fact]
    public Task TestJsonElement() =>
        Verify(JsonDocument.Parse(json).RootElement);

    [Fact]
    public Task TestJsonNode() =>
        Verify(JsonNode.Parse(json));

    [Fact]
    public Task TestJsonObject() =>
        Verify(JsonNode.Parse(json)!.AsObject());

    [Fact]
    public Task NullValue() =>
        Verify(JsonDocument.Parse("""
        {
          "short": {
            "a": null,
            "error": "a"
          }
        }
        """));
}