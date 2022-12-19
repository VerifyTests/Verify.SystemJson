namespace VerifyTests;

public static class VerifySystemJson
{
    public static void Enable()
    {
        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings
            .AddExtraSettings(_ =>
            {
                var converters = _.Converters;
                converters.Add(new JsonElementConverter());
                converters.Add(new JsonDocumentConverter());
                converters.Add(new JsonPropertyConverter());
            });
    }
}