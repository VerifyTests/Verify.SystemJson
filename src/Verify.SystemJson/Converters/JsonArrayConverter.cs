class JsonArrayConverter :
    WriteOnlyJsonConverter<JsonArray>
{
    public override void Write(VerifyJsonWriter writer, JsonArray value)
    {
        writer.WriteStartArray();
        foreach (var item in value)
        {
            if (item is null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.Serialize(item);
            }
        }
        writer.WriteEndArray();
    }
}