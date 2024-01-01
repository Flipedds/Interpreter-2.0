namespace Interpreter.utils;

public record Function(string Nome)
{
    private List<string> _listOfMembers = [];
    private List<Var?> _funcVars = [];

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