public class Program
{
  public static void Main()
  {
    string exitCommand = "exit";

    Hasher hasher = new Hasher();
    DataBase dataBase = new DataBase();
    
    Model model = new Model(dataBase, hasher);
    PresenterFactory factory = new PresenterFactory(model);
    ConsoleView view = new ConsoleView(factory);

    string input;
    
    while ((input = Console.ReadLine()) != exitCommand)
    {
      view.SetPasportTextBoxText(input);  
    }
  }
}