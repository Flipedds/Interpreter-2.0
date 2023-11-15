namespace Interpreter.mappers;

public class Function
{
    private readonly List<string> _listOfMembers;
    public string Nome { get;}
    public Function(string nome)
    {
        Nome = nome;
        _listOfMembers = new List<string>();
    }

    public void Add(string member)
    {
        _listOfMembers.Add(member);
    }
    public List<string> MyList => _listOfMembers;
}