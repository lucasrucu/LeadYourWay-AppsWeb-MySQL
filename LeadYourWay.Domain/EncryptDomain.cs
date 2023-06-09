using System.Text;

namespace LeadYourWay.Domain;

public class EncryptDomain : IEncryptDomain
{
    public string Encrypt(string value)
    {
        try
        {
            byte[] encodeDataByte = new byte[value.Length];
            encodeDataByte = Encoding.UTF8.GetBytes(value);
            string returnValue = Convert.ToBase64String(encodeDataByte);
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
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecodeByte = Convert.FromBase64String(value);
            int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
            char[] decodedChar = new char[charCount];
            utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
            string result = new string(decodedChar);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception("Error inbase64Decode" + e.Message);
        }
    }
}