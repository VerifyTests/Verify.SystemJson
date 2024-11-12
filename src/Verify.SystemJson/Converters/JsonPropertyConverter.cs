class JsonPropertyConverter :
    WriteOnlyJsonConverter<JsonProperty>
{
    public override void Write(VerifyJsonWriter writer, JsonProperty property)
    {
        var value = property.Value;
        var name = property.Name;
        if (value.ValueKind == JsonValueKind.Null)
        {
            writer.WriteMember(property, null, name);
        }
        else
        {
            writer.WriteMember(property, value, name);
        }
    }
}