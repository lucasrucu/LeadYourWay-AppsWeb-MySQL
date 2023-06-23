using System.Text;

namespace LeadYourWay.Domain;

public class EncryptDomain : IEncryptDomain
{
    public string Encrypt(string value)
    {
        try
        {
            var encodeDataByte = new byte[value.Length];
            encodeDataByte = Encoding.UTF8.GetBytes(value);
            var returnValue = Convert.ToBase64String(encodeDataByte);
            return returnValue;
        }
        catch (Exception e)
        {
            throw new Exception("Error in base64Encode" + e.Message);
        }
    }

    public string Decrypt(string value)
    {
        try
        {
            var encoder = new UTF8Encoding();
            var utf8Decode = encoder.GetDecoder();
            var todecodeByte = Convert.FromBase64String(value);
            var charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
            var decodedChar = new char[charCount];
            utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
            var result = new string(decodedChar);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception("Error inbase64Decode" + e.Message);
        }
    }
}