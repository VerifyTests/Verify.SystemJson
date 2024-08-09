class JsonNodeConverter :
    WriteOnlyJsonConverter<JsonNode>
{
    public override void Write(VerifyJsonWriter writer, JsonNode value) =>
        writer.Serialize(JsonDocument.Parse(value.ToJsonString()));
}