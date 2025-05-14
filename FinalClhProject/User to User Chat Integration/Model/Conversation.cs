//using GroqSharp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.User_to_User_Chat_Integration.Model

{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int User1Id { get; set; }

        [Required]
        public int User2Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public User User1 { get; set; }
        public User User2 { get; set; }

        public ICollection<Message> Messages { get; set; }
    }

}
