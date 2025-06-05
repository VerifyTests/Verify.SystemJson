class JsonObjectConverter :
    WriteOnlyJsonConverter<JsonObject>
{
    public override void Write(VerifyJsonWriter writer, JsonObject value)
    {
        writer.WriteStartObject();
        foreach (var node in value)
        {
            // if (node.Value?.GetValueKind() == JsonValueKind.String)
            // {
            //     var s = node.ToString();
            //     writer.WriteMember(value, s, node.Key);
            //     continue;
            // }

            var nodeValue = node.Value!;
            var type = nodeValue.GetType();
            var jsonElement = (JsonElement)type.GetField("Value", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(nodeValue)!;
            var rawText = jsonElement.GetRawText();
            var s = nodeValue.ToString();
            var jsonString = nodeValue.ToJsonString();
            var s1 = node.ToString();
            writer.WriteMember(value, node.Value, node.Key);
        }
        writer.WriteEndObject();
    }
}