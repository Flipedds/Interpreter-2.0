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

    public JObject AdicionarFuncao(string nameFunc, int lineFunc)
    {
        JObject objeto = new JObject
        {
            { "Tipo", "def" },
            { "Nome", nameFunc },
            { "linha", lineFunc },
            { "membros", new JArray() }
        };

        return objeto;
    }

    public JObject AdicionarPrint(string line, int lineCount)
    {
        JObject objeto = new()
        {
            { "Tipo", "print" },
            { "linha", line },
            { "posicao", lineCount }
        };

        return objeto;
    }

    public JObject AdicionarVar(string line, string name, dynamic value, int lineCount)
    {
        JObject objeto = new()
        {
            { "Tipo", "var" },
            { "Nome", name },
            { "Valor", value },
            { "linha", line },
            { "posicao", lineCount }
        };

        return objeto;
    }

    public JObject AdicionarExecDef(
        string funcName, string line, int lineCount)
    {

        JObject objeto = new()
        {
            { "Tipo", "execDef" },
            { "Nome", funcName },
            { "linha", line },
            { "posicao", lineCount }
        };

        return objeto;
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