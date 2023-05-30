namespace UserManagementApp.Models
{
    public enum DesignationList { NULL,Admin,User};
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public DesignationList Designation { get; set; } 
        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public string Bio { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
    }
}