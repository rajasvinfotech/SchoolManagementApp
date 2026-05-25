namespace SchoolManagementApp.Models
{
    public class FeeStructure
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Class { get; set; } = string.Empty;
        public string FeeType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Frequency { get; set; } = "Monthly"; // Monthly, Quarterly, Yearly, One-time
        public bool IsActive { get; set; } = true;
        public DateTime EffectiveFrom { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
    }
}