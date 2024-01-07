using Newtonsoft.Json.Linq;

namespace Interpreter.domain;
public class ObjectJson
{
    public static JObject Funcao(string nameFunc, int lineFunc)
    {
        JObject objeto = new ()
        {
            { "Tipo", "def" },
            { "Nome", nameFunc },
            { "linha", lineFunc },
            { "membros", new JArray() }
        };

        return objeto;
    }

    public static JObject Print(string line, int lineCount)
    {
        JObject objeto = new()
        {
            { "Tipo", "print" },
            { "linha", line },
            { "posicao", lineCount }
        };

        return objeto;
    }

    public static JObject Var(string line, string name, dynamic value, int lineCount)
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

    public static JObject ExecDef(
        string funcName, string line, int lineCount)
    {

        JObject objeto = new()
        {
            { "Tipo", "execDef" },
            { "Nome da funcao", funcName },
            { "linha", line },
            { "posicao", lineCount }
        };

        return objeto;
    }
}