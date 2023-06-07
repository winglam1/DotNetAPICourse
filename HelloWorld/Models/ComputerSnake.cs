namespace HelloWorld.Models
{
    // chapter to show class
    public class ComputerSnake
    {
        // Field
        //private string _motherboard;
        public int computer_id { get; set; }
        // Properties
        public int? cpu_cores { get; set; }
        public bool has_wifi { get; set; }
        public bool has_lte { get; set; }
        public DateTime? release_date { get; set; }
        public decimal price { get; set; }
        public string motherboard { get; set; } = "";
        //public string Motherboard { get { return _motherboard; } set { _motherboard = value;} }
        public string video_card { get; set; } = ""; // same as above of getting and setting the field value.

        public ComputerSnake() 
        {
            if (video_card == null)
            {
                video_card = "";
            }

            if (motherboard == null)
            {
                motherboard = "";
            }

            if (cpu_cores == null)
            {
                cpu_cores = 0;
            }
        }
    }
}