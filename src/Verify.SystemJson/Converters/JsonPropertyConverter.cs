class JsonPropertyConverter :
    WriteOnlyJsonConverter<JsonProperty>
{
    public override void Write(VerifyJsonWriter writer, JsonProperty value)
    {
        writer.WritePropertyName(value.Name);
        if (value.Value.ValueKind == JsonValueKind.Null)
        {
            writer.WriteNull();
        }
        else
        {
            writer.Serialize(value.Value);
        }
    }
}