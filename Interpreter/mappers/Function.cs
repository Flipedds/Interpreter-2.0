namespace Interpreter.mappers;

public class Function(string nome)
{
    private List<string> _listOfMembers = new();
    public string Nome { get;} = nome;


    public void Add(string member)
    {
        _listOfMembers.Add(member);
    }
    public List<string> MyList => _listOfMembers;
}