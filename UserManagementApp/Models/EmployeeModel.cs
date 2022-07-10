namespace UserManager.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public string Designation { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public string Bio { get; set; }
    }
}