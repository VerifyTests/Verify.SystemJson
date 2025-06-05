class JsonElementConverter :
    WriteOnlyJsonConverter<JsonElement>
{
    public override void Write(VerifyJsonWriter writer, JsonElement value)
    {
        var kind = value.ValueKind;
        switch (kind)
        {
            case JsonValueKind.Object:
                writer.WriteStartObject();
                foreach (var item in value.EnumerateObject())
                {
                    writer.Serialize(item);
                }

                writer.WriteEndObject();
                break;
            case JsonValueKind.Array:
                writer.WriteStartArray();
                foreach (var item in value.EnumerateArray())
                {
                    writer.Serialize(item);
                }

                writer.WriteEndArray();
                break;
            case JsonValueKind.String:
                writer.WriteValue(value.GetString());
                break;
            case JsonValueKind.Number:
                if (value.TryGetInt64(out var valueAsLong))
                {
                    writer.WriteValue(valueAsLong);
                }
                else
                {
                    writer.WriteValue(value.GetDouble());
                }

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
                throw new($"Unsupported JsonValueKind: {kind}");
        }
    }
}