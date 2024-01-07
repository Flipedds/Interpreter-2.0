using System.Text.RegularExpressions;
using Interpreter.utils;
using Interpreter.domain;
using Interpreter.services;

StreamReader sr = new(args[0]);
string? line = sr.ReadLine();
Repository repo = new();
Patterns patterns = new();
JsonArray array = new();
while (!sr.EndOfStream)
{
    switch (line)
    {
        case string s when s != null &&
                         (Regex.IsMatch(s, patterns.Print) ||
                         Regex.IsMatch(s, patterns.PrintNumber) ||
                         Regex.IsMatch(s, patterns.PrintVar)):
            ValidationService.PrintValidation(
                patterns, ref line, ref repo.LineCount,
                ref repo.FuncList, ref repo.VarList, repo.NameFunc, ref sr,
                repo.LineFunc, ref array);
            break;

        case string s when s != null &&
                        Regex.IsMatch(s, patterns.Def):
            ValidationService.DefValidation(
            ref line, ref repo.FuncList, ref repo.NameFunc,
            ref repo.LineFunc, ref repo.LineCount, ref sr, ref array);
            break;

        case string s when s != null &&
                        Regex.IsMatch(s, patterns.ExecDef):
            ValidationService.ExecDefValidation(
            patterns, ref line, ref repo.FuncList,
            ref repo.LineCount, repo.NameFunc, ref sr,
            ref repo.VarList, repo.LineFunc, ref array);
            break;

        case string s when s == "" ||
                        string.IsNullOrWhiteSpace(s):
            ValidationService.IsNullOrWhiteSpaceValidation(
            ref line, ref repo.LineCount, ref sr);
            break;

        case string s when s != null && 
                        line.Contains('='):
            ValidationService.VarValidation(
            repo.NameFunc, patterns, ref line,
            ref repo.FuncList, ref repo.LineCount, ref repo.VarList, ref sr,
            repo.LineFunc, ref array);
            break;

        default:
            throw new InvalidOperationException($"Erro: não foi possível interpretar a linha {repo.LineCount}");
    }
}
string jsonString = array.ArrayJson.ToString();
string caminhoDoArquivo = "parsed.json";
File.WriteAllText(caminhoDoArquivo, jsonString);

/*
 * todo
 * lambda funcs
 */