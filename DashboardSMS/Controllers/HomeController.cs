using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DashboardSMS.Models;
using Microsoft.AspNetCore.SignalR;
using Hubs;

namespace DashboardSMS.Controllers
{

    public class HomeController : Controller
    {
        IHubContext<MonitoramentoHub, ITypedHubClient> _chatHubContext;
        public HomeController(IHubContext<MonitoramentoHub, ITypedHubClient> chatHubContext)
        {
            _chatHubContext = chatHubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Chart()
        {
            return View();
        }

        public IActionResult _SucessoModal()
        {
            return View();
        }
    }
}
