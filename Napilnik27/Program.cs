public class Program
{
  public static void Main()
  {
    Model model = new Model();
    Presenter presenter = new Presenter();
    IView view = new ConsoleView();
  }
}

public class PresenterFactory : IPresenterFactory
{
  private readonly Model _model;
  
  public PresenterFactory(Model model)
  {
    _model = model;
  }

  public Presenter Create(IView view)
  {
    return new Presenter(_model, view);
  }
}

public interface IPresenterFactory
{
  public Presenter Create(IView view);
}