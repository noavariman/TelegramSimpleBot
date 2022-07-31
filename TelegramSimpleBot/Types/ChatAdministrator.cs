using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramSimpleBot.Types
{
    public class ChatAdministrator
    {
        public User User { get; set; }
        public Permissions Permissions { get; set; }
    }

    public class Permissions
    {
        public string Status { get; set; }
        public bool CanBeEdited { get; set; }
        public bool CanManageChat { get; set; }
        public bool CanChangeInfo { get; set; }
        public bool CanDeleteMessages { get; set; }
        public bool CanInviteUsers { get; set; }
        public bool CanRestrictMembers { get; set; }
        public bool CanPinMessages { get; set; }
        public bool CanPromoteMembers { get; set; }
        public bool CanManageVideoChats { get; set; }
        public bool CanManageVoiceChats { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
