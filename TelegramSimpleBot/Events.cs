using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramSimpleBot.Types;

namespace TelegramSimpleBot
{
    public class TelegramBotEvents
    {
        public TelegramBotEvents() { }

        public event EventHandler<MessageReceiveEvent> onMessageReceive;
        public event EventHandler<FileReceiveEvent> onFileReceive;
        public event EventHandler<MemberChatLeaveEvent> onMemberChatLeave;
        public event EventHandler<MemberChatJoinEvent> onMemberChatJoin;
        public event EventHandler<PollAnswareEvent> onPollAnsware;
        public event EventHandler<MessageEditedEvent> onMessageEdited;
        public event EventHandler<LocationReceiveEvent> onLocationReceve;
        public event EventHandler<ErrorReceiveEvent> onErrorReceive;
        internal void MessageReceive(object sender, MessageReceiveEvent e)
        {
            onMessageReceive.Invoke(sender, e);
        }
        internal void FileReceive(object sender, FileReceiveEvent e)
        {
            onFileReceive.Invoke(sender, e);
        }
        internal void MemberChatLeave(object sender, MemberChatLeaveEvent e)
        {
            onMemberChatLeave.Invoke(sender, e);
        }
        internal void MemberChatJoin(object sender, MemberChatJoinEvent e)
        {
            onMemberChatJoin.Invoke(sender, e);
        }
        internal void PollAnsware(object sender, PollAnswareEvent e)
        {
            onPollAnsware.Invoke(sender, e);
        }
        internal void MessageEdited(object sender, MessageEditedEvent e)
        {
            onMessageEdited.Invoke(sender, e);
        }
        internal void LocationReceive(object sender, LocationReceiveEvent e)
        {
            onLocationReceve.Invoke(sender, e);
        }
        internal void ErrorReceive(object sender, ErrorReceiveEvent e)
        {
            onErrorReceive.Invoke(sender, e);
        }
    }

    public class MessageReceiveEvent : EventArgs
    {
        internal MessageReceiveEvent(Message message, User from , bool inGroup, Group group)
        {
            Message = message;
            From = from;
            InGroup = inGroup;
            Group = group;
        }
        public Message Message { get; }
        public User From { get; }
        public bool InGroup { get; }
        public Group Group { get; }
    public class OnMessageReceive : EventArgs
    {
        internal OnMessageReceive(Message message, User from , bool inGroup, Group group)
        {
            Message = message;
            From = from;
            InGroup = inGroup;
            Group = group;
        }
        public Message Message { get; }
        public User From { get; }
        public bool InGroup { get; }
        public Group Group { get; }
    }
    }

    public class FileReceiveEvent : EventArgs
    {
        internal FileReceiveEvent(Message message, User from, File file, bool inGroup, Group group)
        {
            Message = message;
            From = from;
            File = file;
            InGroup = inGroup;
            Group = group;
        }
        public Message Message { get; }
        public User From { get; }
        public File File { get; }
        public bool InGroup { get; }
        public Group Group { get; }
        public class OnMessageReceive : EventArgs
        {
            internal OnMessageReceive(Message message, User from, bool inGroup, Group group)
            {
                Message = message;
                From = from;
                InGroup = inGroup;
                Group = group;
            }
            public Message Message { get; }
            public User From { get; }
            public bool InGroup { get; }
            public Group Group { get; }
        }
    }

    public class MemberChatLeaveEvent : EventArgs
    {
        internal MemberChatLeaveEvent(User user, Group group, User by, bool isAdmin, DateTime date)
        {
            User = user;
            Group = group;
            By = by;
            IsAdmin = isAdmin;
            Date = date;
        }
        public User User { get; }
        public Group Group { get; }
        public User By { get; }
        public bool IsAdmin { get; }
        public DateTime Date { get; set; }
    }

    public class MemberChatJoinEvent : EventArgs
    {
        internal MemberChatJoinEvent(User user, Group group, User by, bool isAdmin, DateTime date)
        {
            User = user;
            Group = group;
            By = by;
            IsAdmin = isAdmin;
            Date = date;
        }
        public User User { get; }
        public Group Group { get; }
        public User By { get; }
        public bool IsAdmin { get; }
        public DateTime Date { get; set; }
    }

    public class PollAnswareEvent : EventArgs
    {
        internal PollAnswareEvent(Poll poll)
        {
            Poll = poll;
        }
        public Poll Poll { get; }
    }

    public class MessageEditedEvent : EventArgs
    {
        internal MessageEditedEvent(Message message, bool inGroup, Group group)
        {
            Message = message;
            InGroup = inGroup;
            Group = group;
        }
        public Message Message { get; }
        public bool InGroup { get; }
        public Group Group { get; }
    }

    public class LocationReceiveEvent : EventArgs
    {
        internal LocationReceiveEvent(Message message, User from, Location location, bool inGroup, Group group)
        {
            Message = message;
            From = from;
            Location = location;
            InGroup = inGroup;
            Group = group;
        }
        public Message Message { get; }
        public User From { get; }
        public Location Location { get; }
        public bool InGroup { get; }
        public Group Group { get; }
    }

    public class ErrorReceiveEvent : EventArgs
    {
        internal ErrorReceiveEvent(string error, bool inGroup, Group group)
        {
            Error = error;
            InGroup = inGroup;
            Group = group;
        }
        public string Error { get; }
        public bool InGroup { get; }
        public Group Group { get; }
    }
}
