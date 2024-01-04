using System.Text.RegularExpressions;
using Interpreter.utils;
using Interpreter.domain;
using Interpreter.services;

StreamReader sr = new(args[0]);

string? line = sr.ReadLine();
string nameFunc = "";
int lineFunc = 0;
int lineCount = 1;
List<Function?> funcList = [];
List<Var?> varList = [];
Patterns patterns = new();
while (!sr.EndOfStream)
{
    if (line != null
    && Regex.IsMatch(line, patterns.Print)
    | Regex.IsMatch(line, patterns.PrintNumber)
    | Regex.IsMatch(line, patterns.PrintVar))
    {
        ValidationService.PrintValidation(
            patterns, ref line, ref lineCount, 
            ref funcList, ref varList, nameFunc, ref sr,
            lineFunc);
    }
    else if (line != null
    && Regex.IsMatch(line, patterns.Def))
    {
        ValidationService.DefValidation(
            ref line, ref funcList, ref nameFunc, 
            ref lineFunc, ref lineCount, ref sr);
    }
    else if (line != null
    && Regex.IsMatch(line, patterns.ExecDef))
    {
        ValidationService.ExecDefValidation(
            patterns, ref line, ref funcList, 
            ref lineCount, nameFunc, ref sr, ref varList, lineFunc);
    }
    else if (line == "" | string.IsNullOrWhiteSpace(line))
    {
        ValidationService.IsNullOrWhiteSpaceValidation(
            ref line, ref nameFunc, 
            ref lineFunc, ref lineCount, ref sr);
    }
    else if (line != null
    && line.Contains('='))
    {
        ValidationService.VarValidation(
            nameFunc, patterns, ref line, 
            ref funcList, ref lineCount, ref varList, ref sr,
            lineFunc);
    }
    else
    {
        Console.WriteLine($"Erro: não foi possível interpretar a linha {lineCount}");
        break;
    }
}

#pragma warning disable CS8604 // Possível argumento de referência nula.

/*
 * todo
 * lambda funcs
 */