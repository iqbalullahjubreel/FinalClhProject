using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.DataAccess
{
    public class ChatStorageHistory
    {
        public string Content { get; set; }
        public string Role { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
