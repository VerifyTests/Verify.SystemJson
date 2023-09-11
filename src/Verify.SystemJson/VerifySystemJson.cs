namespace VerifyTests;

public static class VerifySystemJson
{
    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        var converters = Argon.DefaultContractResolver.Converters;
        converters.Add(new JsonElementConverter());
        converters.Add(new JsonDocumentConverter());
        converters.Add(new JsonPropertyConverter());
    }
}