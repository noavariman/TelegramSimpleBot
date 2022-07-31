using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using TelegramSimpleBot.ITypes;
using IType = TelegramSimpleBot.Types;
using System.Drawing;

namespace TelegramSimpleBot
{
    public class TelegramBot
    {
        #region Variables
        //private TelegramBotEvents _events = new TelegramBotEvents();
        public TelegramBotEvents Events { get; set; }
        //{
        //    get { return _events; }
        //    set
        //    {
        //        if (value == null) _events = new TelegramBotEvents();
        //        else _events = value;
        //    }
        //}
        private HttpClient _client;
        private string _apitoken;
        public string ApiToken
        {
            get { return _apitoken; }
            set { _apitoken = value; }
        }
        private string _requesturl;
        public string RequestUrl
        {
            get { return _requesturl + _apitoken; }
            set { _requesturl = value; }
        }
        private Me _me;
        public Me Me { get { return _me; } }
        private int lastupdateid;
        private string timeout;
        #endregion

        #region Constractor
        public TelegramBot(string apiToken)
            : this(apiToken, new HttpClient(), "https://api.telegram.org/bot", 10) { }
        public TelegramBot(string apiToken, string requesturl)
            : this(apiToken, new HttpClient(), requesturl, 10) { }
        public TelegramBot(string apiToken, HttpClient client)
            : this(apiToken, client, "https://api.telegram.org/bot", 10) { }
        public TelegramBot(string apiToken, HttpClient client, string requesturl, int timeout)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Events = new TelegramBotEvents();
            RequestUrl = requesturl;
            ApiToken = apiToken;
            this.timeout = timeout.ToString();
            _client = client;
            _me = GetMe();
        }
        #endregion

        #region Methods
        public void PollMessages()
        {
            foreach (Update upd in GetUpdates())
            {
                try
                {
                    if (upd.Edited_Message != null)
                    {
                        #region Edit Message
                        var message = new IType.Message();
                        message.MessageId = upd.Edited_Message.Message_Id;
                        message.Text = upd.Edited_Message.Text;
                        DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        message.Date = date.AddSeconds(upd.Message.Date).ToLocalTime();
                        var user = new IType.User();
                        user.Id = upd.Edited_Message.From.Id.ToString();
                        user.FirstName = upd.Edited_Message.From.First_Name;
                        user.LastName = upd.Edited_Message.From.Last_Name;
                        user.Username = upd.Edited_Message.From.Username;
                        user.IsBot = upd.Edited_Message.From.Is_Bot;
                        var group = new IType.Group();
                        if (upd.Edited_Message.Chat.Title != null)
                        {
                            group.Id = upd.Edited_Message.Chat.Id;
                            group.Username = upd.Edited_Message.Chat.Username;
                            group.FirstName = upd.Edited_Message.Chat.First_Name;
                            group.LastName = upd.Edited_Message.Chat.Last_Name;
                            group.Title = upd.Edited_Message.Chat.Title;
                            group.Bio = upd.Edited_Message.Chat.Bio;
                            group.Type = upd.Edited_Message.Chat.Type;
                        }
                        try
                        {
                            Events.MessageEdited(this, new MessageEditedEvent(message, upd.Edited_Message.Chat.Title != null, upd.Edited_Message.Chat.Title != null ? group : null));
                        }
                        catch { }
                        #endregion
                    }
                    else if (upd.Message != null && (upd.Message.Location != null || upd.Message.Venue != null))
                    {
                        #region Location
                        var message = new IType.Message();
                        message.MessageId = upd.Edited_Message.Message_Id;
                        message.Text = upd.Edited_Message.Text;
                        DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        message.Date = date.AddSeconds(upd.Message.Date).ToLocalTime();
                        var user = new IType.User();
                        user.Id = upd.Message.Left_Chat_Member.Id.ToString();
                        user.FirstName = upd.Message.Left_Chat_Member.First_name;
                        user.LastName = upd.Message.Left_Chat_Member.Last_name;
                        user.Username = upd.Message.Left_Chat_Member.Username;
                        user.IsBot = upd.Message.Left_Chat_Member.Is_Bot;
                        var group = new IType.Group();
                        if (upd.Edited_Message.Chat.Title != null)
                        {
                            group.Id = upd.Edited_Message.Chat.Id;
                            group.Username = upd.Edited_Message.Chat.Username;
                            group.FirstName = upd.Edited_Message.Chat.First_Name;
                            group.LastName = upd.Edited_Message.Chat.Last_Name;
                            group.Title = upd.Edited_Message.Chat.Title;
                            group.Bio = upd.Edited_Message.Chat.Bio;
                            group.Type = upd.Edited_Message.Chat.Type;
                        }
                        if (upd.Message.Location != null)
                        {
                            try
                            {
                                Events.LocationReceive(this, new LocationReceiveEvent(message, user, new Types.Location
                                {
                                    Latitude = upd.Message.Location.Latitude,
                                    Longitude = upd.Message.Location.Longitude
                                }, upd.Edited_Message.Chat.Title != null, upd.Edited_Message.Chat.Title != null ? group : null));
                            }
                            catch { }
                        }
                        else if (upd.Message.Venue != null)
                        {
                            try
                            {
                                Events.LocationReceive(this, new LocationReceiveEvent(message, user, new Types.Location
                                {
                                    Latitude = upd.Message.Venue.Location.Latitude,
                                    Longitude = upd.Message.Venue.Location.Longitude,
                                    Address = upd.Message.Venue.Address,
                                    Title = upd.Message.Venue.Title
                                }, upd.Edited_Message.Chat.Title != null, upd.Edited_Message.Chat.Title != null ? group : null));
                            }
                            catch { }
                        }
                        #endregion
                    }
                    else if (upd.Message != null)
                    {
                        if (upd.Message.Left_Chat_Member != null)
                        {
                            #region Left Chat Member
                            var user = new IType.User();
                            user.Id = upd.Message.Left_Chat_Member.Id.ToString();
                            user.FirstName = upd.Message.Left_Chat_Member.First_name;
                            user.LastName = upd.Message.Left_Chat_Member.Last_name;
                            user.Username = upd.Message.Left_Chat_Member.Username;
                            user.IsBot = upd.Message.Left_Chat_Member.Is_Bot;
                            var group = new IType.Group();
                            group.Id = upd.Message.Chat.Id;
                            group.Username = upd.Message.Chat.Username;
                            group.FirstName = upd.Message.Chat.First_Name;
                            group.LastName = upd.Message.Chat.Last_Name;
                            group.Title = upd.Message.Chat.Title;
                            group.Bio = upd.Message.Chat.Bio;
                            group.Type = upd.Message.Chat.Type;
                            var by = new IType.User();
                            by.Id = upd.Message.From.Id.ToString();
                            by.FirstName = upd.Message.From.First_Name;
                            by.LastName = upd.Message.From.Last_Name;
                            by.Username = upd.Message.From.Username;
                            by.IsBot = upd.Message.From.Is_Bot;
                            try
                            {
                                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                                date = date.AddSeconds(upd.Message.Date).ToLocalTime();
                                Events.MemberChatLeave(this, new MemberChatLeaveEvent(user, group, by, upd.Message.Left_Chat_Participant == null ? false : true, date));
                            }
                            catch { }
                            #endregion
                        }
                        else if (upd.Message.New_Chat_Member != null)
                        {
                            #region New Chat Member
                            var user = new IType.User();
                            user.Id = upd.Message.New_Chat_Member.Id.ToString();
                            user.FirstName = upd.Message.New_Chat_Member.First_name;
                            user.LastName = upd.Message.New_Chat_Member.Last_name;
                            user.Username = upd.Message.New_Chat_Member.Username;
                            user.IsBot = upd.Message.New_Chat_Member.Is_Bot;
                            var group = new IType.Group();
                            group.Id = upd.Message.Chat.Id;
                            group.Username = upd.Message.Chat.Username;
                            group.FirstName = upd.Message.Chat.First_Name;
                            group.LastName = upd.Message.Chat.Last_Name;
                            group.Title = upd.Message.Chat.Title;
                            group.Bio = upd.Message.Chat.Bio;
                            group.Type = upd.Message.Chat.Type;
                            var by = new IType.User();
                            by.Id = upd.Message.From.Id.ToString();
                            by.FirstName = upd.Message.From.First_Name;
                            by.LastName = upd.Message.From.Last_Name;
                            by.Username = upd.Message.From.Username;
                            by.IsBot = upd.Message.From.Is_Bot;
                            try
                            {
                                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                                date = date.AddSeconds(upd.Message.Date).ToLocalTime();
                                Events.MemberChatJoin(this, new MemberChatJoinEvent(user, group, by, upd.Message.New_Chat_Participant == null ? false : true, date));
                            }
                            catch { }
                            #endregion
                        }
                        else
                        {
                            #region Message And File
                            var message = new IType.Message();
                            message.MessageId = upd.Message.Message_Id;
                            message.Text = upd.Message.Text;
                            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                            message.Date = date.AddSeconds(upd.Message.Date).ToLocalTime();
                            var user = new IType.User();
                            user.Id = upd.Message.From.Id.ToString();
                            user.FirstName = upd.Message.From.First_Name;
                            user.LastName = upd.Message.From.Last_Name;
                            user.Username = upd.Message.From.Username;
                            user.IsBot = upd.Message.From.Is_Bot;
                            var group = new IType.Group();
                            if (upd.Message.Chat.Title != null)
                            {
                                group.Id = upd.Message.Chat.Id;
                                group.Username = upd.Message.Chat.Username;
                                group.FirstName = upd.Message.Chat.First_Name;
                                group.LastName = upd.Message.Chat.Last_Name;
                                group.Title = upd.Message.Chat.Title;
                                group.Bio = upd.Message.Chat.Bio;
                                group.Type = upd.Message.Chat.Type;
                            }
                            object f = null;
                            if (upd.Message.Photo != null)
                            {
                                var file = new IType.Photo();
                                file.FileId = upd.Message.Photo[0].File_Id;
                                file.FileSize = upd.Message.Photo[0].File_Size;
                                file.FilePath = GetFile(upd.Message.Photo[0].File_Id).File_Path;
                                file.FileUniqueId = upd.Message.Photo[0].File_Unique_Id;
                                file.Caption = upd.Message.Caption;
                                file.Type = "photo";
                                file.Width = upd.Message.Photo[0].Width;
                                file.Height = upd.Message.Photo[0].Height;
                                f = file;
                            }
                            else if (upd.Message.Video != null)
                            {
                                var file = new IType.Video();
                                file.FileId = upd.Message.Video.File_Id;
                                file.FileSize = upd.Message.Video.File_Size;
                                file.FilePath = GetFile(upd.Message.Video.File_Id).File_Path;
                                file.FileUniqueId = upd.Message.Video.File_Unique_Id;
                                file.Caption = upd.Message.Caption;
                                file.Type = "video";
                                file.Width = upd.Message.Video.Width;
                                file.Height = upd.Message.Video.Height;
                                file.Duration = upd.Message.Video.Duration;
                                f = file;
                            }
                            else if (upd.Message.Video_Note != null)
                            {
                                var file = new IType.VideoNote();
                                file.FileId = upd.Message.Video_Note.File_Id;
                                file.FileSize = upd.Message.Video_Note.File_Size;
                                file.FilePath = GetFile(upd.Message.Video_Note.File_Id).File_Path;
                                file.FileUniqueId = upd.Message.Video_Note.File_Unique_Id;
                                file.Caption = upd.Message.Caption;
                                file.Type = "videonote";
                                file.Length = upd.Message.Video_Note.Length;
                                file.Duration = upd.Message.Video_Note.Duration;
                                f = file;
                            }
                            else if (upd.Message.Audio != null)
                            {
                                var file = new IType.Audio();
                                file.FileId = upd.Message.Audio.File_Id;
                                file.FileSize = upd.Message.Audio.File_Size;
                                file.FilePath = GetFile(upd.Message.Audio.File_Id).File_Path;
                                file.FileUniqueId = upd.Message.Audio.File_Unique_Id;
                                file.Caption = upd.Message.Caption;
                                file.Type = "audio";
                                file.Title = upd.Message.Audio.Title;
                                file.FileName = upd.Message.Audio.File_Name;
                                file.Duration = upd.Message.Audio.Duration;
                                f = file;
                            }
                            else if (upd.Message.Voice != null)
                            {
                                var file = new IType.Voice();
                                file.FileId = upd.Message.Voice.File_Id;
                                file.FileSize = upd.Message.Voice.File_Size;
                                file.FilePath = GetFile(upd.Message.Voice.File_Id).File_Path;
                                file.FileUniqueId = upd.Message.Voice.File_Unique_Id;
                                file.Caption = upd.Message.Caption;
                                file.Type = "voice";
                                file.Duration = upd.Message.Voice.Duration;
                                f = file;
                            }
                            else if (upd.Message.Document != null)
                            {
                                var file = new IType.Document();
                                file.FileId = upd.Message.Document.File_Id;
                                file.FileSize = upd.Message.Document.File_Size;
                                file.FilePath = GetFile(upd.Message.Document.File_Id).File_Path;
                                file.FileUniqueId = upd.Message.Document.File_Unique_Id;
                                file.Caption = upd.Message.Caption;
                                file.Type = "document";
                                file.FileName = upd.Message.Document.File_Name;
                                f = file;
                            }
                            else if (upd.Message.Sticker != null)
                            {
                                var file = new IType.Sticker();
                                file.FileId = upd.Message.Sticker.File_Id;
                                file.FileSize = upd.Message.Sticker.File_Size;
                                file.FilePath = GetFile(upd.Message.Sticker.File_Id).File_Path;
                                file.FileUniqueId = upd.Message.Sticker.File_Unique_Id;
                                file.Caption = upd.Message.Caption;
                                file.Type = "sticker";
                                file.Width = upd.Message.Sticker.Width;
                                file.Height = upd.Message.Sticker.Height;
                                file.Duration = upd.Message.Sticker.Duration;
                                file.IsAnimated = upd.Message.Sticker.Is_Animated;
                                file.IsVideo = upd.Message.Sticker.Is_Video;
                                file.Emoji = upd.Message.Sticker.Emoji;
                                file.SetName = upd.Message.Sticker.Set_Name;
                                f = file;
                            }
                            else if (upd.Message.Animation != null)
                            {
                                var file = new IType.Animation();
                                file.FileId = upd.Message.Animation.File_Id;
                                file.FileSize = upd.Message.Animation.File_Size;
                                file.FilePath = GetFile(upd.Message.Animation.File_Id).File_Path;
                                file.FileUniqueId = upd.Message.Animation.File_Unique_Id;
                                file.Caption = upd.Message.Caption;
                                file.Type = "animation";
                                file.Width = upd.Message.Animation.Width;
                                file.Height = upd.Message.Animation.Height;
                                file.Duration = upd.Message.Animation.Duration;
                                file.FileName = upd.Message.Animation.File_Name;
                                f = file;
                            }
                            if (f != null)
                                try
                                {
                                    Events.FileReceive(this, new FileReceiveEvent(message, user, f as IType.File, upd.Message.Chat.Title != null, upd.Message.Chat.Title != null ? group : null));
                                }
                                catch { }
                            else
                                try
                                {
                                    Events.MessageReceive(this, new MessageReceiveEvent(message, user, upd.Message.Chat.Title != null, upd.Message.Chat.Title != null ? group : null));
                                }
                                catch { }
                            #endregion
                        }
                        continue;
                    }
                    if (upd.Poll != null)
                    {
                        #region Poll Answare
                        var poll = new IType.Poll();
                        poll.Id = upd.Poll.Id;
                        poll.Question = upd.Poll.question;
                        poll.TotalVotes = upd.Poll.Total_Voter_Count;
                        poll.IsAnonymous = upd.Poll.Is_Anonymous;
                        var polls = new List<IType.PollOption>();
                        foreach (PollOption po in upd.Poll.options)
                            polls.Add(new IType.PollOption() { Text = po.Text, VoteCount = po.Voter_Count });
                        poll.Options = polls.ToArray();
                        try
                        {
                            Events.PollAnsware(this, new PollAnswareEvent(poll));
                        }
                        catch { }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        Events.ErrorReceive(this, new ErrorReceiveEvent(ex.Message, false, null));
                    }
                    catch { }
                }
            }
        }

        public Me GetMe()
        {
            string responce = DoScript("getme");
            return JsonConvert.DeserializeObject<Me>(JObject.Parse(responce)["result"].ToString());
        }

        public List<Message> GetMessages(string chat_id)
        {
            string result = DoScript($"getupdates");
            return JsonConvert.DeserializeObject<List<Message>>(JObject.Parse(result)["result"].ToString());
        }

        private List<Update> GetUpdates()
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters["timeout"] = timeout.ToString(); ;
                parameters["offset"] = (lastupdateid + 1).ToString();

                List<Update> response = new List<Update>();
                while (response.Count == 0)
                {
                    string result = DoScript("getUpdates", new FormUrlEncodedContent(parameters));
                    JObject obj = JObject.Parse(result);
                    response = JsonConvert.DeserializeObject<List<Update>>(obj["result"].ToString());
                }

                if (!response.Any()) return new List<Update>();

                lastupdateid = response.Last().Update_Id;
                return response;
            }
            catch (Exception ex)
            {
                Events.ErrorReceive(this, new ErrorReceiveEvent(ex.Message, false, null));
                return new List<Update>();
            }
        }

        public Result SendMessage(string chat_id, string text, IType.Message replytomessage = null)
        {
            for (int i = 0; i < text.Length; i++)
                if (text[i] == ' ')
                {
                    text = text.Remove(i, 1);
                    text = text.Insert(i, "%20");
                }

            return ToResult(DoScript($"sendmessage?chat_id={chat_id}&text={text}" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString())));
        }

        public Result SendMessageFormattedV2(string chat_id, string text, IType.Message replytomessage = null)
        {
            for (int i = 0; i < text.Length; i++)
                if (text[i] == ' ')
                {
                    text = text.Remove(i, 1);
                    text = text.Insert(i, "%20");
                }

            return ToResult(DoScript($"sendmessage?chat_id={chat_id}&text={text}&parse_mode=MarkdownV2" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString())));
        }

        public Result SendMessageFormatted(string chat_id, string text, IType.Message replytomessage = null)
        {
            for (int i = 0; i < text.Length; i++)
                if (text[i] == ' ')
                {
                    text = text.Remove(i, 1);
                    text = text.Insert(i, "%20");
                }

            return ToResult(DoScript($"sendmessage?chat_id={chat_id}&text={text}&parse_mode=Markdown" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString())));
        }

        public Result SendMessageHtml(string chat_id, string text, IType.Message replytomessage = null)
        {
            for (int i = 0; i < text.Length; i++)
                if (text[i] == ' ')
                {
                    text = text.Remove(i, 1);
                    text = text.Insert(i, "%20");
                }

            return ToResult(DoScript($"sendmessage?chat_id={chat_id}&text={text}&parse_mode=HTML" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString())));
        }

        public Result SendPhoto(string chat_id, string filename, string caption = "", IType.Message replytomessage = null)
        {
            if (!File.Exists(filename))
                return new Result() { Ok = false, Description = "File not found" };
            return SendPhoto(chat_id, File.ReadAllBytes(filename), caption, replytomessage);
        }
        public Result SendPhoto(string chat_id, byte[] bytes, string caption = "", IType.Message replytomessage = null)
        {
            for (int i = 0; i < caption.Length; i++)
                if (caption[i] == ' ')
                {
                    caption = caption.Remove(i, 1);
                    caption = caption.Insert(i, "%20");
                }

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "photo", Path.GetFileName("myphoto"));

            return ToResult(DoScript($"sendphoto?chat_id={chat_id}&caption={caption}" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString()), content));
        }

        public Result SendVideo(string chat_id, string filename, string caption, IType.Message replytomessage = null, int duration = 0)
        {
            byte[] bytes = File.ReadAllBytes(filename);

            for (int i = 0; i < caption.Length; i++)
                if (caption[i] == ' ')
                {
                    caption = caption.Remove(i, 1);
                    caption = caption.Insert(i, "%20");
                }

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "video", Path.GetFileName(filename));
            if (duration != 0)
                content.Add(new StringContent(duration.ToString()), "duration");

            return ToResult(DoScript($"sendvideo?chat_id={chat_id}&caption={caption}&duration={duration}" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString()), content));
        }

        public Result SendVideoNote(string chat_id, string filename, IType.Message replytomessage = null, int duration = 0)
        {
            byte[] bytes = File.ReadAllBytes(filename);

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "video_note", Path.GetFileName(filename));
            if (duration != 0)
                content.Add(new StringContent(duration.ToString()), "duration");

            return ToResult(DoScript($"sendvideonote?chat_id={chat_id}", content));
        }

        public Result SendAnimation(string chat_id, string filename, string caption, IType.Message replytomessage = null, int duration = 0)
        {
            byte[] bytes = File.ReadAllBytes(filename);

            for (int i = 0; i < caption.Length; i++)
                if (caption[i] == ' ')
                {
                    caption = caption.Remove(i, 1);
                    caption = caption.Insert(i, "%20");
                }

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "animation", Path.GetFileName(filename));
            if (duration != 0)
                content.Add(new StringContent(duration.ToString()), "duration");

            return ToResult(DoScript($"sendanimation?chat_id={chat_id}&caption{caption}" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString()), content));
        }

        public Result SendAudio(string chat_id, string filename, string caption, IType.Message replytomessage = null, int duration = 0)
        {
            byte[] bytes = File.ReadAllBytes(filename);

            for (int i = 0; i < caption.Length; i++)
                if (caption[i] == ' ')
                {
                    caption = caption.Remove(i, 1);
                    caption = caption.Insert(i, "%20");
                }

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "audio", Path.GetFileName(filename));
            if (duration != 0)
                content.Add(new StringContent(duration.ToString()), "duration");

            return ToResult(DoScript($"sendaudio?chat_id={chat_id}&caption={caption}" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString()), content));
        }

        public Result SendVoice(string chat_id, string filename, string caption, IType.Message replytomessage = null, int duration = 0)
        {
            byte[] bytes = File.ReadAllBytes(filename);

            for (int i = 0; i < caption.Length; i++)
                if (caption[i] == ' ')
                {
                    caption = caption.Remove(i, 1);
                    caption = caption.Insert(i, "%20");
                }

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "voice", Path.GetFileName(filename));
            if (duration != 0)
                content.Add(new StringContent(duration.ToString()), "duration");

            return ToResult(DoScript($"sendvoice?chat_id={chat_id}&caption={caption}" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString()), content));
        }

        //public Result SendMediaGroup(string chat_id, Dictionary<string, string> files, string caption)
        //{
        //    return null;
        //    var content = new MultipartFormDataContent();
        //    foreach (var file in files)
        //        content.Add(new ByteArrayContent(File.ReadAllBytes(file.Key)), file.Value);

        //    var content2 = new MultipartFormDataContent();
        //    content2.Add(content, "media");
        //    content2.Add(new StringContent(caption), "caption");

        //    Console.WriteLine(DoScript($"sendvoice?chat_id={chat_id}", content2));
        //    return null;
        //}

        public Result SendSticker(string chat_id, string filename, IType.Message replytomessage = null, int duration = 0)
        {
            byte[] bytes = File.ReadAllBytes(filename);

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "sticker", Path.GetFileName(filename));
            if (duration != 0)
                content.Add(new StringContent(duration.ToString()), "duration");

            return ToResult(DoScript($"sendsticker?chat_id={chat_id}" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString()), content));
        }

        public Result SendDocument(string chat_id, string filename, string caption = "", IType.Message replytomessage = null)
        {
            byte[] bytes = File.ReadAllBytes(filename);

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "document", Path.GetFileName(filename));

            return ToResult(DoScript($"senddocument?chat_id={chat_id}&caption={caption}" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString()), content));
        }

        public Result SendLocation(string chat_id, float latitude, float longitude, IType.Message replytomessage = null)
        {
            return ToResult(DoScript($"SendLocation?chat_id={chat_id}&longitude={longitude}&latitude={latitude}" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString())));
        }

        public Result SendVeune(string chat_id, float latitude, float longitude, string title, string address, IType.Message replytomessage = null)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(title), "title");
            content.Add(new StringContent(address), "address");
            return ToResult(DoScript($"sendvaune?chat_id={chat_id}&longitude={longitude}&latitude={latitude}" +
                (replytomessage == null ? "" : "&reply_to_message_id=" + replytomessage.MessageId.ToString()), content));
        }

        public Result SendPoll(string chat_id, string question, string[] options, bool IsAnonymous = false)
        {
            for (int i = 0; i < question.Length; i++)
                if (question[i] == ' ')
                {
                    question = question.Remove(i, 1);
                    question = question.Insert(i, "%20");
                }

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(JsonConvert.SerializeObject(options)), "options");

            string tt = DoScript($"sendpoll?chat_id={chat_id}&question={question}&is_anonymous=" + IsAnonymous.ToString(), content);
            Result res = ToResult(tt);
            return res;
        }

        public Result SendChatAction(string chat_id, ChatActions action)
        {
            return ToResult(DoScript($"sendchataction?chat_id={chat_id}&action={action}"));
        }

        public Result ForwardMessage(Message message, string chat_id, bool protect_content = false)
        {
            var content = new MultipartFormDataContent();
            if (protect_content != false)
                content.Add(new StringContent("protect_content"), protect_content.ToString());

            return ToResult(DoScript($"forwardmessage?chat_id={chat_id}&message_id={message.Message_Id}&from_chat_id{(message.Chat == null ? message.From.Id : message.Chat.Id)}", content));
        }

        public Result EditMessage(string chat_id, string message_id, string text)
        {
            return ToResult(DoScript($"editmessagetext?chat_id={chat_id}&message_id={message_id}&text={text}"));
        }

        public Result DeleteMessage(string chat_id, string message_id)
        {
            return ToResult(DoScript($"deletemessage?chat_id={chat_id}&message_id={message_id}"));
        }

        public int SendDice(string chat_id, string emoji = null)
        {
            string result = DoScript($"senddice?chat_id={chat_id}&emoji={emoji}");
            Dice dice = JsonConvert.DeserializeObject<Dice>(JObject.Parse(result)["result"]["dice"].ToString());
            return dice.Value;
        }

        public FileResult GetFile(string file_id)
        {
            string result = DoScript($"getfile?file_id={file_id}");
            FileResult file = JsonConvert.DeserializeObject<FileResult>(JObject.Parse(result)["result"].ToString());
            return file;
        }

        public byte[] DownloadFile(string file_path)
        {
            var webClient = new WebClient();
            return webClient.DownloadData(_requesturl.Remove(_requesturl.Length - 3, 3) + "file/bot" + _apitoken + "/" + file_path);
        }

        public Result LeaveChat(string chat_id)
        {
            return ToResult(DoScript($"leavechat?chat_id={chat_id}"));
        }

        public int GetChatMemberCount(string chat_id)
        {
            string result = DoScript($"getChatMemberCount?chat_id={chat_id}");
            JObject obj = JObject.Parse(result);
            int num = 0;
            if (int.TryParse(obj["result"].ToString(), out num))
                return num;
            return 0;
        }

        public List<IType.ChatAdministrator> GetChatAdministrators(string chat_id)
        {
            string result = DoScript($"getchatadministrators?chat_id={chat_id}");
            JObject obj = JObject.Parse(result);
            List<ChatAdministrator> admins = JsonConvert.DeserializeObject<List<ChatAdministrator>>(obj["result"].ToString());
            List<IType.ChatAdministrator> cadmins = new List<IType.ChatAdministrator>();
            foreach (var admin in admins)
            {
                var cadmin = new IType.ChatAdministrator();
                cadmin.User = new Types.User();
                cadmin.Permissions = new Types.Permissions();
                cadmin.User.Id = admin.User.Id;
                cadmin.User.IsBot = admin.User.Is_Bot;
                cadmin.User.Username = admin.User.Username;
                cadmin.User.FirstName = admin.User.First_Name;
                cadmin.User.LastName = admin.User.Last_Name;
                cadmin.Permissions.Status = admin.status;
                cadmin.Permissions.CanBeEdited = admin.can_be_edited;
                cadmin.Permissions.CanChangeInfo = admin.can_change_info;
                cadmin.Permissions.CanDeleteMessages = admin.can_delete_messages;
                cadmin.Permissions.CanInviteUsers = admin.can_invite_users;
                cadmin.Permissions.CanManageChat = admin.can_manage_chat;
                cadmin.Permissions.CanManageVoiceChats = admin.can_manage_voice_chats;
                cadmin.Permissions.CanManageVideoChats = admin.can_manage_video_chats;
                cadmin.Permissions.CanPinMessages = admin.can_pin_messages;
                cadmin.Permissions.CanPromoteMembers = admin.can_promote_members;
                cadmin.Permissions.CanRestrictMembers = admin.can_restrict_members;
                cadmin.Permissions.IsAnonymous = admin.is_anonymous;
                cadmins.Add(cadmin);
            }
            return cadmins;
        }

        public Result RestrictChatMember(string chat_id, string user_id, IType.ChatPermissions permissions)
        {
            ChatPermissions ps = new ChatPermissions();
            ps.can_add_web_page_previews = permissions.CanAddWebPagePreviews;
            ps.can_change_info = permissions.CanchangeInfo;
            ps.can_invite_users = permissions.CanInviteUsers;
            ps.can_pin_messages = permissions.CanPinMessages;
            ps.can_send_media_messages = permissions.CanSendMediaMessages;
            ps.can_send_messages = permissions.CanSendMessages;
            ps.can_send_other_messages = permissions.CanSendotherMessages;
            ps.can_send_polls = permissions.CanSendPolls;
            return ToResult(DoScript($"restrictchatmember?chat_id={chat_id}&user_id={user_id}&permissions={JsonConvert.SerializeObject(ps)}"));
        }

        public Result PromoteChatMember(string chat_id, string user_id, IType.AdministorPermissions permissions)
        {
            return ToResult(DoScript($"promotechatmember?chat_id={chat_id}&user_id={user_id}"
                + $"&is_anonymous={permissions.IsAnonymous.ToString()}"
                + $"&can_manage_chat={permissions.CanManageChat.ToString()}"
                + $"&can_post_messages={permissions.CanPostMessages.ToString()}"
                + $"&can_edit_messages={permissions.CanEditMessages.ToString()}"
                + $"&can_delete_messages={permissions.CanDeleteMessages.ToString()}"
                + $"&can_manage_video_chats={permissions.CanmanageVideoChats.ToString()}"
                + $"&can_restrict_members={permissions.CanRestrictMembers.ToString()}"
                + $"&can_promote_members={permissions.CanPromoteMembers.ToString()}"
                + $"&can_change_info={permissions.CanChangeInfo.ToString()}"
                + $"&can_invite_users={permissions.CanInviteUsers.ToString()}"
                + $"&can_pin_messages={permissions.CanPinMessages.ToString()}"
                ));
        }

        public Result SetChatPermissions(string chat_id, IType.ChatPermissions permissions)
        {
            ChatPermissions ps = new ChatPermissions();
            ps.can_add_web_page_previews = permissions.CanAddWebPagePreviews;
            ps.can_change_info = permissions.CanchangeInfo;
            ps.can_invite_users = permissions.CanInviteUsers;
            ps.can_pin_messages = permissions.CanPinMessages;
            ps.can_send_media_messages = permissions.CanSendMediaMessages;
            ps.can_send_messages = permissions.CanSendMessages;
            ps.can_send_other_messages = permissions.CanSendotherMessages;
            ps.can_send_polls = permissions.CanSendPolls;
            return ToResult(DoScript($"setchatpermissions?chat_id={chat_id}&permissions={JsonConvert.SerializeObject(ps)}"));
        }

        public Result PinChatMessage(string chat_id, string message_id)
        {
            return ToResult(DoScript($"pinchatmessage?chat_id={chat_id}"));
        }

        public Result UnPinChatMessage(string chat_id, string message_id)
        {
            return ToResult(DoScript($"unpinchatmessage?chat_id={chat_id}"));
        }

        public Result UnPinAllChatMessage(string chat_id)
        {
            return ToResult(DoScript($"unpinallchatmessages?chat_id={chat_id}"));
        }

        public Result setChatPhoto(string chat_id, string filename)
        {
            if (!File.Exists(filename))
                return new Result() { Ok = false, Description = "File not found" };
            return SetChatPhoto(chat_id, File.ReadAllBytes(filename));
        }

        public Result SetChatPhoto(string chat_id, byte[] bytes)
        {
            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(bytes), "photo", "photo");
            return ToResult(DoScript($"setchatphoto?chat_id={chat_id}", content));
        }

        public Result deleteChatPhoto(string chat_id)
        {
            return ToResult(DoScript($"deletechatphoto?chat_id={chat_id}"));
        }

        public Result SetChatTitle(string chat_id, string title)
        {
            return ToResult(DoScript($"setchattitle?chat_id={chat_id}&title={title}"));
        }

        public Result SetChatDescription(string chat_id, string description)
        {
            return ToResult(DoScript($"setchatdescription?chat_id={chat_id}&description={description}"));
        }

        public Result BanChatMember(string chat_id, string user_id, int until_days, int until_hours = 0, int until_minutes = 0)
        {
            DateTime now = DateTime.Now;
            now = now.AddDays(until_days);
            now = now.AddHours(until_hours);
            now = now.AddMinutes(until_minutes);
            long until_date = (long)now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

            return ToResult(DoScript($"banchatmember?chat_id={chat_id}&user_id={user_id}&until_date={until_date}"));
        }

        public Result UnBanChatMember(string chat_id, string user_id)
        {
            return ToResult(DoScript($"unbanchatmember?chat_id={chat_id}&user_id={user_id}"));
        }

        //getUserProfilePhotos
        public IType.UserProfilePhotos GetUserProfilePhotos(string user_id)
        {
            string result = DoScript($"getuserprofilephotos?user_id={user_id}");
            JObject obj = JObject.Parse(result);
            UserProfilePhotos photos = JsonConvert.DeserializeObject<UserProfilePhotos>(obj["result"].ToString());
            IType.UserProfilePhotos iphotos = new Types.UserProfilePhotos();
            iphotos.TotalCount = photos.total_count;
            iphotos.Photos = new List<IType.Photo>();
            List<IType.Photo> mp;
            foreach (var tphoto in photos.photos)
            {
                mp = new List<IType.Photo>();
                foreach (var photo in tphoto)
                {
                    IType.Photo iphoto = new IType.Photo();
                    iphoto.FileId = photo.File_Id;
                    iphoto.FileSize = photo.File_Size;
                    iphoto.FileUniqueId = photo.File_Unique_Id;
                    iphoto.Width = photo.Width;
                    iphoto.Height = photo.Height;
                    mp.Add(iphoto);
                }
                int max = mp.Select(p => p.Width).Max();
                iphotos.Photos.Add(mp.Single(p => p.Width == max));
            }
            return iphotos;
        }
        #endregion

        #region Scripts
        public string DoScript(string method)
        {
            HttpResponseMessage response = _client.GetAsync(RequestUrl + "/" + method).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        public string DoScript(string method, HttpContent content)
        {
            string p = RequestUrl + "/" + method;
            HttpResponseMessage response = _client.PostAsync(RequestUrl + "/" + method, content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        public Result ToResult(string input)
        {
            Result res = JsonConvert.DeserializeObject<Result>(input);
            if (JObject.Parse(input)["result"] != null && JObject.Parse(input)["result"].ToString().Length > 4)
                res.Message = JsonConvert.DeserializeObject<Message>(JObject.Parse(input)["result"].ToString());
            return res;
        }
        public Type GetType(Type type)
        {
            foreach (PropertyInfo p in type.GetProperties())
                if (p.GetValue(type) != null)
                    return p.GetType();
            return null;
        }
        #endregion
    }
}