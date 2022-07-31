using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramSimpleBot.Types
{
    class VideoNote : File
    {
        public string FileId { get; set; }
        public string FilePath { get; set; }
        public int FileSize { get; set; }
        public string FileUniqueId { get; set; }
        public string Caption { get; set; }
        public string Type { get; set; }
        public int Length { get; set; }
        public int Duration { get; set; }
    }
}
