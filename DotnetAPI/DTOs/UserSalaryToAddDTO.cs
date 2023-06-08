namespace DotnetAPI.DTOs
{
    public partial class UserSalaryToAddDTO
    {

        public int UserId { get; set; }
        public decimal Salary { get; set; }
        public decimal AvgSalary { get; set; }

        public UserSalaryToAddDTO()
        {
        }
    }
}