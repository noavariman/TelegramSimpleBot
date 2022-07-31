using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramSimpleBot.Types
{
    public class Location
    {
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
    }
}
