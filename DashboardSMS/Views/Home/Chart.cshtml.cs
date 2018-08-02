using System;
using System.Threading.Tasks;
using DashboardSMS.Service;
using Hubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace DashboardSMS.Views.Home
{
    public class ChartModel : PageModel
    {

        private readonly IApplicationLifetime _appLifetime;
        private readonly ILogger _logger;
        IHubContext<MonitoramentoHub, ITypedHubClient> _chatHubContext;
        #region snippet1
        public ChartModel(IBackgroundTaskQueue queue,
            IApplicationLifetime appLifetime,
            ILogger<ChartModel> logger, IHubContext<MonitoramentoHub, ITypedHubClient> chatHubContex)
        {
            Queue = queue;
            _appLifetime = appLifetime;
            _logger = logger;
            _chatHubContext = chatHubContex;
        }

        public IBackgroundTaskQueue Queue { get; }
        #endregion

        public void OnGet()
        {
        }

        #region snippet2
        public IActionResult OnPostAddTask()
        {
            Queue.QueueBackgroundWorkItem(async token =>
            {
                var guid = Guid.NewGuid().ToString();

                for (int delayLoop = 0; delayLoop < 3; delayLoop++)
                {
                    _logger.LogInformation(
                        $"Queued Background Task {guid} is running. {delayLoop}/3");
                    await Task.Delay(TimeSpan.FromSeconds(5), token);
                }

                _logger.LogInformation(
                    $"Queued Background Task {guid} is complete. 3/3");
            });

            return RedirectToPage();
        }
        #endregion
    }
}