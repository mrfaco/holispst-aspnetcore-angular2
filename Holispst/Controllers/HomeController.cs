using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Repositories;

namespace Holispst.Controllers
{
    public class HomeController : Controller
    {

        private IMateriasRepository materiasRepository;

        public HomeController(IMateriasRepository materiasRepository)
        {
            this.materiasRepository = materiasRepository;
        }

        public IActionResult Index()
        {
            var mats = materiasRepository.GetAllMaterias();
            return View(mats);
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
            return View();
        }
    }
}
