using System.Data;
using System.Data.SQLite;
using System.Reflection;

public class Program
{
  public static void Main()
  {
    string passportTextbox = Console.ReadLine();
  }
}

public class View()
{
  private string passportTextboxText;
  
  private void checkButton_Click(object sender, EventArgs e)
    {
      if (this.passportTextboxText.Trim() == "")
      {
        Console.WriteLine("Введите серию и номер паспорта");
      }
      else
      {
        string rawData = this.passportTextboxText.Trim().Replace(" ", string.Empty);
        if (rawData.Length < 10)
        {
          Console.WriteLine("Неверный формат серии или номера паспорта");
        }
        else
        {
          string commandText = string.Format("select * from passports where num='{0}' limit 1;", (object) ComputeSha256Hash(rawData));
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
                Console.WriteLine("По паспорту «" + this.passportTextboxText + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН");
              else
                Console.WriteLine("По паспорту «" + this.passportTextboxText + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ");
            }
            else
              Console.WriteLine("Паспорт «" + this.passportTextboxText + "» в списке участников дистанционного голосования НЕ НАЙДЕН");
            connection.Close();
          }
          catch (SQLiteException ex)
          {
            if (ex.ErrorCode != 1)
              return;
            int num2 = (int) Console.WriteLine("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
          }
        }
      }
    }

  private object ComputeSha256Hash(string rawData)
  {
    throw new NotImplementedException();
  }
}

