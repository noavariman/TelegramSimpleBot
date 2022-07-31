using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramSimpleBot.Types
{
    class Sticker : File
    {
        public string FileId { get; set; }
        public string FilePath { get; set; }
        public int FileSize { get; set; }
        public string FileUniqueId { get; set; }
        public string Caption { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Duration { get; set; }
        public bool IsAnimated { get; set; }
        public bool IsVideo { get; set; }
        public string Emoji { get; set; }
        public string SetName { get; set; }
    }
}
