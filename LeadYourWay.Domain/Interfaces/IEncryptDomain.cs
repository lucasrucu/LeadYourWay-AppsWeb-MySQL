namespace LeadYourWay.Domain;

public interface IEncryptDomain
{
    public string Encrypt(string value);
    public string Decrypt(string value);
}