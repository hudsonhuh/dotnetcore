using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Business;
using Core.Entity.Decanter;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Web.Controllers
{
    public class LanguageController : BaseController
    {
        public LanguageController(IServiceService serviceService, ILanguageService languageService) : base(serviceService, languageService)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
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

        //[HttpPost]
        //public IActionResult CreateLanguage(int serviceNo, string key, string en, string zh_cn, string zh_tw, string ja, string ru)
        //{
        //    Language language = new Language()
        //    {
        //        ServiceNo = serviceNo,
        //        Key = key,
        //        en = en,
        //        zh_CN = zh_cn,
        //        zh_TW = zh_tw,
        //        ja = ja,
        //        ru = ru
        //    };
        //    bool result = false;
        //    string errMsg = string.Empty;
        //    try
        //    {
        //        languageService.CreateLanguage(language);
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result = false;
        //        errMsg = ex.Message;
        //    }

        //    return Json(new { id = language.LanguageNo, result = result, errMsg = errMsg });
        //}

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
                        if(!string.IsNullOrEmpty(languageNoList))
                        {
                            try
                            {
                                string[] languageNoArray = JsonConvert.DeserializeObject<string[]>(languageNoList);
                                List<int> languages = languageNoArray.Select(Int32.Parse).ToList();
                                languageService.DeleteLanguage(languages);
                            }
                            catch(Exception ex)
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
    }
}
