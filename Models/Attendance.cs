namespace SchoolManagementApp.Models
{
    public class Attendance
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Present"; // Present, Absent, Late
        public string Remarks { get; set; } = string.Empty;
    }
}