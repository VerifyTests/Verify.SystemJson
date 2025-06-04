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
                      }
                    }
                  }
                  """;

    [Test]
    public Task Quotes()
    {
        var json = """
                   {
                     "key": "\"value\""
                   }
                   """;
        return Verify(JsonDocument.Parse(json));
    }

    [Test]
    public Task TestJsonDocument() =>
        Verify(JsonDocument.Parse(json));

    [Test]
    public Task ScrubMember() =>
        Verify(JsonDocument.Parse(json)).ScrubMember("error");

    [Test]
    public Task TestJsonElement() =>
        Verify(JsonDocument.Parse(json).RootElement);

    [Test]
    public Task TestJsonNode() =>
        Verify(JsonNode.Parse(json));

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