using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public static class JsonExpander
{
    public static JArray Expand(JObject input, string arrayPropertyName)
    {
        List<JObject> expandedObjects = new List<JObject>();

        foreach (JObject obj in input[arrayPropertyName])
        {
            JObject expandedObj = new JObject();
            foreach (JProperty property in input.Properties())
            {
                if (property.Name == arrayPropertyName)
                {
                    continue;
                }

                expandedObj[property.Name] = property.Value;
            }

            foreach (JProperty property in obj.Properties())
            {
                expandedObj[property.Name] = property.Value;
            }

            expandedObjects.Add(expandedObj);
        }

        return new JArray(expandedObjects.ToArray());
    }
}
