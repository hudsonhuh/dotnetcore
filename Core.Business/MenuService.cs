using Core.Data.Infrastructure;
using Core.Entity.Decanter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Business
{
    // operations you want to expose
    public interface IMenuService
    {
        List<Menu> GetMenuList(int serviceNo);
        Menu GetMenu(int menuNo);
        void CreateMenu(Menu Menu);
        void UpdateMenu(Menu Menu);
        void DeleteMenu(Menu Menu);
        void SaveMenu();
    }

    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Menu> menuRepository;

        public MenuService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.menuRepository = this.unitOfWork.GetRepository<Menu>();
        }

        #region MenuService Menus
        
        public Menu GetMenu(int menuNo)
        {
            var Menu = this.menuRepository.FindBy(c => c.MenuNo == menuNo).FirstOrDefault<Menu>();
            return Menu;
        }

        public List<Menu> GetMenuList(int serviceNo)
        {
            var menuRoleList =
                from menu in this.menuRepository.List
                where menu.ServiceNo == serviceNo && menu.IsDelete == false && menu.IsPublic == true
                select new Menu
                {
                    MenuNo = menu.MenuNo,
                    MenuName = menu.MenuName,
                    MenuUrl = menu.MenuUrl,
                    Description = menu.Description,
                    Service = new Service()
                    {
                        ServiceName = menu.Service.ServiceName
                    }
                };
            return menuRoleList.ToList();
        }

        public void CreateMenu(Menu Menu)
        {
            this.menuRepository.Add(Menu);
        }

        public void UpdateMenu(Menu Menu)
        {
            this.menuRepository.Update(Menu);
        }

        public void DeleteMenu(Menu Menu)
        {
            this.menuRepository.Delete(Menu);
        }

        public void SaveMenu()
        {
            this.menuRepository.Commit();
        }

        #endregion
    }
}
