using System.Data;
using System.Data.SQLite;
using System.Reflection;

public class ConsoleView : IView
{
    private Presenter _presenter;
    private string _passportTextboxText;

    public ConsoleView(Presenter presenter)
    {
        _presenter = presenter;
    }

    public void SetPasportTextBoxText(string text)
    {
        _passportTextboxText = text;
    }
  
    public void MessageBoxShow(string text)
    {
        Console.WriteLine($"MsgBox: {text}");
    }

    public void TextResultShow(string text)
    {
        Console.WriteLine($"TxtResult: {text}");
    }
  
    private void checkButton_Click(object sender, EventArgs e)
    {
        if (_passportTextboxText.Trim() == "")
        {
            MessageBoxShow("Введите серию и номер паспорта");
        }
        else
        {
            string rawData = _passportTextboxText.Trim().Replace(" ", string.Empty);
            if (rawData.Length < 10)
            {
                TextResultShow("Неверный формат серии или номера паспорта");
            }
            else
            {
                string commandText = string.Format("select * from passports where num='{0}' limit 1;", ComputeSha256Hash(rawData));
                string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");
                try
                {
                    SQLiteConnection connection = new SQLiteConnection(connectionString);
                    connection.Open();
                    SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));
                    DataTable dataTable1 = new DataTable();
                    DataTable dataTable2 = dataTable1;
                    sqLiteDataAdapter.Fill(dataTable2);
                    if (dataTable1.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dataTable1.Rows[0].ItemArray[1]))
                            TextResultShow("По паспорту «" + _passportTextboxText + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН");
                        else
                            TextResultShow("По паспорту «" + _passportTextboxText + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ");
                    }
                    else
                        TextResultShow("Паспорт «" + _passportTextboxText + "» в списке участников дистанционного голосования НЕ НАЙДЕН");
                    connection.Close();
                }
                catch (SQLiteException ex)
                {
                    if (ex.ErrorCode != 1)
                        return;
            
                    MessageBoxShow("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
                }
            }
        }
    }

    private string ComputeSha256Hash(string rawData)
    {
        throw new NotImplementedException();
    }
}