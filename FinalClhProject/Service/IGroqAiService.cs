using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.Service
{
    public interface IGroqAiService
    {
        public Task<string> AiRizzResponse(string request);
        public Task<string> GenerateDirtyPickUpLines(string request);
        public Task<string> GenerateNormalPickUpLines(string request);
        public Task<string> GenerateRizzLines(string request);
        public Task<string> GenerateDirtyRizzLines(string request);
    }
}
