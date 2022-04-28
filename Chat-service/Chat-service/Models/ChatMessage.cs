namespace Chat_service.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? User { get; set; }
        public string? ChatRoom { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
