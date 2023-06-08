namespace DotnetAPI.DTOs
{
    public partial class UserJobInfoToAddDTO
    {
        public int UserId { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }

        public UserJobInfoToAddDTO()
        {
            if (JobTitle == null)
            {
                JobTitle = "";
            }
            if (Department == null)
            {
                Department = "";
            }
        }
    }
}