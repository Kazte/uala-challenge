using System.Text;
using ualax.application.Abstractions.Authentication;

namespace ualax.infrastructure.Authentication
{
    public class UsernameHasher : IHasher
    {
        public string Hash(string key)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
        }

        public string Unhash(string hashedKey)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(hashedKey));
        }
    }
}
