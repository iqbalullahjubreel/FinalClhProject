using FinalClhProject.User_to_User_Chat_Integration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.User_to_User_Chat_Integration.Repository
{
    public interface IChatRepository
    {
        Conversation GetOrCreateConversation(int user1Id, int user2Id);
        void AddMessage(Message message);
        List<Message> GetMessages(int conversationId);
    }

}
