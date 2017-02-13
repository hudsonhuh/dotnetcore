using Core.Data.Infrastructure;
using Core.Entity.Decanter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Repositories
{
    public class LanguageRepository : RepositoryBase<Language>, ILanguageRepository
    {
        public LanguageRepository(DecanterContext context)
            : base(context)
        {
        }

        public override void Update(Language entity)
        {
            entity.RegDate = DateTime.Now;
            base.Update(entity);
        }

        public Language GetLanguageByKey(int serviceNo, string key)
        {
            var language = this.DbContext.Language.Where(c => c.Key == key).FirstOrDefault();
            return language;
        }
    }

    public interface ILanguageRepository : IRepository<Language>
    {
        Language GetLanguageByKey(int serviceNo, string key);
    }
}
