namespace Interpreter.utils;

public class Patterns
{
    public string Def => @"^def\s+\w+\s*\(\s*((\w+\s*,\s*)*\w*)?\s*\):\s*$";
    public string Print => @"print\(""+[^""]+""\)$";
    public string PrintVar => @"print\(+[^""]+\)$";
    public string PrintNumber => @"print\([0-9]+[\+\/*\-][0-9]+\)$";
    public string ExecDef => @"\w+\s*\(\s*((\w+\s*,\s*)*\w*)?\s*\)$";
    public string Var => @"^[a-zA-Z]+\s\=\s[0-9]+$";

    public string StringVar => @"^[a-zA-Z]+\s\=\s\""[^""]+\""$";
}
