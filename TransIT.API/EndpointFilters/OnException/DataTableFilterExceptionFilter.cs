using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TransIT.API.EndpointFilters.OnException
{
    public class DataTableFilterExceptionFilter : Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new JsonResult(new { Error = context.Exception.Message });
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
