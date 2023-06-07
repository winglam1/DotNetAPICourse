using System.Text.Json.Serialization;

namespace HelloWorld.Models
{
    // chapter to show class
    public class Computer
    {
        // Field
        //private string _motherboard;
        [JsonPropertyName("computer_id")]
        public int ComputerId { get; set; }
        // Properties
        [JsonPropertyName("cpu_cores")]
        public int? CPUCores { get; set; }
        [JsonPropertyName("has_wifi")]
        public bool HasWifi { get; set; }
        [JsonPropertyName("has_lte")]
        public bool HasLTE { get; set; }
        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("motherboard")]
        public string Motherboard { get; set; } = "";
        //public string Motherboard { get { return _motherboard; } set { _motherboard = value;} }
        [JsonPropertyName("video_card")]
        public string VideoCard { get; set; } = ""; // same as above of getting and setting the field value.

        public Computer() 
        {
            if (VideoCard == null)
            {
                VideoCard = "";
            }

            if (Motherboard == null)
            {
                Motherboard = "";
            }

            if (CPUCores == null)
            {
                CPUCores = 0;
            }
        }
    }
}