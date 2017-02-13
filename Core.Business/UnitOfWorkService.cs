using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Data.Infrastructure;
using Core.Data.Repositories;
using Core.Entity.Decanter;

namespace Core.Business
{
    public class UnitOfWorkService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Language> languagesRepository;
        private readonly IRepository<Service> servicesRepository;

        public UnitOfWorkService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.languagesRepository = this.unitOfWork.GetRepository<Language>();
            this.servicesRepository = this.unitOfWork.GetRepository<Service>();
        }

        public List<Language> GetLanguageList(int serviceNo, string key = null)
        {
            return this.languagesRepository.List.ToList();
        }

        public Language GetLanguage(int languageNo)
        {
            Language result = null;

            var language =
                    from lang in this.languagesRepository.List
                    join service in this.servicesRepository.List on lang.ServiceNo equals service.ServiceNo
                    where lang.LanguageNo == languageNo
                    select new Language{
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
            
            foreach (Language item in language)
            {
                result = item;
            }
            return result;
        }
    }
}
