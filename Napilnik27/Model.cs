using System.Data;

public class Model
{
    private DataBase _dataBase;
    private Hasher _hasher;

    public Model(DataBase dataBase, Hasher hasher)
    {
        _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        _dataBase = dataBase ?? throw new ArgumentNullException(nameof(dataBase));
    }

    public Human FindHumanBy(Passport passport)
    {
        if (passport == null) throw new ArgumentNullException(nameof(passport));

        if (string.IsNullOrWhiteSpace(passport.SeriesAndNumber))
            throw new ArgumentException(nameof(passport.SeriesAndNumber));
        
        string hash = _hasher.GetHash(passport.SeriesAndNumber);
        
        int firstRowIndex = 0;
        int voteBoolIndex = 1;

        string passportHash = _hasher.GetHash(passport.SeriesAndNumber);
        DataTable citizenData = _dataBase.GetDataTable(passportHash);

        if (citizenData.Rows.Count == 0)
            throw new InvalidOperationException($"Паспорт «{passport.SeriesAndNumber}» в списке участников дистанционного голосования НЕ НАЙДЕН");

        bool canVote = Convert.ToBoolean(citizenData.Rows[firstRowIndex].ItemArray[voteBoolIndex]);

        return new Human(passport, canVote);
    }
  
}