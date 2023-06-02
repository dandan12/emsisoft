using System.Security.Cryptography;
using System.Text;

namespace emsisoft.test.core.Application.Hash.Services
{
    public interface IHashService
    {
        IEnumerable<string> GenerateRandomSHA1Hash(int count = 1);
    }

    public class HashService : IHashService
    {
        public IEnumerable<string> GenerateRandomSHA1Hash(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                var data = RandomNumberGenerator.GetBytes(32);
                using SHA1 sha1 = SHA1.Create();
                var hash = sha1.ComputeHash(data);
                var sb = new StringBuilder();
                foreach (var h in hash)
                    sb.Append(h.ToString("2x"));

                yield return sb.ToString();
            }
        }
    }
}
