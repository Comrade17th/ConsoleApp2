public class Presenter
{
    private readonly Model _model;
    private readonly IView _view;

    public Presenter(Model model, IView view)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _view = view ?? throw new ArgumentNullException(nameof(view));
    }

    public void Process(string passportTextboxText)
    {
        if (string.IsNullOrWhiteSpace(passportTextboxText))
            throw new ArgumentException(nameof(passportTextboxText));
        
        Passport passport = new Passport(passportTextboxText);
        Human human = _model.FindHumanBy(passport);
        
        ShowResault(human);
    }
    
    private void ShowResault(Human human)
    {
        string acces = human.HasRightsToVote
            ? "НЕ ПРЕДОСТАВЛЯЛСЯ"
            : "ПРЕДОСТАВЛЕН";

        string message = $"По паспорту «{human.SeriesAndNumber}» доступ к бюллетеню на дистанционном электронном голосовании {acces}";

        _view.TextResultShow(message);
    }
}