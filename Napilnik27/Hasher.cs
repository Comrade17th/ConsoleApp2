using System.Security.Cryptography;
using System.Text;

public class Hasher
{
    public string GetHash(string data) => 
        Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(data)));
}