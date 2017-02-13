using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

using Core.Data.Infrastructure;
using Core.Data.Repositories;
using Core.Entity.Decanter;

namespace Core.Business
{
    // operations you want to expose
    public interface ILanguageService
    {
        List<Language> GetLanguageList(int serviceNo, string name, int rows, int page, string sidx, string sord, out int total);
        List<Language> GetLanguageAll(int serviceNo);
        Language GetLanguage(int id);
        Language GetLanguage(int serviceNo, string name);
        void CreateLanguage(Language Language);
        void UpdateLanguage(Language Language);
        void DeleteLanguage(Language Language);
        void DeleteLanguage(List<int> languageNoList);
        void SaveLanguage();
    }

    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Language> languagesRepository;
        private readonly IRepository<Service> servicesRepository;

        public LanguageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.languagesRepository = this.unitOfWork.GetRepository<Language>();
            this.servicesRepository = this.unitOfWork.GetRepository<Service>();
        }

        #region ILanguageService Members

        public List<Language> GetLanguageList(int serviceNo, string key, int rows, int page, string sidx, string sord, out int total)
        {
            var languageList =
                from service in this.servicesRepository.List
                join lang in this.languagesRepository.List on service.ServiceNo equals lang.ServiceNo
                where service.ServiceNo == serviceNo
                select new Language
                {
                    LanguageNo = lang.LanguageNo,
                    ServiceNo = lang.ServiceNo,
                    Key = lang.Key,
                    en = lang.en,
                    zh_CN = lang.zh_CN,
                    zh_TW = lang.zh_TW,
                    ja = lang.ja,
                    ru = lang.ru,
                    Service = new Service()
                    {
                        ServiceName = service.ServiceName
                    }
                };

            if(!string.IsNullOrEmpty(key))
            {
                languageList =
                    from data in languageList
                    where data.Key.Contains(key)
                    select data;
            }

            total = languageList.Count();
            
            if (!string.IsNullOrEmpty(sidx))
            {
                languageList = languageList.OrderBy(sidx + " " + sord);
            }
            
            return languageList.Skip(rows * (page - 1)).Take(rows).ToList();
        }

        public List<Language> GetLanguageAll(int serviceNo)
        {
            // left join
            var languageList = 
                from service in this.servicesRepository.List
                join lang in this.languagesRepository.List on service.ServiceNo equals lang.ServiceNo
                where service.ServiceNo == serviceNo
                //orderby sidx
                select new Language
                {
                    LanguageNo = lang.LanguageNo,
                    ServiceNo = lang.ServiceNo,
                    Key = lang.Key,
                    en = lang.en,
                    zh_CN = lang.zh_CN,
                    zh_TW = lang.zh_TW,
                    ja = lang.ja,
                    ru = lang.ru,
                    Service = new Service()
                    {
                        ServiceName = service.ServiceName
                    }
                };

            return languageList.ToList();
        }

        public Language GetLanguage(int languageNo)
        {
            var language =
                from service in this.servicesRepository.List
                join lang in this.languagesRepository.List on service.ServiceNo equals lang.ServiceNo
                where lang.LanguageNo == languageNo
                select new Language
                {
                    LanguageNo = lang.LanguageNo,
                    ServiceNo = lang.ServiceNo,
                    Key = lang.Key,
                    en = lang.en,
                    zh_CN = lang.zh_CN,
                    zh_TW = lang.zh_TW,
                    ja = lang.ja,
                    ru = lang.ru,
                    Service = new Service()
                    {
                        ServiceName = service.ServiceName
                    }
                };
            return language.FirstOrDefault();
        }

        public Language GetLanguage(int serviceNo, string name)
        {
            var language = (string.IsNullOrEmpty(name) ?
                from lang in this.languagesRepository.List
                where lang.ServiceNo == serviceNo
                select new Language
                {
                    LanguageNo = lang.LanguageNo,
                    ServiceNo = lang.ServiceNo,
                    Key = lang.Key,
                    en = lang.en,
                    zh_CN = lang.zh_CN,
                    zh_TW = lang.zh_TW,
                    ja = lang.ja,
                    ru = lang.ru
                } :
                from lang in this.languagesRepository.List
                where lang.ServiceNo == serviceNo && lang.Key == name
                select new Language
                {
                    LanguageNo = lang.LanguageNo,
                    ServiceNo = lang.ServiceNo,
                    Key = lang.Key,
                    en = lang.en,
                    zh_CN = lang.zh_CN,
                    zh_TW = lang.zh_TW,
                    ja = lang.ja,
                    ru = lang.ru
                }
            );
            return language.ToList().FirstOrDefault();
        }

        public void CreateLanguage(Language Language)
        {
            Language.RegDate = DateTime.Now;
            this.languagesRepository.Add(Language);
            this.SaveLanguage();
        }

        public void UpdateLanguage(Language Language)
        {
            Language.RegDate = DateTime.Now;
            this.languagesRepository.Update(Language);
            this.SaveLanguage();
        }

        public void DeleteLanguage(Language Language)
        {
            this.languagesRepository.Delete(Language);
            this.SaveLanguage();
        }

        public void DeleteLanguage(List<int> languageNoList)
        {
            foreach(int languageNo in languageNoList)
            {
                this.languagesRepository.Delete(new Language { LanguageNo = languageNo });
            }
            this.SaveLanguage();
        }

        public void SaveLanguage()
        {
            this.unitOfWork.Commit();
        }

        #endregion
    }
}