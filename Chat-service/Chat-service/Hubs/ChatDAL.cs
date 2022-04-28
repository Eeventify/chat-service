using Chat_service.Models;

namespace Chat_service.Hubs
{
    /// <summary>
    /// DAL for saving chatmessages
    /// </summary>
    public class ChatDAL
    {
        private readonly ChatContext _context;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>
        public ChatDAL(ChatContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Save message to db
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Savemessage(ChatMessage message)
        {
            await _context.AddAsync(message);
            await _context.SaveChangesAsync();
        }
    }
}
