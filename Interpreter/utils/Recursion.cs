using System.Text.RegularExpressions;
using Interpreter.domain;

namespace Interpreter.utils;

public class Recursion
{
    public void DefRecursion(Function func, List<Function?> list, int lineCount, ref List<Var?> varList)
    {
        List<string> iterList = func.MyList;
        Patterns patterns = new();
        foreach (var item in iterList)
        {
            if (item.Contains("print"))
            {
                Mapper map = new();
                map.Print(item, lineCount, ref varList, func);
            }
            else if (item != null && Regex.IsMatch(item, patterns.Var) | Regex.IsMatch(item, patterns.StringVar))
            {
                Mapper map = new();
                string name = map.VarName(item);
                dynamic value = map.VarValue(item);

                Var? variable = func.MyVars.Find(obj => obj?.Nome == name);

                if (variable != null)
                {
                    variable.Value = value;
                }

                Var newVariable = new(name, value);
                func.NewVar(newVariable);
            }
            else if (item != null && item.Contains("()"))
            {
                var itemName = item.Split("(");
                var nome = itemName[0].Trim();
                Function? funcs = list.Find(obj => obj?.Nome == nome);

                if (funcs == null)
                {
                    Console.WriteLine($"Erro não foi possível interpretar a linha {lineCount}. Função {nome} não foi encontrada!!");
                    Environment.Exit(0);
                }
                DefRecursion(funcs, list, lineCount, ref varList);
            }
        }
    }
}