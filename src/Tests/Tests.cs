using System.Text.Json;

[UsesVerify]
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