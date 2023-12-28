using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Classes.JWT
{
    public class UserInfoWithToken
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public int KullaniciId { get; set; }
    }
}
