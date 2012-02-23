using Imbube.NationalRail.Domain.Interface;

namespace Imbube.NationalRail.Domain.Models
{
    public class Train : ITrain
    {
        public string StartTime { get; set; }
        public string ExpectedTime { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Spare { get; set; }
        public string UriForCurrentStatus { get; set; }
    }
}
