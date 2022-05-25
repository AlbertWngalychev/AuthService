using CryptSharp.Utility;
using System.Security.Cryptography;
using System.Text;

namespace AuthService
{
    public static class PasswordHash
    {
        /// <summary>
        /// Генерирует с криптографически надежной случайной последовательностью значений
        /// </summary>
        /// <param name="size">Размер байт</param>
        /// <returns>Массив случайных байт</returns>
        public static byte[] GenerateSalt(int size = 1024) => RandomNumberGenerator.GetBytes(size);

        /// <summary>
        /// Создаёт хешированный пароль с использование алгоритма SCrypt
        /// </summary>
        /// <param name="pwd">пароль</param>
        /// <param name="salt">соль</param>
        /// <returns>Хешированный пароль длинной 255 байт</returns>
        public static byte[] Create(byte[] pwd, byte[] salt) => SCrypt.ComputeDerivedKey(pwd, salt, 16384, 8, 1, null, 255);
        /// <summary>
        /// Сравнение паролей
        /// </summary>
        /// <param name="pwd">Не зашифрованный пароль</param>
        /// <param name="salt">Соль от хешированного пароля</param>
        /// <param name="pwdHash">Хешированный пароль</param>
        /// <returns>true or false</returns>
        public static bool Compare(byte[] pwd, byte[] salt, byte[] pwdHash) => Create(pwd, salt).SequenceEqual(pwdHash);
        /// <summary>
        /// Сравнение паролей
        /// </summary>
        /// <param name="str">Не зашифрованный пароль</param>
        /// <param name="salt">Соль от хешированного пароля</param>
        /// <param name="pwdHash">Хешированный пароль</param>
        /// <returns>true or false</returns>
        public static bool ComparePass(this string str, byte[] salt, byte[] pwdHash) => Compare(Encoding.Default.GetBytes(str), salt, pwdHash);

        public static byte[] CreatePass(this string pwd, byte[] salt)
        {
            return SCrypt.ComputeDerivedKey(Encoding.Default.GetBytes(pwd), salt, 16384, 8, 1, null, 255);
        }


    }
}
