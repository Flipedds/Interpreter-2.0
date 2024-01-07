using Newtonsoft.Json.Linq;

namespace Interpreter.domain;

public class JsonArray
{
    public JArray ArrayJson { get; set; }

    public JsonArray()
    {
        ArrayJson = [];
    }

    public void AdicionarFuncaoAoArray(string nameFunc, int lineFunc)
    {
        JObject objeto = new JObject
        {
            { "Tipo", "def" },
            { "Nome", nameFunc },
            { "linha", lineFunc },
            { "members", new JArray() }
        };

        ArrayJson.Add(objeto);
    }

    public void AdicionarMembroAFuncaoDoArray(
        string nameFunc, string type,
        string line, int lineCount, string memberType)
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
            JArray membersArray = (JArray)objetoEncontrado["members"];
            if(memberType == "print")
            {
                JObject novoItem = new()
            {
                { "Tipo", "print" },
                { "linha", line },
                { "posicao", lineCount }
            };
                membersArray.Add(novoItem);
            }
        }
    }

    public void AdicionarPrintAoArray(string line, int lineCount)
    {
        JObject objeto = new()
        {
            { "Tipo", "print" },
            { "linha", line },
            { "posicao", lineCount }
        };

        ArrayJson.Add(objeto);
    }
}