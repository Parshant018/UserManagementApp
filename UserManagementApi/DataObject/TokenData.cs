using System;
using System.Runtime.Serialization;

namespace UserManagementApi.DataObjects
{
    [DataContract]
    public class TokenData
    {
        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public DateTime TokenExpireTime { get; set; }
    }
}