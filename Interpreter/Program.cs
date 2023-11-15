using System.Text.RegularExpressions;
using Interpreter.mappers;

StreamReader sr = new ("C:\\Users\\felip\\RiderProjects\\Interpreter\\Interpreter\\main.py");

string? line = sr.ReadLine();
string nameFunc = "";
int lineCount = 1;
List<Function?> funcList = new List<Function?>();
List<Var?> varList = new();

while (!sr.EndOfStream)
{
    string? print = new Patterns().Print;
    string? printNumber = new Patterns().PrintNumber;
    string? printVar = new Patterns().PrintVar;
    string? pattern = new Patterns().Def;
    string? execDef = new Patterns().ExecDef;
    string? var = new Patterns().Var;
    string? stringVar = new Patterns().StringVar;
    
    if (line != null && printNumber != null && print != null && printVar != null && Regex.IsMatch(line, print) | Regex.IsMatch(line, printNumber) | Regex.IsMatch(line, printVar))
    {
        if (nameFunc != "")
        {
            Function? func = funcList.Find(obj => obj?.Nome == nameFunc);
            func?.Add(line);
            line = sr.ReadLine();
            lineCount++;
            continue;
        }
        Mapper map = new();
        map.Print(line, lineCount, ref varList);
        line = sr.ReadLine();
        lineCount++;
    }
    else if (line != null && pattern != null && Regex.IsMatch(line, pattern))
    {
        Mapper map = new();
        nameFunc = map.Def(line);
        Function? func = new(nameFunc);
        funcList.Add(func);
        line = sr.ReadLine();
        lineCount++;
    }
    else if (line != null && execDef != null && Regex.IsMatch(line, execDef))
    {   
        if(nameFunc != ""){
            Function? funcs = funcList.Find(obj => obj?.Nome == nameFunc);
            funcs?.Add(line);
            line = sr.ReadLine();
            lineCount++;
            continue;
        }
        var name = line.Split("(");
        Function? func = funcList.Find(obj => obj?.Nome == name[0]);
        
        if ( func != null)
        {
            new Recursion().DefRecursion(func, funcList, lineCount, ref varList);
            line = sr.ReadLine();
            lineCount++;
        }
        else
        {
            Console.WriteLine($"Erro não foi possível interpretar a linha {lineCount}. Função {name[0]} não foi encontrada!");
            break;
        }
    }
    else if(line == "" | string.IsNullOrWhiteSpace(line))
    {   
        line = sr.ReadLine();
        nameFunc = "";
        lineCount++;
    }
    else if (line != null && var!= null && Regex.IsMatch(line, var) | Regex.IsMatch(line,stringVar ))
    {
        if(nameFunc != ""){
            Function? funcs = funcList.Find(obj => obj?.Nome == nameFunc);
            funcs?.Add(line);
            line = sr.ReadLine();
            lineCount++;
            continue;
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
        Console.WriteLine($"Erro não foi possível interpretar a linha {lineCount}");
        break;
    }
}



/*
 * todo
 * variáveis de escopo de função
 * lambda funcs
 */