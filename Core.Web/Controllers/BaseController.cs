using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Business;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILanguageService languageService;
        protected readonly IServiceService serviceService;
        protected readonly IMemberService memberService;
        protected readonly IMenuService menuService;

        public BaseController(IServiceService serviceService, ILanguageService languageService)
        {
            this.languageService = languageService;
            this.serviceService = serviceService;
        }

        public BaseController(IMemberService memberService, IMenuService menuService)
        {
            this.memberService = memberService;
            this.menuService = menuService;
        }

        [HttpPost]
        public IActionResult GetServiceList()
        {
            List<Core.Entity.Decanter.Service> serviceList = serviceService.GetServiceList();
            return Json(new { rows = serviceList });
        }
    }
}
