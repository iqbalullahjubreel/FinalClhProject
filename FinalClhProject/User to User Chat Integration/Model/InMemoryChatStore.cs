using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.User_to_User_Chat_Integration.Model
{
    public static class InMemoryChatStore
    {
        public static List<User> Users { get; set; } = new();
        public static List<Conversation> Conversations { get; set; } = new();
        public static List<Message> Messages { get; set; } = new();
    }
}
