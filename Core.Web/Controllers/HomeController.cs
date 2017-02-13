using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Business;
using Core.Data.Repositories;
using Core.Entity.Decanter;

namespace Core.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IServiceService serviceService, ILanguageService languageService) : base(serviceService, languageService)
        {
        }

        public IActionResult Index()
        {
            //List<Language> list = this.service.GetLanguageList(2);
            //Language language = this.service.GetLanguage(200);
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
            return View();
        }
    }
}
