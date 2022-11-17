using BenchmarkDotNet.Attributes;
using System;
using System.Security.Cryptography;
using System.Text;

namespace password_array_resize_copy
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        [Benchmark]
        public string UseResize()
        {
            string passwordText = "password";
            byte[] salt = Encoding.UTF8.GetBytes("saltsaltsaltsalt");
            var iterate = 10000;
            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
            var hash = pbkdf2.GetBytes(20);
            Array.Resize(ref salt, 36);
            Array.Copy(hash, 0, salt, 16, 20);
            var passwordHash = Convert.ToBase64String(salt);
            return passwordHash;
        }

        [Benchmark]
        public string UseCopy()
        {
            string passwordText = "password";
            byte[] salt = Encoding.UTF8.GetBytes("saltsaltsaltsalt");
            var iterate = 10000;
            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            var passwordHash = Convert.ToBase64String(hashBytes);
            return passwordHash;
        }

        [Benchmark]
        public string UseLoop()
        {
            string passwordText = "password";
            byte[] hashBytes = new byte[36];
            byte[] salt = Encoding.UTF8.GetBytes("saltsaltsaltsalt");
            for (int i = 0; i < salt.Length; i++)
            {
                hashBytes[i] = salt[i];
            }

            var iterate = 10000;
            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < hash.Length; i++)
            {
                hashBytes[i + 16] = hash[i];
            }

            var passwordHash = Convert.ToBase64String(hashBytes);
            return passwordHash;
        }

        [Benchmark]
        public string UseSpan()
        {
            Span<byte> span = stackalloc byte[36];
            Encoding.UTF8.GetBytes("saltsaltsaltsalt", span);
            ReadOnlySpan<char> passwordText = "password".AsSpan();

            var iterate = 10000;
            Rfc2898DeriveBytes.Pbkdf2(passwordText, span[..16], span.Slice(16, 20), iterate, HashAlgorithmName.SHA1);
            var passwordHash = Convert.ToBase64String(span);
            return passwordHash;
        }
    }
}
