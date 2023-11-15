namespace Interpreter.mappers;

public class Var(string nome, dynamic value)
{
    public string? Nome {get; set;} = nome;
    public dynamic Value { get; set; } = value;
}