using System.Runtime.Serialization;

namespace UserServices
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string DateOfBirth { get; set; }

        [DataMember]
        public string Designation { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public int Salary { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string Bio { get; set; }
    }
}