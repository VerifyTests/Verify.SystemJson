class JsonDocumentConverter :
    WriteOnlyJsonConverter<JsonDocument>
{
    public override void Write(VerifyJsonWriter writer, JsonDocument value) =>
        writer.Serialize(value.RootElement);
}