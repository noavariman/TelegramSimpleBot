using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramSimpleBot.Types
{
    public interface File
    {
        string FileId { get; set; }
        string FileUniqueId { get; set; }
        int FileSize { get; set; }
        string FilePath { get; set; }
        string Caption { get; set; }
        string Type { get; set; }
        //int Width { get; set; }
        //int Height { get; set; }
        //int Duration { get; set; }
        //bool Is_Animated { get; set; }
        //bool Is_Video { get; set; }
        //string Emoji { get; set; }
        //string Set_Name { get; set; }
    }
}
