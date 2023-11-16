namespace Interpreter.mappers;

public class Function(string nome)
{
    private List<string> _listOfMembers = new();
    private List<Var?> _funcVars = new();
    public string Nome { get;} = nome;

    public void NewVar(Var var)
    {
        _funcVars.Add(var);
    }

    public void Add(string member)
    {
        _listOfMembers.Add(member);
    }
    public List<string> MyList => _listOfMembers;
    public List<Var?> MyVars => _funcVars;
}