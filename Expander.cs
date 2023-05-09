using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class JsonExpander
{
    public static JArray Expand(JArray input)
    {
        JArray output = new JArray();
        foreach (JObject item in input)
        {
            ExpandRecursive(item, output);
        }
        return output;
    }

    private static void ExpandRecursive(JObject inputObject, JArray outputArray)
    {
        JArray detailArray = (JArray)inputObject["detail"];
        if (detailArray == null || detailArray.Count == 0)
        {
            outputArray.Add(inputObject);
        }
        else
        {
            foreach (JObject detailObject in detailArray)
            {
                JObject parentObject = new JObject(inputObject);
                parentObject.Remove("detail");
                foreach (JProperty detailProperty in detailObject.Properties())
                {
                    parentObject[detailProperty.Name] = detailProperty.Value;
                }
                ExpandRecursive(parentObject, outputArray);
            }
        }
    }
}
