using System.Text.RegularExpressions;

namespace Interpreter.mappers;

public class Mapper()
{

    public void Print(string line, int lineCount, ref List<Var?> varList, Function func = null)
    {
        var tokens = line.Split("(");

        string ReplaceA(string str) => str.Replace(")", "");
        string ReplaceC(string str) => str.Trim();

        tokens = tokens.Select((Func<string, string>)ReplaceA).ToArray();
        tokens = tokens.Select((Func<string, string>)ReplaceC).ToArray();

        switch (tokens)
        {
            case ["print", var content]:
                Var? variable = varList.Find(obj => obj?.Nome == content);
                Var? variableB = func?.MyVars.Find(obj => obj?.Nome == content);
                if (content.Contains('"'))
                {
                    content = content.Replace("\"", "");
                    Console.WriteLine(content);
                    break;
                }
                if (variableB != null)
                {
                    Console.WriteLine(variableB.Value);
                    break;
                }
                if (Regex.IsMatch(content, "^[0-9]+\\+[0-9]+$"))
                {
                    string[] partes = content.Split('+');
                    int valueUm = int.Parse(partes[0]);
                    int valueDois = int.Parse(partes[1]);

                    Console.WriteLine(valueUm + valueDois);
                    break;
                }
                if (Regex.IsMatch(content, "^[0-9]+\\-[0-9]+$"))
                {
                    string[] partes = content.Split('-');
                    int valueUm = int.Parse(partes[0]);
                    var valueDois = int.Parse(partes[1]);

                    Console.WriteLine(valueUm - valueDois);
                    break;
                }
                if (Regex.IsMatch(content, "^[0-9]+\\/[0-9]+$"))
                {
                    string[] partes = content.Split('/');
                    int valueUm = int.Parse(partes[0]);
                    var valueDois = int.Parse(partes[1]);

                    Console.WriteLine(valueUm / valueDois);
                    break;
                }
                if (Regex.IsMatch(content, "^[0-9]+\\*[0-9]+$"))
                {
                    string[] partes = content.Split('*');
                    int valueUm = int.Parse(partes[0]);
                    var valueDois = int.Parse(partes[1]);

                    Console.WriteLine(valueUm * valueDois);
                }
                else if (Regex.IsMatch(content, @"^[a-zA-Z]+\+[a-zA-Z]+$"))
                {
                    string[] partes = content.Split("+");
                    var (varUm, vardois) = PrintFind(partes, lineCount, ref varList, func);
                    Console.WriteLine(varUm?.Value + vardois?.Value);
                }
                else if (Regex.IsMatch(content, @"^[a-zA-Z]+\-[a-zA-Z]+$"))
                {
                    string[] partes = content.Split("-");
                    var (varUm, vardois) = PrintFind(partes, lineCount, ref varList, func);
                    Console.WriteLine(varUm?.Value - vardois?.Value);
                }
                else if (Regex.IsMatch(content, @"^[a-zA-Z]+\*[a-zA-Z]+$"))
                {
                    string[] partes = content.Split("*");
                    var (varUm, vardois) = PrintFind(partes, lineCount, ref varList, func);
                    Console.WriteLine(varUm?.Value * vardois?.Value);
                }
                else if (Regex.IsMatch(content, @"^[a-zA-Z]+\/[a-zA-Z]+$"))
                {
                    string[] partes = content.Split("/");
                    var (varUm, vardois) = PrintFind(partes, lineCount, ref varList, func);
                    Console.WriteLine(varUm?.Value / vardois?.Value);
                }
                else if (variable != null)
                {
                    Console.WriteLine(variable.Value);
                }
                else
                {
                    Console.WriteLine($"Erro não foi possível interpretar a linha {lineCount}");
                    Environment.Exit(0);
                }
                break;
        }
    }

    public (Var, Var) PrintFind(string[] partes, int lineCount, ref List<Var?> varList, Function func = null)
    {
        Var? varUm = varList.Find(obj => obj?.Nome == partes[0]);
        Var? vardois = varList.Find(obj => obj?.Nome == partes[1]);
        if (varUm == null | vardois == null)
        {
            if (func != null)
            {
                varUm = func.MyVars.Find(obj => obj?.Nome == partes[0]);
                vardois = func.MyVars.Find(obj => obj?.Nome == partes[1]);

                if (varUm == null | vardois == null)
                {
                    Console.WriteLine($"Erro não foi possível interpretar a linha {lineCount}. " +
                              $"Variáveis {partes[0]} ou {partes[1]} não encontradas !");
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine($"Erro não foi possível interpretar a linha {lineCount}. " +
                              $"Variáveis {partes[0]} ou {partes[1]} não encontradas !");
                Environment.Exit(0);
            }

        }

        if (varUm?.Value.GetType() == typeof(int) && vardois?.Value.GetType() == typeof(string) || varUm?.Value.GetType() == typeof(string) && vardois?.Value.GetType() == typeof(int))
        {
            Console.WriteLine($"Erro não foi possível interpretar a linha {lineCount}." +
                              $" Coerção de tipos entre inteiro e string");
            Environment.Exit(0);
        }

        return (varUm, vardois);
    }

    public string Def(string line)
    {
        var tokens = line.Split(" ");

        var name = tokens[1];
        name = name.Replace(")", "");
        name = name.Replace("(", "");
        name = name.Replace(":", "");
        return name;
    }

    public string VarName(string line)
    {
        var tokens = line.Split("=");
        var name = tokens[0].Trim();


        return name;
    }

    public dynamic VarValue(string line)
    {
        var tokens = line.Split("=");
        if (line.Contains('\"'))
        {
            tokens[1] = tokens[1].Replace("\"", "");
            tokens[1] = tokens[1].Trim();
            return tokens[1];
        }
        tokens[1] = tokens[1].Trim();
        int value = int.Parse(tokens[1]);

        return value;
    }

}