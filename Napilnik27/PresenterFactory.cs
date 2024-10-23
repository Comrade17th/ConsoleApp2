public class PresenterFactory : IPresenterFactory
{
    private readonly Model _model;
  
    public PresenterFactory(Model model) => 
        _model = model ?? throw new ArgumentNullException(nameof(model));

    public Presenter Create(IView view) => 
        new(_model, view);
}