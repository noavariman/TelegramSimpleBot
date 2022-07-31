using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramSimpleBot.Types
{
    public class UserProfilePhotos
    {
        public int TotalCount { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
