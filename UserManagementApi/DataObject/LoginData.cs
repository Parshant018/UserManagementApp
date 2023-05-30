using System.Runtime.Serialization;

namespace UserManagementApi.DataObject
{
    [DataContract]
    public class LoginData
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public DesignationList Designation { get; set; }
    }
}