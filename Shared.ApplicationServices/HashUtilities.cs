using System;
using System.Security.Cryptography;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices
{
    public static class HashUtilities
    {
        public static string ComputeMd5Hash(this string s)
        {
            using var md5 = MD5.Create();
            var bytes = Encoding.ASCII.GetBytes(s);
            var hash = md5.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public static string ComputeSha256Hash(this string s)
        {
            using var sha256 = new SHA256Managed();
            var bytes = Encoding.UTF8.GetBytes(s);
            var hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}
