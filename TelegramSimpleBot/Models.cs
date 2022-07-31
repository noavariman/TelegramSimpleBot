namespace TelegramSimpleBot.ITypes
{
    #region media
    public class Animation
    {
        public string File_Id { get; set; }
        public string File_Unique_Id { get; set; }
        public int File_Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Duration { get; set; }
        public PhotoSize Thumb { get; set; }
        public string File_Name { get; set; }
    }

    public class Audio
    {
        public string File_Id { get; set; }
        public string File_Unique_Id { get; set; }
        public int File_Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public string File_Name { get; set; }
    }

    public class Document
    {
        public string File_Id { get; set; }
        public string File_Unique_Id { get; set; }
        public int File_Size { get; set; }
        public PhotoSize Thumb { get; set; }
        public string File_Name { get; set; }
    }

    public class Video
    {
        public string File_Id { get; set; }
        public string File_Unique_Id { get; set; }
        public int File_Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Duration { get; set; }
        public PhotoSize Thumb { get; set; }
        public string File_Name { get; set; }
    }

    public class VideoNote
    {
        public string File_Id { get; set; }
        public string File_Unique_Id { get; set; }
        public int File_Size { get; set; }
        public int Length { get; set; }
        public int Duration { get; set; }
        public PhotoSize Thumb { get; set; }
    }

    public class Voice
    {
        public string File_Id { get; set; }
        public string File_Unique_Id { get; set; }
        public int File_Size { get; set; }
        public int Duration { get; set; }
    }

    public class PhotoSize
    {
        public string File_Id { get; set; }
        public string File_Unique_Id { get; set; }
        public int File_Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Duration { get; set; }
    }

    public class Sticker
    {
        public string File_Id { get; set; }
        public string File_Unique_Id { get; set; }
        public int File_Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Duration { get; set; }
        public bool Is_Animated { get; set; }
        public bool Is_Video { get; set; }
        public PhotoSize Thumb { get; set; }
        public string Emoji { get; set; }
        public string Set_Name { get; set; }
    }

    public class FileResult
    {
        public string File_Id { get; set; }
        public string File_Unique_Id { get; set; }
        public int File_Size { get; set; }
        public string File_Path { get; set; }
    }
    #endregion

    #region Other
    class UserProfilePhotos
    {
        public int total_count { get; set; }
        public PhotoSize[][] photos { get; set; }
    }
    public class ChatAdministrator
    {
        public User User { get; set; }
        public string status { get; set; }
        public bool can_be_edited { get; set; }
        public bool can_manage_chat { get; set; }
        public bool can_change_info { get; set; }
        public bool can_delete_messages { get; set; }
        public bool can_invite_users { get; set; }
        public bool can_restrict_members { get; set; }
        public bool can_pin_messages { get; set; }
        public bool can_promote_members { get; set; }
        public bool can_manage_video_chats { get; set; }
        public bool is_anonymous { get; set; }
        public bool can_manage_voice_chats { get; set; }
    }

    public class ChatPermissions
    {
        public bool can_send_messages { get; set; }
        public bool can_send_media_messages { get; set; }
        public bool can_send_polls { get; set; }
        public bool can_send_other_messages { get; set; }
        public bool can_add_web_page_previews { get; set; }
        public bool can_change_info { get; set; }
        public bool can_invite_users { get; set; }
        public bool can_pin_messages { get; set; }
    }

    public class Dice
    {
        public string Emoji { get; set; }
        public int Value { get; set; }
    }

    public class PollOption
    {
        public string Text { get; set; }
        public int Voter_Count { get; set; }
    }

    public class Poll
    {
        public string Id { get; set; }
        public string question { get; set; }
        public PollOption[] options { get; set; }
        public int Total_Voter_Count { get; set; }
        public bool Is_Anonymous { get; set; }

    }

    public class Location
    {
        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }

    public class Venue
    {
        public Location Location { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
    }

    public class PollAnsware
    {
        public string Poll_Id { get; set; }
        public User User { get; set; }
        public int[] Option_Ids { get; set; }
    }
    #endregion

    #region Message
    public class Contact
    {
        public string Phone_Number { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int User_Id { get; set; }
        public string Vcard { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public bool Is_Bot { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Language_Code { get; set; }
    }

    public class MessageEntity
    {
        public string Type { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
        public string Url { get; set; }
        public User User { get; set; }
        public string Language { get; set; }
    }

    public class From
    {
        public string Id { get; set; }
        public bool Is_Bot { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Language_Code { get; set; }
    }


    public class Chat
    {
        public string Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Type { get; set; }
        public string Bio { get; set; }
    }

    public class Message
    {
        public int Message_Id { get; set; }
        public From From { get; set; }
        public Chat Chat { get; set; }
        public Dice Dice { get; set; }
        public Animation Animation { get; set; }
        public Audio Audio { get; set; }
        public Document Document { get; set; }
        public Sticker Sticker { get; set; }
        public Video Video { get; set; }
        public VideoNote Video_Note { get; set; }
        public Voice Voice { get; set; }
        public Location Location { get; set; }
        public Venue Venue { get; set; }
        public Contact Contact { get; set; }
        public PhotoSize[] Photo { get; set; }
        public MessageEntity[] Entities { get; set; }
        public MessageEntity[] Caption_Entities { get; set; }
        //for member
        public Left_Chat_Participant Left_Chat_Participant { get; set; }
        public Left_Chat_Member Left_Chat_Member { get; set; }
        public New_Chat_Participant New_Chat_Participant { get; set; }
        public New_Chat_Member New_Chat_Member { get; set; }
        //end
        public string Caption { get; set; }
        public string Text { get; set; }
        public int Date { get; set; }
    }
    #endregion

    #region Result
    public class Result
    {
        public bool Ok { get; set; }
        public Message Message { get; set; }
        public int Error_Code { get; set; }
        public string Description { get; set; }
    }

    public class Update
    {
        public int Update_Id { get; set; }
        //for message
        public Message Message { get; set; }
        //for poll
        public Poll Poll { get; set; }
        public Message Edited_Message { get; set; }
    }

    public class Left_Chat_Participant
    {
        public int Id { get; set; }
        public bool Is_Bot { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
    }
    public class Left_Chat_Member
    {
        public int Id { get; set; }
        public bool Is_Bot { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
    }
    public class New_Chat_Participant
    {
        public long Id { get; set; }
        public bool Is_Bot { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
    }
    public class New_Chat_Member
    {
        public long Id { get; set; }
        public bool Is_Bot { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
    }

    public class Me
    {
        public long Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public bool Can_Join_Groups { get; set; }
        public bool Can_Read_All_Group_Messages { get; set; }
        public bool Supports_Inline_Queries { get; set; }
    }
    #endregion

    public enum ChatActions
    {
        Typing,
        Upload_Photo,
        Upload_Video,
        Record_Video,
        Upload_Audio,
        Record_Audio,
        Upload_Document,
        Find_Location,
        Upload_Video_Note,
        Record_Video_Note
    }
}