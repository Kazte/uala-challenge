using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ualax.application.Abstractions.Authentication
{
    public interface IHasher
    {
        string Hash(string key);
        string Unhash(string hashedKey);
    }
}
