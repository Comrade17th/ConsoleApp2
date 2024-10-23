public class Passport
{
    public Passport(string pasportInput)
    {
        int correctLenght = 10;
          
        if (string.IsNullOrWhiteSpace(pasportInput))
            throw new ArgumentNullException(nameof(pasportInput));

        string seriesAndNumber = pasportInput.Trim().Replace(" ", string.Empty);

        if (seriesAndNumber.Length < correctLenght)
            throw new ArgumentOutOfRangeException(nameof(seriesAndNumber));

        SeriesAndNumber = seriesAndNumber;
    }
        
    public string SeriesAndNumber { get; }
}