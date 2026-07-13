using System.Security.Cryptography;
using System.Text;
namespace IdentityService.Infrastructure.Utilities;

public static class UtitlityHeandler
{
    public static string CreateGuidFromString(string input)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        
        byte[] guidBytes = new byte[16];
        Array.Copy(hash, guidBytes, 16);
    
        return new Guid(guidBytes).ToString();
    }
}
