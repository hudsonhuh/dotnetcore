using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Business;
using Core.Entity.Decanter;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Web.Controllers
{
    public class MemberController : BaseController
    {
        public MemberController(IMemberService memberService, IMenuService menuService) : base(memberService, menuService)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //for(int i=1; i<1000; i++)
            //{
            //    Member member = new Member()
            //    {
            //        MemberId = "test" + i,
            //        Password = "pass" + i,
            //        MemberName = "name" + i,
            //        DepartmentNo = 1,
            //        Email = "test" + i + "@nsuslab.com",
            //        UserIP = "127.0.0.1",
            //        IsDelete = false,
            //        RegDate = DateTime.Now
            //    };
            //    memberService.CreateMember(member);
            //}
            //memberService.SaveMember();

            //for (int i = 1; i < 1000; i++)
            //{
            //    Menu menu = new Menu()
            //    {
            //        MenuName = "menu" + i,
            //        Description = "desc" + i,
            //        MenuUrl = "/Menu/" + i,
            //        IsPublic = true,
            //        ServiceNo = i%6+5,
            //        SortOrder = 1,
            //        IsDelete = false,
            //        RegDate = DateTime.Now
            //    };
            //    menuService.CreateMenu(menu);
            //}
            //memberService.SaveMember();

            //for (int i = 1; i <= 500; i++)
            //{
            //    for(int j=1;j<=5;j++)
            //    {
            //        MemberRole role = new MemberRole()
            //        {
            //            MemberNo = i,
            //            MenuNo = (j+i)%1000+1,
            //            RegDate = DateTime.Now
            //        };
            //        memberService.CreateMemberRole(role);
            //    }
            //}
            //memberService.SaveMember();

            return Json(new {  });
        }

        // GET: /<controller>/
        public IActionResult Login()
        {
            string id = "test700";
            string pass = "pass700";
            Member member = memberService.Login(id, pass);
            var memberRoleList = memberService.GetMemberRole(member.MemberNo);


            var result = new List<dynamic>();
            foreach(MemberRole memberRole in memberRoleList)
            {
                result.Add(new
                {
                    RoleNo = memberRole.RoleNo,
                    MenuNo = memberRole.Menu.MenuNo,
                    ServiceNo = memberRole.Menu.ServiceNo
                });
            }
            return Json(new { data = member, role = result });
        }
    }
}
