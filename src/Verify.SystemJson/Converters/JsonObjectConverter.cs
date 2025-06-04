class JsonObjectConverter :
    WriteOnlyJsonConverter<JsonObject>
{
    public override void Write(VerifyJsonWriter writer, JsonObject value)
    {
        writer.WriteStartObject();
        foreach (var node in value)
        {
            writer.WriteMember(value, node.Value, node.Key);
        }
        writer.WriteEndObject();
    }
}