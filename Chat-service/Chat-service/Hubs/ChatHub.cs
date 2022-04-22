using Microsoft.AspNetCore.SignalR;
using Chat_service.Models;

namespace Chat_service.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string room, bool join)
        {
            if (join)
            {
                await JoinRoom(room).ConfigureAwait(false);
                await Clients.Group(room).SendAsync("ReceiveMessage", user, " joined " + room).ConfigureAwait(true);
            }
            else
            {
                await Clients.Group(room).SendAsync("ReceiveMessage", user, message).ConfigureAwait(true);
            }
            ChatMessage newchatmessage = new() { ChatRoom = room, Message = message, User = user}; // save in db
        }

        public Task JoinRoom(string room)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, room);
        }

        public Task LeaveRoom(string room)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
        }

        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
    }
}
