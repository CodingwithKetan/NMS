using System.Security.Cryptography;
using System.Text;

namespace LiteNMS.Utils.Security;

public static class EncryptionHelper
{
    public static string Encrypt(string clearText, string encryptionKey)
    {
        byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(encryptionKey);
        aes.IV = new byte[16]; // Default IV

        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(clearBytes, 0, clearBytes.Length);
        cs.Close();
        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string cipherText, string encryptionKey)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(encryptionKey);
        aes.IV = new byte[16];

        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(cipherBytes, 0, cipherBytes.Length);
        cs.Close();
        return Encoding.UTF8.GetString(ms.ToArray());
    }
}