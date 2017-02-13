using Core.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.APIService.Lib
{
    public class CoreHttpApplication
    {
        private readonly RequestDelegate next;

        public CoreHttpApplication(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            this.BeginInvoke(context);
            await this.next.Invoke(context);
            this.EndInvoke(context);
        }

        private void BeginInvoke(HttpContext context)
        {
            // Do custom work before controller execution
        }

        private void EndInvoke(HttpContext context)
        {
            // Do custom work after controller execution
        }
    }
}
