namespace UserServices
{
    public class SearchFilterModel
    {
        public string Name { get; set; } = null;
        public string Email { get; set; } = null;
        public string DateOfBirth { get; set; }
        public string Designation { get; set; } = null;
        public int Age { get; set; } = 0;
        public int Salary { get; set; } = 0;
        public string PhoneNumber { get; set; } = null;
    }
}