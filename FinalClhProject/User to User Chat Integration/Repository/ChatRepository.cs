using FinalClhProject.User_to_User_Chat_Integration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.User_to_User_Chat_Integration.Repository
{
    public class ChatRepository : IChatRepository
    {
        public Conversation GetOrCreateConversation(int user1Id, int user2Id)
        {
            var convo = InMemoryChatStore.Conversations.FirstOrDefault(
                c => (c.User1Id == user1Id && c.User2Id == user2Id) ||
                     (c.User1Id == user2Id && c.User2Id == user1Id));

            if (convo == null)
            {
                convo = new Conversation { Id = InMemoryChatStore.Conversations.Count + 1, User1Id = user1Id, User2Id = user2Id };
                InMemoryChatStore.Conversations.Add(convo);
            }

            return convo;
        }

        public void AddMessage(Message message)
        {
            message.Id = InMemoryChatStore.Messages.Count + 1;
            InMemoryChatStore.Messages.Add(message);

            var convo = InMemoryChatStore.Conversations.FirstOrDefault(c => c.Id == message.ConversationId);
            convo?.Messages.Add(message);
        }

        public List<Message> GetMessages(int conversationId)
        {
            return InMemoryChatStore.Messages.Where(m => m.ConversationId == conversationId).OrderBy(m => m.Timestamp).ToList();
        }
    }

}
