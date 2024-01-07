using Interpreter.utils;
using Interpreter.domain;
using System.Text.RegularExpressions;

namespace Interpreter.services;

public class ValidationService
{

#pragma warning disable CS8604 // Possível argumento de referência nula.

    public static void PrintValidation(
        Patterns patterns, ref string? line,
        ref int lineCount, ref List<Function?> funcList,
        ref List<Var?> varList, string nameFunc, ref StreamReader sr,
        int lineFunc)
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
            return;
        }
        else if (nameFunc != ""
        && !Regex.IsMatch(line, patterns.DefPrint)
        | !Regex.IsMatch(line, patterns.DefPrintNumber)
        | !Regex.IsMatch(line, patterns.DefPrintVar)
        )
        {
            if (lineFunc + 1 == lineCount)
            { throw new InvalidOperationException($"Erro: não foi possível interpretar a linha {lineCount} esperada uma identação depois da definição de uma função na linha {lineFunc} !"); }
            else
            {
                nameFunc = "";
                lineFunc = 0;
            }
        }
        Mapper map = new();
        map.Print(line, lineCount, ref varList);
        line = sr.ReadLine();
        lineCount++;
    }

    public static void DefValidation(ref string? line,
        ref List<Function?> funcList, ref string nameFunc,
        ref int lineFunc, ref int lineCount, ref StreamReader sr)
    {
        Mapper map = new();
        nameFunc = map.Def(line);
        lineFunc = lineCount;
        Function? func = new(nameFunc);
        funcList.Add(func);
        line = sr.ReadLine();
        lineCount++;
    }

    public static void ExecDefValidation(
        Patterns patterns, ref string? line,
        ref List<Function?> funcList, ref int lineCount, string nameFunc,
        ref StreamReader sr, ref List<Var?> varList, int lineFunc)
    {
        if (nameFunc != ""
            && Regex.IsMatch(line, patterns.DefExecDef))
        {
            Function? funcs = funcList.Find(obj => obj?.Nome == nameFunc);
            funcs?.Add(line);
            line = sr.ReadLine();
            lineCount++;
            return;
        }
        else if (nameFunc != ""
        && !Regex.IsMatch(line, patterns.DefExecDef))
        {
            if (lineFunc + 1 == lineCount)
            { throw new InvalidOperationException($"Erro: não foi possível interpretar a linha {lineCount} esperada uma identação depois da definição de uma função na linha {lineFunc} !"); }
            else
            {
                nameFunc = "";
                lineFunc = 0;
            }
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
            throw new InvalidOperationException($"Erro: não foi possível interpretar a linha {lineCount}. Função {name[0]} não foi encontrada!");
        }
    }

    public static void IsNullOrWhiteSpaceValidation(
        ref string? line, ref int lineCount, ref StreamReader sr
    )
    {
        line = sr.ReadLine();
        lineCount++;
    }

    public static void VarValidation(
        string nameFunc, Patterns patterns, ref string? line,
        ref List<Function?> funcList, ref int lineCount,
        ref List<Var?> varList, ref StreamReader sr, int lineFunc)
    {
        if (nameFunc != ""
            && Regex.IsMatch(line, patterns.DefVar)
            | Regex.IsMatch(line, patterns.DefStringVar))
        {
            Function? funcs = funcList.Find(obj => obj?.Nome == nameFunc);
            funcs?.Add(line.Trim());
            line = sr.ReadLine();
            lineCount++;
            return;
        }
        else if (nameFunc != ""
        && !Regex.IsMatch(line, patterns.DefVar)
        | !Regex.IsMatch(line, patterns.DefStringVar))
        {
            if (lineFunc + 1 == lineCount)
            { throw new InvalidOperationException($"Erro: não foi possível interpretar a linha {lineCount} esperada uma identação depois da definição de uma função na linha {lineFunc} !"); }
            else
            {
                nameFunc = "";
                lineFunc = 0;
            }
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
            return;
        }

        Var newVariable = new(name, value);
        varList.Add(newVariable);
        line = sr.ReadLine();
        lineCount++;
    }
}
