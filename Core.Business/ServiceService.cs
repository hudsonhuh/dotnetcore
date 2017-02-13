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
    // operations you want to expose
    public interface IServiceService
    {
        List<Service> GetServiceList(string name = null);
        Service GetService(int id);
        Service GetService(string name);
        void CreateService(Service service);
        void SaveService();
    }

    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Service> servicesRepository;

        public ServiceService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.servicesRepository = this.unitOfWork.GetRepository<Service>();
        }

        #region IServiceService Members

        public List<Service> GetServiceList(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return this.servicesRepository.List.ToList();
            else
                return this.servicesRepository.List.Where(c => c.ServiceName == name).Select(s => new Service() { ServiceNo = s.ServiceNo, ServiceName = s.ServiceName }).ToList();
        }

        public Service GetService(int id)
        {
            var Service = this.servicesRepository.FindBy(c => c.ServiceNo == id).FirstOrDefault<Service>();
            return Service;
        }

        public Service GetService(string name)
        {
            var Service = this.servicesRepository.GetSingle(c => c.ServiceName == name);
            return Service;
        }

        public void CreateService(Service Service)
        {
            this.servicesRepository.Add(Service);
        }

        public void SaveService()
        {
            this.unitOfWork.Commit();
        }

        #endregion
    }
}
