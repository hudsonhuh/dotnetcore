using Core.Entity.Decanter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data
{
    public class DecanterSeedData
    {
        private static DecanterContext context;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            context = (DecanterContext)serviceProvider.GetService(typeof(DecanterContext));

            if (!context.Service.Any())
            {
                InitializeSchedules();
            }
        }

        private static void InitializeSchedules()
        {
            GetServiceList().ForEach(c => context.Service.Add(c));
            context.SaveChanges();
        }

        private static List<Service> GetServiceList()
        {
            return new List<Service>
            {
                new Service {
                    ServiceName = "Decanter",
                    IsPublic = true,
                    RegDate = DateTime.Now
                },
                new Service {
                    ServiceName = "Brandy",
                    IsPublic = true,
                    RegDate = DateTime.Now
                },
                new Service {
                    ServiceName = "Maipo",
                    IsPublic = true,
                    RegDate = DateTime.Now
                },
                new Service {
                    ServiceName = "Client-Mac",
                    IsPublic = true,
                    RegDate = DateTime.Now
                },
                new Service {
                    ServiceName = "Client-Web",
                    IsPublic = true,
                    RegDate = DateTime.Now
                },
                new Service {
                    ServiceName = "Client-WPF",
                    IsPublic = true,
                    RegDate = DateTime.Now
                }
            };
        }
    }
}
