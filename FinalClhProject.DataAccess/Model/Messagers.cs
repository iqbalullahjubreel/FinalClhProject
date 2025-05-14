using FinalClhProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.DataAccess

{
    public class Messagers
    {
        public string Content { get; set; }
        public MessageRoleType Role { get; set; }

        public enum MessageRoleType
        {
            User,
            Assistant
        }
    }
}
