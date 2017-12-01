using System.Threading.Tasks;

namespace IoTSensorPortal.Core.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
