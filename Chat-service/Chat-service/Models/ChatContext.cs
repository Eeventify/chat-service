using Microsoft.EntityFrameworkCore;

namespace Chat_service.Models
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options): base(options) { }

        public DbSet<ChatMessage> Messages { get; set; }
    }
}
