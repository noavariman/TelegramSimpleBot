using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramSimpleBot.Types
{
    public class Poll
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public PollOption[] Options { get; set; }
        public int TotalVotes { get; set; }
        public bool IsAnonymous { get; set; }
    }

    public class PollOption
    {
        public string Text { get; set; }
        public int VoteCount { get; set; }
    }
}
