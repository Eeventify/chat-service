using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chat_service.Models;

namespace Chat_service.Controllers
{

    [Route("[controller]")]
    [ApiController]
    //[Produces("application/json")]
    public class ChatHistoryController : ControllerBase
    {
        private readonly ChatContext _context;

        public ChatHistoryController(ChatContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Get/{chatroom}")]
        public async Task<ActionResult<ChatMessage>> GetChatHistory(string chatroom)
        {
            var chat = _context.Messages.Where(x => x.ChatRoom == chatroom).ToList();
            return Ok(chat);
        }

    }
}
