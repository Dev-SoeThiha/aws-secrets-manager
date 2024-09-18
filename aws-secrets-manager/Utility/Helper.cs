using System.Text.Json;

namespace aws_secrets_manager.Utility
{
    public class Helper
    {
        public static Dictionary<string, string> ParseJsonToDictionary(JsonElement element, string prefix = "")
        {
            var dictionary = new Dictionary<string, string>();

            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (var property in element.EnumerateObject())
                    {
                        var newPrefix = string.IsNullOrEmpty(prefix) ? property.Name : $"{prefix}:{property.Name}";
                        foreach (var kvp in ParseJsonToDictionary(property.Value, newPrefix))
                        {
                            dictionary[kvp.Key] = kvp.Value;
                        }
                    }
                    break;

                case JsonValueKind.Array:
                    int index = 0;
                    foreach (var item in element.EnumerateArray())
                    {
                        foreach (var kvp in ParseJsonToDictionary(item, $"{prefix}[{index}]"))
                        {
                            dictionary[kvp.Key] = kvp.Value;
                        }
                        index++;
                    }
                    break;

                case JsonValueKind.String:
                    dictionary[prefix] = element.GetString();
                    break;

                case JsonValueKind.Number:
                    dictionary[prefix] = element.GetDecimal().ToString();
                    break;

                case JsonValueKind.True:
                case JsonValueKind.False:
                    dictionary[prefix] = element.GetBoolean().ToString();
                    break;

                case JsonValueKind.Null:
                    dictionary[prefix] = "null";
                    break;
            }

            return dictionary;
        }
    }
}
