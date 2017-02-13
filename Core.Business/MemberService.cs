using Core.Data.Infrastructure;
using Core.Entity.Decanter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Business
{
    // operations you want to expose
    public interface IMemberService
    {
        Member Login(string memberId, string password);
        Member GetMember(int id);
        Member GetMember(string name);
        List<MemberRole> GetMemberRole(int id);
        void CreateMember(Member Member);
        void UpdateMember(Member Member);
        void DeleteMember(Member Member);
        void CreateMemberRole(MemberRole MemberRole);
        void SaveMember();
    }

    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Member> memberRepository;
        private readonly IRepository<MemberRole> memberRoleRepository;
        private readonly IRepository<Department> departmentRepository;
        private readonly IRepository<Menu> menuRepository;

        public MemberService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.memberRepository = this.unitOfWork.GetRepository<Member>();
            this.memberRoleRepository = this.unitOfWork.GetRepository<MemberRole>();
            this.departmentRepository = this.unitOfWork.GetRepository<Department>();
            this.menuRepository = this.unitOfWork.GetRepository<Menu>();
        }

        #region MemberService Members

        public Member Login(string memberId, string password)
        {
            if (string.IsNullOrEmpty(memberId) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                return (
                    from member in this.memberRepository.List
                    join memberRole in this.memberRoleRepository.List on member.MemberNo equals memberRole.MemberNo into memberRole1
                    from memberRole2 in memberRole1.DefaultIfEmpty()
                    join department in this.departmentRepository.List on member.DepartmentNo equals department.DepartmentNo
                    where member.MemberId == memberId && member.Password == password && member.IsDelete == false
                    select new Member
                    {
                        MemberNo = member.MemberNo,
                        MemberId = member.MemberId,
                        MemberName = member.MemberName,
                        Department = new Department()
                        {
                            DepartmentName = department.DepartmentName
                        },
                        Email = member.Email
                    }).FirstOrDefault();
                //return this.memberRepository.List
                //    .Where(c => c.MemberId == memberId && c.Password == password)
                //    .Select(s => new Member() { MemberNo = s.MemberNo, MemberName = s.MemberName })
                //    .FirstOrDefault();
            }
        }

        public Member GetMember(int id)
        {
            var Member = this.memberRepository.FindBy(c => c.MemberNo == id).FirstOrDefault<Member>();
            return Member;
        }

        public Member GetMember(string memberId)
        {
            var Member = this.memberRepository.GetSingle(c => c.MemberName == memberId);
            return Member;
        }

        public List<MemberRole> GetMemberRole(int id)
        {
            var memberRoleList =
                from memberRole in this.memberRoleRepository.List
                join member in this.memberRepository.List on memberRole.MemberNo equals member.MemberNo
                join menu in this.menuRepository.List on memberRole.MenuNo equals menu.MenuNo
                where member.MemberNo == id && member.IsDelete == false
                select new MemberRole
                {
                    RoleNo = memberRole.RoleNo,
                    Menu = new Menu()
                    {
                        MenuNo = menu.MenuNo,
                        ServiceNo = menu.ServiceNo
                    }
                };
            return memberRoleList.ToList();
        }

        public void CreateMember(Member Member)
        {
            this.memberRepository.Add(Member);
        }

        public void UpdateMember(Member Member)
        {
            this.memberRepository.Update(Member);
        }

        public void DeleteMember(Member Member)
        {
            this.memberRepository.Delete(Member);
        }

        public void CreateMemberRole(MemberRole MemberRole)
        {
            this.memberRoleRepository.Add(MemberRole);
        }

        public void SaveMember()
        {
            this.unitOfWork.Commit();
        }

        #endregion
    }
}
