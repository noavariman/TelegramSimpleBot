using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramSimpleBot.Types
{
    public class AdministorPermissions
    {
        public bool IsAnonymous { get; set; }
        public bool CanManageChat { get; set; }
        public bool CanPostMessages { get; set; }
        public bool CanEditMessages { get; set; }
        public bool CanDeleteMessages { get; set; }
        public bool CanmanageVideoChats { get; set; }
        public bool CanRestrictMembers { get; set; }
        public bool CanPromoteMembers { get; set; }
        public bool CanChangeInfo { get; set; }
        public bool CanInviteUsers { get; set; }
        public bool CanPinMessages { get; set; }
    }
}
