using System.Runtime.Serialization;

namespace UserManagementApi.DataObject
{
    public enum DesignationList{NULL,Admin,User};

    [DataContract]
    public class UserData
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string DateOfBirth { get; set; }

        [DataMember]
        public DesignationList Designation { get; set; }

        [DataMember]
        public int Salary { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string Bio { get; set; }

        [DataMember]
        public string CreatedOn { get; set; }

        [DataMember]
        public string ModifiedOn { get; set; }
    }
}