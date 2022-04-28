using Microsoft.AspNetCore.SignalR;
using Chat_service.Models;
using Chat_service.Controllers;

namespace Chat_service.Hubs
{
    /// <summary>
    /// handels signalR
    /// </summary>
    public class ChatHub : Hub
    {
        private ChatDAL _contoller;

        /// <summary>
        /// needs a chatDAL for saving messages
        /// </summary>
        /// <param name="chat"></param>
        public ChatHub(ChatDAL chat)
        {
            _contoller = chat;
        }

        /// <summary>
        /// used for recieving messages on serverside
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <param name="room"></param>
        /// <param name="join"></param>
        /// <param name="leave"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message, string room, bool join, bool leave)
        {
            if (leave)
            {
                await Clients.Group(room).SendAsync("ReceiveMessage", user, " joined " + room);
                await LeaveRoom(room);
            }
            else if (join)
            {
                await JoinRoom(room).ConfigureAwait(false);
                await Clients.Group(room).SendAsync("ReceiveMessage", user, " joined " + room).ConfigureAwait(true);
            }
            else
            {
                await Clients.Group(room).SendAsync("ReceiveMessage", user, message);
                ChatMessage newchatmessage = new() { ChatRoom = room, Message = message, User = user, Timestamp = DateTime.Now};
                try
                {
                    await _contoller.Savemessage(newchatmessage);
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
        }

        /// <summary>
        /// To create or join a room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public Task JoinRoom(string room)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, room);
        }

        /// <summary>
        /// leave a room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public Task LeaveRoom(string room)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
        }
    }
}
