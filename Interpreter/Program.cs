using System.Text.RegularExpressions;
using Interpreter.utils;

StreamReader sr = new(args[0]);

string? line = sr.ReadLine();
string nameFunc = "";
int lineFunc = 0;
int lineCount = 1;
List<Function?> funcList = [];
List<Var?> varList = [];

while (!sr.EndOfStream)
{
    Patterns patterns = new();

    if (line != null
    && Regex.IsMatch(line, patterns.Print)
    | Regex.IsMatch(line, patterns.PrintNumber)
    | Regex.IsMatch(line, patterns.PrintVar))
    {
        if (nameFunc != ""
        && Regex.IsMatch(line, patterns.DefPrint)
        | Regex.IsMatch(line, patterns.DefPrintNumber)
        | Regex.IsMatch(line, patterns.DefPrintVar))
        {
            Function? func = funcList.Find(obj => obj?.Nome == nameFunc);
            func?.Add(line);
            line = sr.ReadLine();
            lineCount++;
            continue;
        }
        else if(nameFunc != ""
        && !Regex.IsMatch(line, patterns.DefPrint)
        | !Regex.IsMatch(line, patterns.DefPrintNumber)
        | !Regex.IsMatch(line, patterns.DefPrintVar)
        )
        {
            Console.WriteLine($"Erro: não foi possível interpretar a linha {lineCount} esperada uma identação depois da definição de uma função na linha {lineFunc} !");
            break;
        }
        
        Mapper map = new();
        map.Print(line, lineCount, ref varList);
        line = sr.ReadLine();
        lineCount++;
    }
    else if (line != null
    && Regex.IsMatch(line, patterns.Def))
    {
        Mapper map = new();
        nameFunc = map.Def(line);
        lineFunc = lineCount;
        Function? func = new(nameFunc);
        funcList.Add(func);
        line = sr.ReadLine();
        lineCount++;
    }
    else if (line != null
    && Regex.IsMatch(line, patterns.ExecDef))
    {
        if (nameFunc != ""
        && Regex.IsMatch(line, patterns.DefExecDef))
        {
            Function? funcs = funcList.Find(obj => obj?.Nome == nameFunc);
            funcs?.Add(line);
            line = sr.ReadLine();
            lineCount++;
            continue;
        }
        else if(nameFunc != ""
        && !Regex.IsMatch(line, patterns.DefExecDef))
        {
            Console.WriteLine($"Erro: não foi possível interpretar a linha {lineCount} esperada uma identação depois da definição de uma função na linha {lineFunc} !");
            break;
        }
        var name = line.Split("(");
        Function? func = funcList.Find(obj => obj?.Nome == name[0]);

        if (func != null)
        {
            new Recursion().DefRecursion(func, funcList, lineCount, ref varList);
            line = sr.ReadLine();
            lineCount++;
        }
        else
        {
            Console.WriteLine($"Erro: não foi possível interpretar a linha {lineCount}. Função {name[0]} não foi encontrada!");
            break;
        }
    }
    else if (line == "" | string.IsNullOrWhiteSpace(line))
    {
        line = sr.ReadLine();
        nameFunc = "";
        lineFunc = 0;
        lineCount++;
    }
    else if (line != null 
    && line.Contains('='))
    {
        if (nameFunc != ""
        && Regex.IsMatch(line, patterns.DefVar)
        |  Regex.IsMatch(line, patterns.DefStringVar))
        {
            Function? funcs = funcList.Find(obj => obj?.Nome == nameFunc);
            funcs?.Add(line.Trim());
            line = sr.ReadLine();
            lineCount++;
            continue;
        }
        else if(nameFunc != ""
        && !Regex.IsMatch(line, patterns.DefVar)
        |  !Regex.IsMatch(line, patterns.DefStringVar))
        {
            Console.WriteLine($"Erro: não foi possível interpretar a linha {lineCount} esperada uma identação depois da definição de uma função na linha {lineFunc} !");
            break;
        }
        Mapper map = new();
        string name = map.VarName(line);
        dynamic value = map.VarValue(line);

        Var? variable = varList.Find(obj => obj?.Nome == name);

        if (variable != null)
        {
            variable.Value = value;
            line = sr.ReadLine();
            lineCount++;
            continue;
        }

        Var newVariable = new(name, value);
        varList.Add(newVariable);
        line = sr.ReadLine();
        lineCount++;
    }
    else
    {
        Console.WriteLine($"Erro: não foi possível interpretar a linha {lineCount}");
        break;
    }
}



/*
 * todo
 * variáveis de escopo de função
 * lambda funcs
 */