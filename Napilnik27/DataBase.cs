using System.Data;
using System.Data.SQLite;
using System.Reflection;

public class DataBase
{
    private readonly string fileName = "db.sqlite";
    private SQLiteConnection _connection;

    public DataTable GetDataTable(string hash)
    {
        string commandText = string.Format("select * from passports where num='{0}' limit 1;", hash);
                
        try
        {
            string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"\\{fileName}");
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            
            connection.Open();
            SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));
            DataTable dataTable = new DataTable();
            sqLiteDataAdapter.Fill(dataTable);
            connection.Close();
            
            return dataTable;
        }
        catch (SQLiteException ex)
        {
            int fileNotFounErrorCode = 1;
            
            if (ex.ErrorCode != fileNotFounErrorCode)
                throw;
            
            throw new FileNotFoundException($"Файл {fileName} не найден. Положите файл в папку вместе с exe.");
        }
    }
}