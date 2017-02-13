using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Business;
using Core.Entity.Decanter;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.APIService.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LanguageController : Controller
    {
        private readonly ILanguageService languageService;
        private readonly IServiceService serviceService;

        public LanguageController(IServiceService serviceService, ILanguageService languageService)
        {
            this.languageService = languageService;
            this.serviceService = serviceService;
        }

        [HttpPost]
        [HttpGet]
        public IActionResult GetServiceList()
        {
            List<Core.Entity.Decanter.Service> serviceList = serviceService.GetServiceList();
            return Json(new { rows = serviceList });
        }

        [HttpPost]
        public IActionResult GetLanguageList(int serviceNo, string keyName, int rows, int page, string sidx, string sord)
        {
            int records = -1;
            List<Language> languageList = languageService.GetLanguageList(serviceNo, keyName, rows, page, sidx, sord, out records);
            int totalPage = records > 0 && rows > 0 ? (int)Math.Ceiling(((double)records / (double)rows)) : 1;
            return Json(new { rows = languageList, records = records, page = page, total = totalPage });
        }

        [HttpPost]
        public IActionResult GetLanguageAll(int serviceNo)
        {
            List<Language> languageList = languageService.GetLanguageAll(serviceNo);
            return Json(new { rows = languageList });
        }

        [HttpPost]
        public IActionResult GetLanguage(int languageNo)
        {
            Language language = languageService.GetLanguage(languageNo);
            return Json(new { rows = language });
        }

        [HttpPost]
        public IActionResult Manage(string oper, int languageNo, int serviceNo, string key, string en, string zh_cn, string zh_tw, string ja, string ru, string languageNoList)
        {
            if (string.IsNullOrEmpty(oper)) return Json(new { result = false, errMsg = "Not Operation." });

            string errMsg = string.Empty;
            Language language = new Language()
            {
                LanguageNo = languageNo,
                ServiceNo = serviceNo,
                Key = key,
                en = en,
                zh_CN = zh_cn,
                zh_TW = zh_tw,
                ja = ja,
                ru = ru
            };

            bool result = false;

            try
            {
                switch (oper.ToLower())
                {
                    case "add":
                        languageService.CreateLanguage(language);
                        result = true;
                        break;
                    case "edit":
                        languageService.UpdateLanguage(language);
                        result = true;
                        break;
                    case "del":
                        if (!string.IsNullOrEmpty(languageNoList))
                        {
                            try
                            {
                                string[] languageNoArray = JsonConvert.DeserializeObject<string[]>(languageNoList);
                                List<int> languages = languageNoArray.Select(Int32.Parse).ToList();
                                languageService.DeleteLanguage(languages);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                        }
                        result = true;
                        break;
                    default:

                        break;
                }
            }
            catch (Exception ex)
            {
                result = false;
                errMsg = ex.Message;
            }

            return Json(new { id = language.LanguageNo, result = result, errMsg = errMsg });
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value : " + id;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
