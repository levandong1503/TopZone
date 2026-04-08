using System.Security.Cryptography;

namespace Application;

public class PasswordHashService : IPasswordHashService
{
    private const int SaltSize = 16; // 128 bit
    private const int HashSize = 32; // 256 bit
    private const int Iterations = 10000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, HashSize);
        return $"{Convert.ToHexString(salt)}-{Convert.ToHexString(hash)}";
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        string[] parts = hashedPassword.Split('-');
        byte[] salt = Convert.FromHexString(parts[0]);
        byte[] hash = Convert.FromHexString(parts[1]);
        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, HashSize);
        return hash.SequenceEqual(inputHash);
    }
}
