using System.Threading.Tasks;

namespace Tracker.Web.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
