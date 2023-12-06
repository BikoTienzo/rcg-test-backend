using System.Text;
using RCGTestBackend.Domain.Interfaces.Services;

namespace RCGTestBackend.Application.Services
{
    public class StringProcessingService : IStringProcessingService
    {
        public string EncodeToBase64(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
