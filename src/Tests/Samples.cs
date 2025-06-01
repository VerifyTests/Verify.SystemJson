[TestFixture]
public class Samples
{
    #region JsonDocumentSample

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

    #endregion
    #region ScrubIgnoreMember

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

    #endregion
    #region GuidsAndDates

    [Test]
    public Task GuidsAndDatesSample()
    {
        var json =
            """
            {
              "node": {
                "date": "1/10/2023",
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

    #endregion
    #region InlineGuidsAndDates

    [Test]
    public Task InlineGuidsAndDatesSample()
    {
        var json =
            """
            {
              "node": {
                "date": "01/10/2023",
                "short": "foo 01/10/2023",
                "error": {
                  "guid": "123e4567-e89b-12d3-a456-426614174000",
                  "msg": "No action taken 123e4567-e89b-12d3-a456-426614174000"
                }
              }
            }
            """;

        var document = JsonDocument.Parse(json);
        return Verify(document)
            .ScrubInlineDates("dd/MM/yyyy")
            .ScrubInlineGuids();
    }

    #endregion
}