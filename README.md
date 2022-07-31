# TelegramSimpleBot
Free Simple Telegram Bot Library 

# Free
https://www.nuget.org/packages/TelegramSimpleBot/

# Simple
Very Easy To Use

# Many Possibilities
Send Message, Formatted Message
Send Media, Document, Poll, Dice, Location
Promote, Ristrict, Kick, Ban, Unban Member Chat
Detect Member Chat Join or Leave
Get Use Profile Photos
Set Chat Permissions, Title, Description, Photo
...

Example:
```
TelegramBot bot;
static void Main()
{
  bot = new TelegramBot("apikey")
  bot.Events.onMessageReceive += Events_onMessageReceive;
  bot.Events.onFileReceive += Events_onFileReceive;
  bot.Events.onMemberChatJoin += Events_onMemberChatJoin;
  bot.Events.onMemberChatLeave += Events_onMemberChatLeave;
  bot.Events.onPollAnsware += Events_onPollAnsware;
  bot.Events.onMessageEdited += Events_onMessageEdited;
  bot.Events.onLocationReceve += Events_onLocationReceve;
  bot.Events.onErrorReceive += Events_onErrorReceive;
  while (true)
    bot.PollMessages();
}

  static void Events_onErrorReceive(object sender, ErrorReceiveEvent e)
  {
    Console.WriteLine("Error: " + e.Error);
  }

  static void Events_onLocationReceve(object sender, LocationReceiveEvent e)
  {
    Console.WriteLine($"[{e.From.Id}] {e.From.Username ?? e.From.FirstName}: {e.Message.Text} | Latituge: {e.Location.Latitude} Longituge:{e.Location.Longitude}"
    + (e.Location.Address != null ? " | Title: " + e.Location.Address : "")
    + (e.Location.Title != null ? " | Title: " + e.Location.Title : "")
    + (e.InGroup ? " | Grout Title: " + e.Group.Title + " | Id: " + e.Group.Id : ""));
  }

  private static void Events_onMessageEdited(object sender, MessageEditedEvent e)
  {
    Console.WriteLine($"{e.Message.Date.ToShortTimeString()} Message Edited: {e.Message.MessageId} Text: {e.Message.Text}" +
    (e.InGroup ? " | Grout Title: " + e.Group.Title + " | Id: " + e.Group.Id : ""));
  }

  private static void Events_onPollAnsware(object sender, PollAnswareEvent e)
  {
    Console.Write($"Question: {e.Poll.Question} Options=");
    for (int i = 0; i < e.Poll.Options.Length; i++)
    Console.Write(e.Poll.Options[i].Text + ":" + e.Poll.Options[i].VoteCount + " ");
    Console.WriteLine("| Total Voters: " + e.Poll.TotalVotes);
  }

  private static void Events_onMemberChatLeave(object sender, MemberChatLeaveEvent e)
  {
    Console.WriteLine($"{e.Date.ToShortTimeString()} Leave: {e.User.Username ?? e.User.FirstName} In: {e.Group.Username} By: {e.By.Username}" +
  (e.IsAdmin ? $" | {e.User.Username} Was an administor" : ""));
  }

  private static void Events_onMemberChatJoin(object sender, MemberChatJoinEvent e)
  {
    Console.WriteLine($"{e.Date.ToShortTimeString()} Join: {e.User.Username ?? e.User.FirstName} In: {e.Group.Username} By: {e.By.Username}" +
  (e.IsAdmin ? $" | {e.User.Username} An administor" : ""));
  }

  private static void Events_onFileReceive(object sender, FileReceiveEvent e)
  {
    Console.WriteLine($"[{e.From.Id}] {e.From.Username}: {e.File.Caption} | Type: {e.File.Type}" +
  (e.InGroup ? " | Grout Title: " + e.Group.Title + " | Id: " + e.Group.Id : ""));
    if (e.File.Type == "Photo")
    {
      System.IO.File.WriteAllBytes(e.File.FileId + ".jpg", bot.DownloadFile(e.File.FilePath));
    }
  }

  private static void Events_onMessageReceive(object sender, MessageReceiveEvent e)
  {
    Console.WriteLine($"{e.Message.Date.ToShortTimeString()} [{e.From.Id}] {e.From.Username ?? e.From.FirstName}: {e.Message.Text}" +
    (e.InGroup ? " | Grout Title: " + e.Group.Title + " | Id: " + e.Group.Id : ""));
    if (e.Message.Text.ToLower() == "poll")
      bot.SendPoll(e.Group.Id ?? e.From.Id, "Question", new string[] { "Yes", "No" }, true);
    if (e.Message.Text.ToLower() == "image")
      bot.SendPhoto(e.From.Id, "1.png");
  }
```
