using CoreHoney.Business.Abstract;
using CoreHoney.WEBUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreHoney.WEBUI.Controllers
{
    public class HomeController : Controller
    {
        private IHoneyService _HoneyService;



        public HomeController(IHoneyService honeyService)
        {
            _HoneyService = honeyService;
        }

        public IActionResult Index()
        {
            return View(new HoneyListModel() { Honeys = _HoneyService.GetAll() });
        }

       

        
    }
}