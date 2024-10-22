public class Presenter
{
    private readonly Model _model;
    private readonly IView _view;

    public Presenter(Model model, IView view)
    {
        _model = model;
        _view = view;
    }
}