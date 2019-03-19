using Microsoft.AspNet.SignalR;

namespace Blog.Web
{
    public class NotificationHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.addNotification(message);
        }
    }
}