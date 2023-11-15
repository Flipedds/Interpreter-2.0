namespace Interpreter.mappers;

public class Var
{
    public string? Nome {get; set;}
    public dynamic Value { get; set; }

    public Var(string nome, dynamic value)
    {
        Nome = nome;
        Value = value;
    }
}