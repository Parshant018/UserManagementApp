using System;

namespace UserServices.Models
{
    public class TokenInfo
    {
        public string Token { get; set; }
        public DateTime TokenExpireTime { get; set; }
    }
}