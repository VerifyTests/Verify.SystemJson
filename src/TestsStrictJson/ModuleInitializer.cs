public static class ModuleInitializer
{
    #region StrictJson

    [ModuleInitializer]
    public static void Init() =>
        VerifierSettings.UseStrictJson();

    #endregion

    [ModuleInitializer]
    public static void InitOther()
    {
        VerifierSettings.InitializePlugins();
        DerivePathInfo(
            (_, projectDirectory, type, method) => new(
                directory: projectDirectory,
                typeName: type.Name,
                methodName: method.Name));
    }
}