using IoTSensorPortal.Core.Contracts;
using System.Threading.Tasks;

namespace IoTSensorPortal.Core
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
