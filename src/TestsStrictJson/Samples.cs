[TestFixture]
public class Samples
{
    #region JsonDocumentSampleStrictJson

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

    #endregion
}