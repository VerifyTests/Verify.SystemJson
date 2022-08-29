public static class ModuleInitializer
{
    #region enable

    [ModuleInitializer]
    public static void Init()
    {
        VerifySystemJson.Enable();

        #endregion

        VerifyDiffPlex.Initialize();
    }
}