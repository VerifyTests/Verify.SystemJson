class JsonNodeConverter :
    WriteOnlyJsonConverter<JsonNode>
{
    public override void Write(VerifyJsonWriter writer, JsonNode value) =>
        writer.Serialize(JsonDocument.Parse(value.ToJsonString()));
}

class JsonValueConverter :
    WriteOnlyJsonConverter<JsonValue>
{
    public override void Write(VerifyJsonWriter writer, JsonValue value)
    {
        switch (value.GetValueKind())
        {
            case JsonValueKind.Object:
                writer.Serialize(value.AsObject());
                break;
            case JsonValueKind.Array:
                writer.Serialize(value.AsArray());
                break;
            case JsonValueKind.String:
                writer.WriteValue(value.GetValue<string>());
                break;
            case JsonValueKind.Number:
                writer.WriteValue(value.GetValue<string>());
                break;
            case JsonValueKind.True:
                writer.WriteValue(true);
                break;
            case JsonValueKind.False:
                writer.WriteValue(false);
                break;
            case JsonValueKind.Null:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}