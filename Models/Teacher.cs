namespace SchoolManagementApp.Models
{
    public class Teacher
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; } = DateTime.Now;
        public decimal Salary { get; set; }
        public bool IsActive { get; set; } = true;
    }
}