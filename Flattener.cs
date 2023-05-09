using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public static class JsonFlattener
{
    public static JArray Flatten(JObject input, string childArrayPropertyName)
    {
        JArray output = new JArray();
        foreach (JObject child in input[childArrayPropertyName])
        {
            JObject item = new JObject();
            foreach (JProperty property in input.Properties())
            {
                if (property.Name == childArrayPropertyName)
                {
                    continue;
                }

                if (property.Value is JArray arrayValue)
                {
                    for (int i = 0; i < arrayValue.Count; i++)
                    {
                        JToken arrayItem = arrayValue[i];
                        item[property.Name + "[" + i + "]"] = arrayItem;
                    }
                }
                else
                {
                    item[property.Name] = property.Value;
                }
            }

            foreach (JProperty childProperty in child.Properties())
            {
                item[childArrayPropertyName + "." + childProperty.Name] = childProperty.Value;
            }

            output.Add(item);
        }

        return output;
    }
}
