using CoreHoney.Business.Abstract;
using CoreHoney.Entities;
using CoreHoney.WEBUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreHoney.WEBUI.Controllers
{
    public class HoneyController : Controller
    {

        private IHoneyService _honeyService;

        public HoneyController (IHoneyService honeyService)
        {
            _honeyService = honeyService;
        }

        public IActionResult Details( int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Honey honey = _honeyService.GetById((int)id);
            if (honey == null)
            {
                return NotFound();
            }
            return View(honey);
            

        }


        public IActionResult List()
        {
            return View(new HoneyListModel()
            {
                Honeys= _honeyService.GetAll()
            });
        }



    }
}
