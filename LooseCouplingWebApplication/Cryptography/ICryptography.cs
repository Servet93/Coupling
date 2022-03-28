namespace LooseCouplingWebApplication
{
    public interface ICryptography
    {
        string EncryptData(string data, string key);
        string DecryptData(string data, string key);
    }
}