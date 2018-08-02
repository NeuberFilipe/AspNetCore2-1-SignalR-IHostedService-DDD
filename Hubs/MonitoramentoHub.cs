using Microsoft.AspNetCore.SignalR;

namespace Hubs
{
    public class MonitoramentoHub : Hub<ITypedHubClient>
    {
        public void Send(string name, string message)
        {
            Clients.All.BroadcastMessage(name, message);
        }

        public void SendChart(string name, string value, string color)
        {
            Clients.All.UpdateChart(name, value, color);
        }

        public void SendPier(object pierData)
        {
            Clients.All.UpdatePier(pierData);
        }

        public void SendBar(object barData)
        {
            Clients.All.UpdateBar(barData);
        }
    }
}
