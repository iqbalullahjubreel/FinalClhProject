using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.User_to_User_Chat_Integration.Model

{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ConversationId { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public string MessageText { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Navigation
        public Conversation Conversation { get; set; }
        public User Sender { get; set; }
    }

}
