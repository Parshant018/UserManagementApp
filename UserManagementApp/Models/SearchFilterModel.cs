namespace UserManagementApp.Models
{
    public class SearchFilterModel
    {
        public string Name { get; set; } = null;
        public string Email { get; set; } = null;
        public string DateOfBirth { get; set; }
        public DesignationList Designation { get; set; }
        public int Salary { get; set; } = 0;
        public string PhoneNumber { get; set; } = null;
    }
}