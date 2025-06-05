class JsonValueConverter :
    WriteOnlyJsonConverter<JsonValue>
{
    public override void Write(VerifyJsonWriter writer, JsonValue value)
    {
        var kind = value.GetValueKind();
        switch (kind)
        {
            case JsonValueKind.Object:
                writer.Serialize(value.AsObject());
                break;
            case JsonValueKind.Array:
                writer.Serialize(value.AsArray());
                break;
            case JsonValueKind.String:
                var type = value.GetType();
                if (type.Name == "JsonValueOfElement")
                {
                    var element = (JsonElement) type.GetField("Value", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(value)!;
                    writer.Serialize(element);
                    return;
                }

                writer.WriteValue(value.GetValue<string>());
                break;
            case JsonValueKind.Number:
                if (value.TryGetValue<int>(out var valueAsLong))
                {
                    writer.WriteValue(valueAsLong);
                }
                else
                {
                    writer.WriteValue(value.GetValue<double>());
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