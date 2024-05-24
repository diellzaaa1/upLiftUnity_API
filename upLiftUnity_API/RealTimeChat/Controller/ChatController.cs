using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.RealTimeChat.Model;
using upLiftUnity_API.RealTimeChat.Repositories;
using upLiftUnity_API.RealTimeChat.Repository.MessageRepository;

namespace upLiftUnity_API.RealTimeChat.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMessageRepository _messageRepository;

        public ChatController(IConversationRepository conversationRepository, IMessageRepository messageRepository)
        {
            _conversationRepository = conversationRepository;
            _messageRepository = messageRepository;
        }

        [HttpGet("conversation")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesByConversationId(string senderEmail, string receiverEmail)
        {
            
            var conversation = await _conversationRepository.GetConversationIdByEmailsAsync(senderEmail, receiverEmail);

            if (conversation == null)
            {
                return NotFound(); 
            }

            
            var messages = await _messageRepository.GetMessagesByConversationIdAsync(conversation);

            if (messages == null)
            {
                return NotFound(); // Mesazhet nuk u gjetën për konversationin e caktuar
            }

            return Ok(messages);
        }


    }
}
