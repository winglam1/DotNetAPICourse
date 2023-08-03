namespace DotnetAPI.DTOs
{
    public partial class PostToEditDto
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }

        public PostToEditDto()
        {
            if (PostTitle == null)
            {
                PostTitle = string.Empty;
            }

            if (PostContent == null)
            {
                PostContent = string.Empty;
            }
        }
    }
}