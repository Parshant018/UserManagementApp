using System.Runtime.Serialization;

namespace UserManagementApi.DataObject
{
    [DataContract]
    public class SearchFilterData
    {
        [DataMember]
        public string Name { get; set; } = null;

        [DataMember]
        public string Email { get; set; } = null;

        [DataMember]
        public string DateOfBirth { get; set; }

        [DataMember]
        public DesignationList Designation { get; set; }
        
        [DataMember]
        public int Salary { get; set; } = 0;

        [DataMember]
        public string PhoneNumber { get; set; } = null;
    }
}