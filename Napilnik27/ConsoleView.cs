using System.Data;
using System.Data.SQLite;

public class ConsoleView : IView
{
    private Presenter _presenter;
    private string _passportTextboxText;

    public ConsoleView(PresenterFactory factory)
    {
        MessageBoxShow("Введите серию и номер паспорта");
        _presenter = factory.Create(this);
    }

    public void SetPasportTextBoxText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException(nameof(text));
        
        _passportTextboxText = text;
        checkButton_Click();
    }
  
    public void MessageBoxShow(string text)
    {
        Console.WriteLine($"MsgBox: {text}");
    }

    public void TextResultShow(string text)
    {
        Console.WriteLine($"TxtResult: {text}");
    }
  
    private void checkButton_Click()
    {
        _presenter.Process(_passportTextboxText);
    }
}