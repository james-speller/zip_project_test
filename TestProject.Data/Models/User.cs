namespace TestProject.Data.Models
{
    public class User
    {
        /// <summary>
        /// Gets or sets the primary key of the User.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the User's Email Address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the User's Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the User's Monthly Salary.
        /// </summary>
        public decimal MonthlySalary { get; set; }

        /// <summary>
        /// Gets or sets the user's Monthly Expenses.
        /// </summary>
        public decimal MonthlyExpenses { get; set; }

        public virtual Account Account { get; set; }
    }
}
