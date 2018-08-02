using System.Threading.Tasks;

namespace Hubs
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string name, string value);

        Task UpdateChart(string name, string value, string color);

        Task UpdatePier(object pierData);

        Task UpdateBar(object BarData);
    }
}
