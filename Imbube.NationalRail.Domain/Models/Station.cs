using System;
using Imbube.NationalRail.Domain.Interface;

namespace Imbube.NationalRail.Domain.Models
{
    public class Station : IStation
    {
        public string Name { get; set; }

        public string Address1 { get; set; }

        public string PostCode { get; set; }

        public  long CoordinateX { get; set; }

        public long CoordinateY { get; set; }

        public DateTime OpenTime { get; set; }

        public DateTime CloseTime { get; set; }

        public string Code { get; set; }

    }
}
