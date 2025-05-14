using FinalClhProject.User_to_User_Chat_Integration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.User_to_User_Chat_Integration.Model.Service
{
    public class ChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public Conversation GetConversation(int user1Id, int user2Id)
        {
            return _chatRepository.GetOrCreateConversation(user1Id, user2Id);
        }

        public void SendMessage(int conversationId, int senderId, string text)
        {
            var message = new Message
            {
                ConversationId = conversationId,
                SenderId = senderId,
                MessageText = text,
                Timestamp = DateTime.Now
            };

            _chatRepository.AddMessage(message);
        }

        public List<Message> GetMessages(int conversationId)
        {
            return _chatRepository.GetMessages(conversationId);
        }
    }

}
