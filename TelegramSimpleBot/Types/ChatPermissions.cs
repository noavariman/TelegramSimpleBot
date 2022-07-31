using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramSimpleBot.Types
{
    public class ChatPermissions
    {
        public bool CanSendMessages { get; set; }
        public bool CanSendMediaMessages { get; set; }
        public bool CanSendPolls { get; set; }
        public bool CanSendotherMessages { get; set; }
        public bool CanAddWebPagePreviews { get; set; }
        public bool CanchangeInfo { get; set; }
        public bool CanInviteUsers { get; set; }
        public bool CanPinMessages { get; set; }
    }
}
