public class Human
{
    private readonly Passport _passport;

    public Human(Passport passport, bool hasRightsToVote)
    {
        ArgumentNullException.ThrowIfNull(passport);
        
        if (string.IsNullOrWhiteSpace(passport.SeriesAndNumber))
            throw new ArgumentException(nameof(passport.SeriesAndNumber));

        _passport = passport;
        HasRightsToVote = hasRightsToVote;
    }

    public string SeriesAndNumber => _passport.SeriesAndNumber;
    public bool HasRightsToVote { get; }
}