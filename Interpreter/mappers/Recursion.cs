namespace Interpreter.mappers;

public class Recursion
{
    public void DefRecursion(Function func, List<Function?> list, int lineCount, ref List<Var?> varList)
    {
        List<string> iterList = func.MyList;
            foreach (var item in iterList)
            {   
                if (item.Contains("print")){
                    Mapper map = new();
                    map.Print(item, lineCount, ref varList);
                }
                else if (item.Contains("()")){
                    var itemName = item.Split("(");
                    var nome = itemName[0].Trim();
                    Function? funcs = list.Find(obj => obj?.Nome == nome);

                    if (funcs == null){
                        Console.WriteLine($"Erro não foi possível interpretar a linha {lineCount}. Função {nome} não foi encontrada!!");
                        Environment.Exit(0);
                    }
                    DefRecursion(funcs, list, lineCount, ref varList);
                } 
            }
    }
}