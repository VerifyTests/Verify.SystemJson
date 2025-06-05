using System.Text.Encodings.Web;
using System.Text.Unicode;

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
                if(type.Name == "JsonValueOfElement")
                {
                    // This is a JsonValue<JsonElement>
                    // We need to extract the JsonElement and write it
                    var jsonElement = (JsonElement)type.GetField("Value", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(value)!;
                    writer.WriteValue(jsonElement.GetRawText());
                    return;
                }
                writer.WriteValue(readOnlySpan);
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