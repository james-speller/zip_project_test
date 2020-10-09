namespace TestProject.Data.Models
{
    public class Account
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal MaximumMonthlyCreditLimit { get; set; }

        public virtual User User { get; set; }
    }
}
