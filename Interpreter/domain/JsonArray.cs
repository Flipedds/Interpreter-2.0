using Newtonsoft.Json.Linq;

namespace Interpreter.domain;

public class JsonArray
{
    public JArray ArrayJson { get; set; }

    public JsonArray()
    {
        ArrayJson = [];
    }

    public void AdicionarAoArray(JObject objeto)
    {
        ArrayJson.Add(objeto);
    }
    
    public void AdicionarMembroAFuncaoDoArray(
        string nameFunc, string type,
        JObject objeto)
    {
        JObject objetoEncontrado = [];
        foreach (JObject item in ArrayJson.Children<JObject>())
        {
            if (string.Equals(item["Tipo"]?.ToString(), type, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(item["Nome"]?.ToString(), nameFunc, StringComparison.OrdinalIgnoreCase))
            {
                objetoEncontrado = item;
                break;
            }
        }
        if (objetoEncontrado != null)
        {
            JArray membersArray = (JArray)objetoEncontrado["membros"];
            membersArray.Add(objeto);
        }
    }
}