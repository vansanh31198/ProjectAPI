using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Auth.Abstrations
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(string userID, string userName);
    }
}
