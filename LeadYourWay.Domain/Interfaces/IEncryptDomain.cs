namespace LeadYourWay.Domain;

public interface IEncryptDomain
{
    public string Encrypt(string password);
    public string Decrypt(string password);
}