using System.Text.RegularExpressions;
using Interpreter.utils;
using Interpreter.domain;
using Interpreter.services;

StreamReader sr = new(args[0]);
string? line = sr.ReadLine();
Repository repo = new();
Patterns patterns = new();

while (!sr.EndOfStream)
{
    if (line != null
    && Regex.IsMatch(line, patterns.Print)
    | Regex.IsMatch(line, patterns.PrintNumber)
    | Regex.IsMatch(line, patterns.PrintVar))
    {
        ValidationService.PrintValidation(
            patterns, ref line, ref repo.LineCount, 
            ref repo.FuncList, ref repo.VarList, repo.NameFunc, ref sr,
            repo.LineFunc);
    }
    else if (line != null
    && Regex.IsMatch(line, patterns.Def))
    {
        ValidationService.DefValidation(
            ref line, ref repo.FuncList, ref repo.NameFunc, 
            ref repo.LineFunc, ref repo.LineCount, ref sr);
    }
    else if (line != null
    && Regex.IsMatch(line, patterns.ExecDef))
    {
        ValidationService.ExecDefValidation(
            patterns, ref line, ref repo.FuncList, 
            ref repo.LineCount, repo.NameFunc, ref sr, 
            ref repo.VarList, repo.LineFunc);
    }
    else if (line == "" | string.IsNullOrWhiteSpace(line))
    {
        ValidationService.IsNullOrWhiteSpaceValidation(
            ref line, ref repo.NameFunc, 
            ref repo.LineFunc, ref repo.LineCount, ref sr);
    }
    else if (line != null
    && line.Contains('='))
    {
        ValidationService.VarValidation(
            repo.NameFunc, patterns, ref line, 
            ref repo.FuncList, ref repo.LineCount, ref repo.VarList, ref sr,
            repo.LineFunc);
    }
    else
    {
        Console.WriteLine($"Erro: não foi possível interpretar a linha {repo.LineCount}");
        break;
    }
}

#pragma warning disable CS8604 // Possível argumento de referência nula.

/*
 * todo
 * lambda funcs
 */