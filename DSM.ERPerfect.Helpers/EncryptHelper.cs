using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace DSM.ERPerfect.Helpers
{
    public static class EncryptHelper
    {
        // Generar una clave secreta derivada de una palabra clave
        public static byte[] GenerateDerivedKey(string password)
        {
            byte[] salt = Encoding.UTF8.GetBytes("EstaEsLaClaveParaTamara");

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32); // Tamaño de la clave derivada (en bytes)
            }
        }

        // Encriptar la contraseña con SHA-256 y clave secreta derivada
        public static string EncryptPassword(string password, byte[] derivedKey)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + derivedKey.Length];

                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(derivedKey, 0, saltedPassword, passwordBytes.Length, derivedKey.Length);

                byte[] hashedPassword = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hashedPassword);
            }
        }

        // Verificar si la contraseña proporcionada coincide con la contraseña almacenada
        public static bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            byte[] derivedKey = GenerateDerivedKey(enteredPassword);
            string hashedEnteredPassword = EncryptPassword(enteredPassword, derivedKey);
            return hashedEnteredPassword == storedPassword;
        }
    }
}