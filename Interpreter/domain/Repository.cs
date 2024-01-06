namespace Interpreter.domain;
public class Repository()
{
    private string _nameFunc = "";
    public ref string NameFunc => ref _nameFunc;
    private int _lineFunc = 0;
    public ref int LineFunc => ref _lineFunc;
    private int _lineCount = 1;
    public ref int LineCount => ref _lineCount;
    private List<Function?> _funcList = [];
    public ref List<Function?> FuncList => ref _funcList;
    private List<Var?> _varList = [];
    public ref List<Var?> VarList => ref _varList;
}
