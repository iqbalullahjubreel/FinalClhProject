using GroqSharp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.User_to_User_Chat_Integration.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        // Optional: Navigation properties
        public ICollection<Conversation> ConversationsAsUser1 { get; set; }
        public ICollection<Conversation> ConversationsAsUser2 { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
    }

}
