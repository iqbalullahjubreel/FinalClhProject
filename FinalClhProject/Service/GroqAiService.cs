//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using GroqSharp;
using GroqSharp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.Service
{
    public class GroqAiService:IGroqAiService
    {
        private readonly IGroqClient _groqClient;
        private List<ChatStorageHistory> _chatHistory = new List<ChatStorageHistory>();

        public GroqAiService(IGroqClient groqClient)
        {
            _groqClient = groqClient;
        }

        public async Task<string> AiRizzResponse(string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                throw new ArgumentException("Request cannot be null or empty.", nameof(request));
            }

            var systemMessage = new Message
            {
                Content = "You are a charming, witty, and emotionally intelligent AI. Respond briefly, with warmth and encouragement. Keep it flirty, helpful, or romantic — but never too long and dont say anything outside Relationship stuff Understood.",
                Role = MessageRoleType.User
            };


            var groqMessages = _chatHistory.Select(m => new Message
            {
                Content = m.Content,
                Role = m.Role == "User" ? MessageRoleType.User : MessageRoleType.Assistant
            }).ToList();

            if (!groqMessages.Any())
            {
                groqMessages.Add(systemMessage);
            }

            groqMessages.Add(new Message { Content = request, Role = MessageRoleType.User });

            Console.WriteLine(JsonConvert.SerializeObject(groqMessages));
            var response = await _groqClient.CreateChatCompletionAsync(groqMessages.ToArray());

            _chatHistory.Add(new ChatStorageHistory
            {
                Content = request,
                Role = "User",
                Timestamp = DateTime.UtcNow
            });

            _chatHistory.Add(new ChatStorageHistory
            {
                Content = response.Trim(),   
                Role = "Assistant",
                Timestamp = DateTime.UtcNow
            });

            return response.Trim();
        }

        public async Task<string> GenerateDirtyPickUpLines(string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                throw new ArgumentException("Request cannot be null or empty.", nameof(request));
            }

            var messageArray = new List<Message>
            {
                new Message { Content = "Generate exactly 5 short, spicy, and clever Dirty pick-up lines. Don't include explanations or extra text — just list the Dirty pick-up lines", Role = MessageRoleType.User },
                new Message { Content = request, Role = MessageRoleType.User }
            };

            Console.WriteLine(JsonConvert.SerializeObject(messageArray));

            try
            {
                var response = await _groqClient.CreateChatCompletionAsync(messageArray.ToArray());
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "An error occurred while generating pick-up lines. Please try again.";
            }
        }

        public async Task<string> GenerateNormalPickUpLines(string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                throw new ArgumentException("Request cannot be null or empty.", nameof(request));
            }

            var messageArray = new List<Message>
            {
                new Message { Content = "Generate exactly 5 short, spicy, and clever pick-up lines. Don't include explanations or extra text — just list the pick-up lines", Role = MessageRoleType.User },
                new Message { Content = request, Role = MessageRoleType.User }
            };

            Console.WriteLine(JsonConvert.SerializeObject(messageArray));

            try
            {
                var response = await _groqClient.CreateChatCompletionAsync(messageArray.ToArray());
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "An error occurred while generating pick-up lines. Please try again.";
            }
        }

        public async Task<string> GenerateRizzLines(string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                throw new ArgumentException("Request cannot be null or empty.", nameof(request));
            }

            var messageArray = new List<Message>
            {
                new Message { Content = "Generate exactly 5 short, spicy, and clever Rizz lines. Don't include explanations or extra text — just list the Rizz lines", Role = MessageRoleType.User },
                new Message { Content = request, Role = MessageRoleType.User }
            };

            Console.WriteLine(JsonConvert.SerializeObject(messageArray));

            try
            {
                var response = await _groqClient.CreateChatCompletionAsync(messageArray.ToArray());
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "An error occurred while generating rizz lines. Please try again.";
            }
        }

        public async Task<string> GenerateDirtyRizzLines(string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                throw new ArgumentException("Request cannot be null or empty.", nameof(request));
            }

            var messageArray = new List<Message>
            {
                new Message { Content = "Generate exactly 5 short, spicy, and clever Dirty pick-up lines. Don't include explanations or extra text — just list the Dirty Rizz lines", Role = MessageRoleType.User },
                new Message { Content = request, Role = MessageRoleType.User }
            };

            Console.WriteLine(JsonConvert.SerializeObject(messageArray));

            try
            {
                var response = await _groqClient.CreateChatCompletionAsync(messageArray.ToArray());
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "An error occurred while generating rizz lines. Please try again.";
            }
        }
    }
}
